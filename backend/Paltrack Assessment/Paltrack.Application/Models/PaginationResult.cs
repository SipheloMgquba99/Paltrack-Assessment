﻿public class PaginationResult<T>
{
    public IQueryable<T>? Items { get; set; }
    public int TotalCount { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public int TotalPages { get; init; }
}