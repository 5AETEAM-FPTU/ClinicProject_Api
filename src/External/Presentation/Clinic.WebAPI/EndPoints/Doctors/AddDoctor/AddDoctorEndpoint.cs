using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.AddDoctor;
using Clinic.Application.Features.Doctors.AddDoctor;
using Clinic.WebAPI.EndPoints.Doctors.AddDoctor.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Doctors.UpdateDoctorDescription.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.AddDoctor;

public class AddDoctorEndpoint : Endpoint<AddDoctorRequest, AddDoctorHttpResponse>
{
    public override void Configure()
    {
        Post("doctor/adding");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to add Doctor achievement";
            summary.Description = "This endpoint allows admin for adding doctor purpose.";
            summary.Response<UpdateDoctorDescriptionHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = AddDoctorResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<AddDoctorHttpResponse> ExecuteAsync(
        AddDoctorRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = AddDoctorHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(httpResponse, httpResponseStatusCode, ct);

        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
