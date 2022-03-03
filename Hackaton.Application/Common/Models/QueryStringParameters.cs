namespace Hackaton.Application.Common.Models
{
    public abstract class QueryStringParameters
    {
        private const int maxPageSize = 30;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize)? maxPageSize : value;
        }
        public string OrderBy { get; init; } = string.Empty;

        public string Fields { get; init; } = string.Empty;

    }
}