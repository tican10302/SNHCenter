﻿namespace DTO.Base
{
    public class GetListPagingRequest
    {
        public string TextSearch { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PageIndex { get; set; }
        public int RowPerPage { get; set; } = 10;
    }
}
