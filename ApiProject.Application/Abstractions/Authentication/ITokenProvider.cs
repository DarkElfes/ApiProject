using ApiProject.Domain.Users;

namespace ApiProject.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}