using AutoMapper;
using CardService.Data;
using CardService.Models;
using CardService.Models.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CardService.Services.Impl
{
    public class AuthenticateService : IAuthenticateService
    {        

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        private readonly Dictionary<string, SessionInfo> _sessions =
            new Dictionary<string, SessionInfo>();

        public const string SecretKey = "jEw5g8qc+2B?F)G-";

        public AuthenticateService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public SessionInfo GetSessionInfo(string sessionToken)
        {
            SessionInfo sessionInfo;

            lock (_sessions)
            {
                _sessions.TryGetValue(sessionToken, out sessionInfo);
            }

            if (sessionInfo == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                CardServiceDbContext context = scope.ServiceProvider.GetRequiredService<CardServiceDbContext>();

                AccountSession session = context
                        .AccountSessions
                        .FirstOrDefault(item => item.SessionToken == sessionToken);

                if (session == null)
                    return null;

                Account account = context.Accounts.FirstOrDefault(item => item.AccountId == session.AccountId);

                sessionInfo = GetSessionInfo(account, session);

                if (sessionInfo != null)
                {
                    lock (_sessions)
                    {
                        _sessions[sessionToken] = sessionInfo;
                    }
                }

            }

            return sessionInfo;
        }

        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            CardServiceDbContext context = scope.ServiceProvider.GetRequiredService<CardServiceDbContext>();

            Account account =
             !string.IsNullOrWhiteSpace(authenticationRequest.Login) ? 
             FindAccountByLogin(context, authenticationRequest.Login) : null;

            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.UserNotFound
                };
            }

            if (!PasswordUtils.VerifyPassword(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.InvalidPassword
                };
            }


            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeLastRequest = DateTime.Now,
                IsClosed = false,
            };

            context.AccountSessions.Add(session);

            context.SaveChanges();


            SessionInfo sessionInfo = GetSessionInfo(account, session);

            lock (_sessions)
            {
                _sessions[sessionInfo.SessionToken] = sessionInfo;
            }


            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                SessionInfo = sessionInfo
            };

        }

        private SessionInfo GetSessionInfo(Account account, AccountSession accountSession)
        {
            var accountDTO = _mapper.Map<AccountDto>(account);
            return new SessionInfo
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Account = accountDTO
                //Account = new AccountDto
                //{
                //    AccountId = account.AccountId,
                //    EMail = account.EMail,
                //    FirstName = account.FirstName,
                //    LastName = account.LastName,
                //    SecondName = account.SecondName,
                //    Locked = account.Locked
                //}
            };
        }


        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                        new Claim(ClaimTypes.Name, account.EMail),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Account FindAccountByLogin(CardServiceDbContext context, string login)
        {
            return context
                .Accounts
                .FirstOrDefault(account => account.EMail == login);
        }
    }
}
