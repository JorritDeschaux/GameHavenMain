using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameHavenMain.Data.HelperClasses
{
	public static class TokenHelper
	{

		private const string SECRET_KEY = "%tw2sm_rj+ef504a@6lx5dg3g^%ozvjk664a!(r5vu2hk4b7wd^&%&^%&^hjsdfb2%%%";
		public static readonly SymmetricSecurityKey SIGNING_KEY = new
			SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

		public static JwtSecurityToken CreateToken(Claim[] claims)
		{
			var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

			return token;
		}

		public static string WriteToken(JwtSecurityToken token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(token);
		}

		public static JwtSecurityToken Verify(string jwt)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(SECRET_KEY);

			tokenHandler.ValidateToken(jwt, new TokenValidationParameters
			{
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuerSigningKey = true,
				ValidateIssuer = false,
				ValidateAudience = false

			}, out SecurityToken validatedToken);

			return (JwtSecurityToken)validatedToken;
		}



	}
}
