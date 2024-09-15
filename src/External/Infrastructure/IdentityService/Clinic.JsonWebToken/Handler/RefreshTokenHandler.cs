using System.Security.Cryptography;
using System.Text;
using Clinic.Application.Commons.Token.RefreshToken;
using Clinic.Domain.Commons.Entities;

namespace Clinic.JsonWebToken.Handler;

/// <summary>
///     Implementation refresh token generator interface.
/// </summary>
internal sealed class RefreshTokenHandler : IRefreshTokenHandler
{
    public string Generate(int length)
    {
        const string Chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*+=";

        if (length < RefreshToken.MetaData.RefreshTokenValue.MinLength)
        {
            return string.Empty;
        }

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
