namespace API.DTOs;

public class BookmarksDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int TotalBookmarks { get; set; }
    public List<EmbeddedDto> Recipes { get; set; }
}
