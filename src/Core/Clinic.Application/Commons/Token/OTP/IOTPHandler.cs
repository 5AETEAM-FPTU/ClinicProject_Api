namespace Clinic.Application.Commons.Token.OTP;

/// <summary>
/// Interface for Otp Handler
/// </summary>
public interface IOTPHandler
{
    /// <summary>
    ///  Generate Otp
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    string Generate(int length);
}
