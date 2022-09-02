namespace CardService.Models.Requests
{
    public class CreateClientResponse : IOperationResult
    {
        public int? ClientId { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
