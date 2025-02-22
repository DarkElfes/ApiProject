using ApiProject.Domain.PhoneCases;

namespace ApiProject.Shared.Cases.Response;
public class PhoneCaseResponse
{ 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PhoneModelTypeDto Model { get; set; }
    public string ImgUrl { get; set; } = null!;
    public string Color { get; set; } = string.Empty;
    public short Stock { get; set; }
    public double Price { get; set; } 
    public short Sold { get; set; }
}
