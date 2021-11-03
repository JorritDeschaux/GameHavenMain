using GameHavenMain.Models;

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

    public static class Endpoints
    {
        public const string AgeRating = "age_ratings";
        public const string AgeRatingContentDescriptions = "age_rating_content_descriptions";
        public const string AlternativeNames = "alternative_names";
        public const string Artworks = "artworks";
        public const string Characters = "characters";
        public const string CharacterMugShots = "character_mug_shots";
        public const string Collections = "collections";
        public const string Companies = "companies";
        public const string CompanyWebsites = "company_websites";
        public const string Covers = "covers";
        public const string ExternalGames = "external_games";
        public const string Franchies = "franchises";
        public const string Games = "games";
        public const string GameEngines = "game_engines";
        public const string GameEngineLogos = "game_engine_logos";
        public const string GameVersions = "game_versions";
        public const string GameModes = "game_modes";
        public const string GameVersionFeatures = "game_version_features";
        public const string GameVersionFeatureValues = "game_version_feature_values";
        public const string GameVideos = "game_videos";
        public const string Genres = "genres";
        public const string InvolvedCompanies = "involved_companies";
        public const string Keywords = "keywords";
        public const string MultiplayerModes = "multiplayer_modes";
        public const string Platforms = "platforms";
        public const string PlatformFamilies = "platform_families";
        public const string PlatformLogos = "platform_logos";
        public const string PlatformVersions = "platform_versions";
        public const string PlatformVersionCompanies = "platform_version_companies";
        public const string PlatformVersionReleaseDates = "platform_version_release_dates";
        public const string PlatformWebsites = "platform_websites";
        public const string PlayerPerspectives = "player_perspectives";
        public const string ReleaseDates = "release_dates";
        public const string Screenshots = "screenshots";
        public const string Search = "search";
        public const string Themes = "themes";
        public const string Websites = "websites";
    }
}
