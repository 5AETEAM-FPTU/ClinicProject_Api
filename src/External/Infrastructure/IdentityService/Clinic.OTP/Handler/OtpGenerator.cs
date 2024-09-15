using System.Security.Cryptography;
using System.Text;
using Clinic.Application.Commons.Token.OTP;

namespace Clinic.OTP.Handler;

/// <summary>
///     OTP Generator
/// </summary>
internal sealed class OtpGenerator : IOTPHandler
{
    public string Generate(int length)
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        StringBuilder builder = new();

        for (int time = default; time < length; time++)
        {
            builder.Append(
                value: Chars[index: RandomNumberGenerator.GetInt32(toExclusive: Chars.Length)]
            );
        }

        return builder.ToString();
    }
}
