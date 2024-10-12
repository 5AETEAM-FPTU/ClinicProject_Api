using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Clinic.Application.Commons.CallToken;
using Clinic.Configuration.Infrastructure.Stringee;
using Microsoft.IdentityModel.Tokens;

namespace Clinic.Stringee.Handler;

/// <summary>
///      Implementation of ICallTokenHandler.
/// </summary>
internal class SrtringeeAccessTokenHandler : ICallTokenHandler
{
    private readonly StringeeOption _stringeeOption;

    public SrtringeeAccessTokenHandler(StringeeOption stringeeOption)
    {
        _stringeeOption = stringeeOption;
    }

    public string GenerateAccessToken(string userId)
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var exp = now + 3600*24;

        var header = new JwtHeader(
            new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_stringeeOption.SecretKey)),
                SecurityAlgorithms.HmacSha256
            )
        );

        header["cty"] = _stringeeOption.Cty;

        var payload = new JwtPayload
        {
            { "jti", $"{_stringeeOption.SID}-{now}" },
            { "iss", _stringeeOption.SID },
            { "exp", exp },
            { "userId", userId }
        };

        var jwt = new JwtSecurityToken(header, payload);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(jwt);

        return tokenString;
    }
}
