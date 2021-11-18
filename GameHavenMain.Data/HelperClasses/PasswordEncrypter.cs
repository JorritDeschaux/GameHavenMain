using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.HelperClasses
{
	public static class PasswordEncrypter
	{

		private const string SECRET_KEY = "0N?;g}|Y`l0FkbgS5@IIKxlhfcie@Qh>$J[b5X<lTJzFnMjkEC:m#k4FwCb5g";

		public static string EncryptPassword(string password)
		{

			string input = password + SECRET_KEY;

			MD5 md5Hasher = MD5.Create();

			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

			StringBuilder sBuilder = new StringBuilder(); 

			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();

		}

	}
}
