using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.CallToken;
using Clinic.Application.Features.Users.UpdateUserPrivateInfo;
using FastEndpoints;

namespace Clinic.WebAPI.EndPoints.Doctors.Hell;

public class TestEndpoint : Endpoint<TestRequest, TestResponse>
{
    private readonly ICallTokenHandler _callTokenHandler;

    public TestEndpoint(ICallTokenHandler callTokenHandler)
    {
        _callTokenHandler = callTokenHandler;
    }

    public override void Configure()
    {
        Get("test/generate/stringee");
        AllowAnonymous();
    }

    public override async Task<TestResponse> ExecuteAsync(TestRequest test, CancellationToken ct)
    {
        var id = test.UserId.ToString();

        await SendAsync(
            new TestResponse { Token = _callTokenHandler.GenerateAccessToken(id) },
            statusCode: 200,
            cancellation: ct
        );

        return new TestResponse { Token = _callTokenHandler.GenerateAccessToken(id) };
    }
}
