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
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.    
        var httpResponse = AddDoctorHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);
        // Store the real http code of http response into a temporary variable.
        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;
        // Send http response to client.
        await SendAsync(httpResponse, httpResponseStatusCode, ct);
         // The http code of http response will be stored into a temporary variable.
        httpResponse.HttpCode = httpResponseStatusCode;
        // Set the http code of http response back to real one.
        return httpResponse;
    }
}
