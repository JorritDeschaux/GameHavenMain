using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SimpleCrypto;
using GameHavenMain.Data.DTO;

namespace GameHavenMain.Data.HelperClasses
{
	public static class PasswordEncrypter
	{

		public static UserDTO EncryptUserPassword(UserDTO user, string password)
		{
			ICryptoService cryptoService = new PBKDF2();

			var hash = cryptoService.Compute(password);

			user.Password = hash;
			user.Salt = cryptoService.Salt;

			return user;
		}

		public static string EncryptPasswordWithGivenSalt(string password, string salt)
		{
			ICryptoService cryptoService = new PBKDF2();

			var hash = cryptoService.Compute(password, salt);

			return hash;
		}

	}
}
