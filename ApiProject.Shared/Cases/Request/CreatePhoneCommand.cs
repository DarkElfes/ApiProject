using ApiProject.Domain.PhoneCases;
using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Cases.Response;
using MediatR;

namespace ApiProject.Shared.Cases.Request;
public class CreatePhoneCaseCommand : IRequest<Result<PhoneCaseResponse>>
{
    public string Name { get; set; } = null!;
    public PhoneModelTypeDto Model { get; set; }
    public string ImgUrl { get; set; } = null!;
    public string Color { get; set; } = null!;
    public int Price { get; set; }
    public short Stock { get; set; }
}
