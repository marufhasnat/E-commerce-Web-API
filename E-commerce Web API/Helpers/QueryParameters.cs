using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Web_API.Helpers
{
    public class QueryParameters
    {
        private const int MaxPageSize = 50;

        //[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 6, [FromQuery] string? search = null, [FromQuery] string? sortOrder = null

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string? Search { get; set; }
        public string? SortOrder { get; set; }

        public QueryParameters Validate()
        {
            if(PageNumber < 1)
            {
                PageNumber = 1;
            }
            if (PageSize < 1)
            {
                PageSize = 6;
            }
            if (PageSize > MaxPageSize)
            {
                PageSize = MaxPageSize;
            }

            return this;
        }
    }
}
