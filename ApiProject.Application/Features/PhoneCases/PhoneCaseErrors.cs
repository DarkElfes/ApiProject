namespace ApiProject.Application.Features.PhoneCases;

public static class PhoneCaseErrors
{
    public static readonly Error NotFound = new("PhoneCase.NotFound", ErrorType.NotFound, "The phone case with the specified ID was not found");

}
