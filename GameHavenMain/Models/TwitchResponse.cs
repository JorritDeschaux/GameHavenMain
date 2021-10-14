using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Models
{
	public class TwitchResponse
	{
		public string Access_Token { get; set; }
		public int Expires_In { get; set; }
		public string Token_Type { get; set; }
	}
}
