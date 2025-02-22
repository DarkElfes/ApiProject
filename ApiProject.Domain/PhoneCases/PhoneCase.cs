using ApiProject.Domain.Abstractions;

namespace ApiProject.Domain.PhoneCases;

public class PhoneCase : Entity
{
    public string Name { get; set; } = string.Empty;
    public PhoneModelType Model { get; set; } = PhoneModelType.Universal;
    public string ImgUrl { get; set; } = null!;
    public string Color { get; set; } = string.Empty;
    public short Stock { get; set; }
    public short Sold { get; set; }
    public double Price { get; set; }
}
