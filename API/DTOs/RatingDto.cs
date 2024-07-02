using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public class RatingDto
{
    [Required(ErrorMessage = "Rating Number is required")]
    public required int RatingNum { get; set; }
    [Required(ErrorMessage = "Comment is required")]
    public required string Comment { get; set; }
}
