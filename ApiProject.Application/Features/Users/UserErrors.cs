
namespace ApiProject.Application.Features.Users;

public static class UserErrors
{
    public static readonly Error IncorrectPassword = new("User.IncorrectPassword", ErrorType.Validation, "Incorrect password.");
    public static readonly Error NotFoundById = new("User.NotFoundById", ErrorType.NotFound, "User with this identifier not found");


    public static class Register
    {
        public static readonly Error AlreadyExist = new("User.Register.AlreadyExist", ErrorType.Conflict, "User with this email already exist.");
    }

    public static class Login
    {
        public static readonly Error NotFoundByEmail = new("User.Login.NotFoundByEmail", ErrorType.NotFound, "User with this email not found.");
        public static readonly Error InvalidGrant = new("User.Login.ByGoogle.InvalidGrant", ErrorType.Problem, "Auth code is invalid or has an invalid format.");
        public static readonly Error InvalidRequest = new("User.Login.ByGoogle.InvalidRequest", ErrorType.Problem, "Auth code is missing.");
        public static readonly Error PasswordNotSet = new("User.Login.ByGoogle.PasswordNotSet", ErrorType.Problem, "Password is not set for this user.");
    }

    public static class Update
    {
        public static readonly Error SamePassword = new("User.Update.SamePassword", ErrorType.Problem, "The new password cannot be same as the old one.");
        public static readonly Error SameUsername = new("User.Update.SameUsername", ErrorType.Problem, "The new username cannot be same as the old one.");
    }
}
