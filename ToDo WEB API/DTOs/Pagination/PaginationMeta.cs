namespace ToDo_WEB_API.DTOs.Pagination;

public class PaginationMeta
{

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }

    public PaginationMeta(int page, int pageSize, int totalCount)
    {
        Page = page;
        PageSize = pageSize;
        TotalPages = Convert.ToInt32(Math.Ceiling((1.0 * totalCount) / pageSize));
    }
}
