namespace API.DTOs;
public class RatingResponseDto
{
    public int RatingId { get; set; }
    public string UserId { get; set; }
    public double RatingNum { get; set; }
    public string Comment { get; set; }
}
