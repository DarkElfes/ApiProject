using ApiProject.Shared.Cases.Request;
using ApiProject.Shared.Cases.Response;
using FluentResults;
using System.Net.Http.Json;

namespace ApiProject.Client.Web.Features.PhoneCases;

public class PhoneCaseService(
    HttpClient _httpClient)
{
    public async Task<Result<PhoneCaseResponse?>> CreateAsync(CreatePhoneCaseCommand request)
        => await HandleResponse<PhoneCaseResponse?>(_httpClient.PostAsJsonAsync("create", request));

    public async Task<Result<PhoneCaseResponse[]?>> GetPage(short pageNumber)
        => await HandleResponse<PhoneCaseResponse[]?>(_httpClient.GetAsync($"page/{pageNumber}"));


    private static async Task<Result<T?>> HandleResponse<T>(Task<HttpResponseMessage> request)
    {
        try
        {
            var response = await request;

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
                return Result.Fail(problemDetails?["detail"]?.ToString() ?? "Unhandled error");
            }

            return Result.Ok(await response.Content.ReadFromJsonAsync<T>());
        }
        catch (HttpRequestException ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
