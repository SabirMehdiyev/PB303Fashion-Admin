using System.ComponentModel.DataAnnotations.Schema;

namespace PB303Fashion.DataAccessLayer.Entities;

public class Slider:Entity
{
    public string? ImgUrl { get; set; }
    public string Content { get; set; }
    public string SubText { get; set; }
    public string Description  { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
}
