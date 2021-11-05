using GameHavenMain.Models;
using IGDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GameHavenMain.Controllers.GamesController;

namespace GameHavenMain
{
	public static class ApiHelper
	{
		private static readonly string clientId = "ptz8ma4spnaia96n78yerskcg7pyyr";
		private static readonly string secretKey = "g1gqnneu4bdwvr46fqx2x7nc84a129";

		public static IGDBClient CreateClient()
		{
			return new IGDBClient
			(
				clientId,
				secretKey
			);
		}
	}
}
