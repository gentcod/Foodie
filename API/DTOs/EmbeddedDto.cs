using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class EmbeddedDto
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string ImageSrc { get; set; }
   }
}