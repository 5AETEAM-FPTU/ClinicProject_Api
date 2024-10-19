using System.Collections.Generic;

namespace Clinic.Configuration.Presentation.Authentication;

/// summary
///     The BaseEndpointUrl class is used to hold various jwt authentication configuration settings.
/// summary
public class BaseEndpointUrlOption
{
    public string Api { get; set; }

    public string Client { get; set; }

    public string Admin { get; set; }
}
