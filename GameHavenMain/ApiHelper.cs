using GameHavenMain.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain
{
	public static class ApiHelper
	{
		private static readonly string clientId = "vl33o1v2tovnccfy3t2woim9l3yy9l";
		private static readonly string accessToken = "1vxvuwih70s6qd3euo7qikm70l1oy2";

		public static async Task<IRestResponse> AuthorizedAPIRequest(IRestClient client, IRestRequest request)
		{
			request.Method = Method.POST;
			request.AddHeader("Client-ID", clientId);
			request.AddHeader("Authorization", "Bearer " + accessToken);
			request.AddHeader("Access-Control-Allow-Origin", "*");

			var data = await client.ExecuteAsync(request);

			return data;
		}

	}
}
