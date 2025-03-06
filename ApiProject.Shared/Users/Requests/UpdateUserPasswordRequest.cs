namespace ApiProject.Shared.Users.Requests;
public record UpdateUserPasswordRequest(string OldPassword, string NewPassword);
