using Google.Apis.Auth;

namespace ApiProject.Application.Abstractions.Authentication;
public interface IGoogleAuthService
{
    string GetGoogleAuthUrl();
    Task<Result<GoogleJsonWebSignature.Payload>> ExchangeCodeForToken(string code);
}
