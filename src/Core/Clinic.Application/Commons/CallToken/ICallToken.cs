namespace Clinic.Application.Commons.CallToken;

/// <summary>
///     Interface handler for generating access token used in video call.
/// </summary>
public interface ICallTokenHandler
{
    /// <summary>
    ///     Generate access token.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    string GenerateAccessToken(string userId);
}
