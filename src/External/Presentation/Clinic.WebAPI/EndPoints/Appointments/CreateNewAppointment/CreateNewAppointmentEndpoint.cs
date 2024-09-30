using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Appointments.CreateNewAppointment;
using Clinic.WebAPI.EndPoints.Appointments.CreateNewAppointment.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.CreateNewAppointment;

public class CreateNewAppointmentEndpoint
    : Endpoint<CreateNewAppointmentRequest, CreateNewAppointmentHttpResponse>
{
    public override void Configure()
    {
        Post("appointment/create");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(sumary =>
        {
            sumary.Summary = "Create new appointment endpoint";
            sumary.Description =
                "Create new appointment endpoint for user create new appointment after successful payment.";
            sumary.Response<CreateNewAppointmentHttpResponse>(
                description: "Represents a successful response",
                example: new()
                {
                    AppCode = CreateNewAppointmentResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    HttpCode = StatusCodes.Status200OK,
                }
            );
        });
    }

    public override async Task<CreateNewAppointmentHttpResponse> ExecuteAsync(
        CreateNewAppointmentRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct);

        var httpResponse = CreateNewAppointmentHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct
        );

        httpResponse.HttpCode = httpResponseStatusCode;
        return httpResponse;
    }
}
