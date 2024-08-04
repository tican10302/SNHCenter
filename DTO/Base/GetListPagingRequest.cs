namespace DTO.Base
{
    public class GetListPagingRequest
    {
        public string? Search { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; } = 10;
        public string? Order { get; set; }
        public string? Sort { get; set; }
    }
}
