using ApiProject.Shared.Abstractions.Result;

namespace ApiProject.Application.Features.Users;

public static class UserErrors
{
    public static class Register
    {
        public static readonly Error AlreadyExist = new("User.Register.AlreadyExist", ErrorType.Conflict, "User with this email already exist.");
    }

    public static class Login
    {
        public static readonly Error NotFoundByEmail = new("User.Login.NotFoundByEmail", ErrorType.NotFound, "User with this email not found.");
        public static readonly Error IncorrectPassword = new("User.Login.IncorrectPassword", ErrorType.Validation, "Incorrect password.");
    }

    public static class Update
    {
        public static readonly Error NotFoundById = new("User.Update.NotFoundById", ErrorType.NotFound, "User with this identifier not found");
    }
}
