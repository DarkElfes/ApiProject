using ApiProject.Shared.Cases.Request;
using ApiProject.Shared.Cases.Response;
using FluentResults;
using System.Net.Http.Json;

namespace ApiProject.Client.Web.PhoneCases;

public class PhoneCaseService(
    HttpClient _httpClient)
{
    public async Task<Result<PhoneCaseResponse?>> CreateAsync(CreatePhoneCaseCommand request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("create", request);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
                return Result.Fail(problemDetails?["detail"]?.ToString() ?? "Unhandled error");
            }

            var phoneCaseRespone = await response.Content.ReadFromJsonAsync<PhoneCaseResponse>();
            return Result.Ok(phoneCaseRespone);

        }
        catch (Exception ex)
        {
            throw; 
        }
    }

    public async Task<Result<PhoneCaseResponse[]?>> GetPage(short pageNumber)
    {
        var response = await _httpClient.GetAsync($"page/{pageNumber}");

        if (response.IsSuccessStatusCode)
        {
            var phoneCasesArr = await response.Content.ReadFromJsonAsync<PhoneCaseResponse[]>();
            return Result.Ok(phoneCasesArr);
        }
        return Result.Fail("Some trouble");
    }
}
