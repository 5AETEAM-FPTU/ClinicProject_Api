using Clinic.Application.Features.Admin.GetStaticInformation;
using Clinic.WebAPI.EndPoints.Admin.GetStaticInformation.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.WebAPI.EndPoints.Admin.GetStaticInformation
{
    public sealed class GetStaticInformationEndpoint : Endpoint<EmptyRequest, GetStaticInformationHttpResponse>
    {
        public override void Configure()
        {
            Get(routePatterns: "admin/static-information");
            AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
            DontThrowIfValidationFails();
            Description(builder: builder =>
            {
                builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
            });
            Summary(endpointSummary: summary =>
            {
                summary.Summary = "Endpoint for Admin feature";
                summary.Description = "This endpoint is used for getting all static information of system.";
                summary.Response<GetStaticInformationHttpResponse>(
                    description: "Represent successful operation response.",
                    example: new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = GetStaticInformationResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    }
                );
            });
        }

        public override async Task<GetStaticInformationHttpResponse> ExecuteAsync(
            EmptyRequest req,
            CancellationToken ct
        )
        {
            var reqq = new GetStaticInformationRequest();
            var appResponse = await reqq.ExecuteAsync(ct: ct);

            var httpResponse = GetStaticInformationHttpResponseMapper
                .Get()
                .Resolve(statusCode: appResponse.StatusCode)
                .Invoke(arg1: reqq, arg2: appResponse);

            var httpResponseStatusCode = httpResponse.HttpCode;
            httpResponse.HttpCode = default;

            await SendAsync(httpResponse, httpResponseStatusCode, ct);

            httpResponse.HttpCode = httpResponseStatusCode;

            return httpResponse;
        }
    }
}
