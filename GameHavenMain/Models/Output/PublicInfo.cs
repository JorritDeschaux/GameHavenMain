using GameHavenMain.Data.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameHavenMain.Models.Output
{
    public class PublicInfo
    {
		public PublicInfo()
		{

		}

		public PublicInfo(UserDTO user)
		{
			Id = user.Id;
			Username = user.Username;
			Birthday = user.Birthday;
			RegisterDate = user.RegisterDate;
		}

		public int Id { get; set; }

		public string Username { get; set; }

		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		public DateTime RegisterDate { get; set; }
	}
}
