using CardService.Data;
using System.Collections.Generic;

namespace CardService.Models.Requests
{
    public class GetCardsResponse : IOperationResult
    {
        public IList<CardDto>? Cards { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
