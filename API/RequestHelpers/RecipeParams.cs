namespace API.RequestHelpers;
public class RecipeParams : PaginationParams
{
    public string Search { get; set; }
    public string SortBy { get; set; }
    public int OrderBy { get; set; }
    public string Category { get; set; }
}
