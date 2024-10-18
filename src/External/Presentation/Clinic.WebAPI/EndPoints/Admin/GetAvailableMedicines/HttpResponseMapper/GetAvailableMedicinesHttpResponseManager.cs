using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.GetAvailableMedicines;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetAvailableMedicines.HttpResponseMapper;

public class GetAvailableMedicinesHttpResponseManager
{
    private readonly Dictionary<
        GetAvailableMedicinesResponseStatusCode,
        Func<
            GetAvailableMedicinesRequest,
            GetAvailableMedicinesResponse,
            GetAvailableMedicinesHttpResponse
        >
    > _dictionary;

    internal GetAvailableMedicinesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAvailableMedicinesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
    }

    internal Func<
        GetAvailableMedicinesRequest,
        GetAvailableMedicinesResponse,
        GetAvailableMedicinesHttpResponse
    > Resolve(GetAvailableMedicinesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
