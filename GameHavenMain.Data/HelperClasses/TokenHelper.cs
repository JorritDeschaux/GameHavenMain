using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameHavenMain.Data.HelperClasses
{
	public class TokenHelper
	{

		private readonly string SECRET_KEY;
		private readonly SymmetricSecurityKey SIGNING_KEY;

		IConfiguration _config;

		public TokenHelper(string secretKey)
		{
			SECRET_KEY = secretKey;
			SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		}

		public TokenHelper(IConfiguration config)
		{
			_config = config;
			SECRET_KEY = _config["Secret"];
			SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
		}

		public JwtSecurityToken CreateToken(Claim[] claims, DateTime? expirationDate = null)
		{
			var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

			var tokenHandler = new JwtSecurityTokenHandler();

			if (expirationDate == null)
				expirationDate = DateTime.UtcNow.AddDays(30);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = expirationDate,
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

			return token;
		}


		public string WriteToken(JwtSecurityToken token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(token);
		}


		public JwtSecurityToken Validate(string jwt)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			tokenHandler.ValidateToken(jwt, new TokenValidationParameters
			{
				IssuerSigningKey = SIGNING_KEY,
				ValidateIssuerSigningKey = true,
				ValidateIssuer = false,
				ValidateAudience = false

			}, out SecurityToken validatedToken);

			return (JwtSecurityToken)validatedToken;
		}

		public bool IsExpired(JwtSecurityToken validatedToken)
		{
			if(DateTime.UtcNow > validatedToken.ValidTo)
				return true;

			return false;
		}

		public bool Authorized(JwtSecurityToken validatedToken, int id)
		{
			return validatedToken.Payload["nameid"].ToString() == Convert.ToString(id) ? true : false;
		}

	}
}
