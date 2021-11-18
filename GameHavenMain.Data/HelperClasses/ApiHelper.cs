using IGDB;

namespace GameHavenMain.Data.HelperClasses
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
