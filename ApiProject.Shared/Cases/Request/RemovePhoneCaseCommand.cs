using ApiProject.Shared.Abstractions.Result;
using MediatR;

namespace ApiProject.Shared.Cases.Request;
public record RemovePhoneCaseCommand(int PhoneCaseId) : IRequest<Result>;
