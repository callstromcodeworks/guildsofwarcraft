/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using CCW.GoW;
using CCW.GoW.Extentions;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace CCW.GoW.BlizzardApi;

public class BlizzApiHandler
{
    static ApplicationConfig AppConfig = ApplicationConfig.GetConfig();

    public enum Region
    {
        US,
        EU
    }
    private readonly struct OauthUri
    {
        public static readonly string Auth = "https://oauth.battle.net/authorize";
        public static readonly string Token = "https://oauth.battle.net/token";
    }
    private readonly struct ApiUri
    {
        public static readonly string Api = ".api.blizzard.com";

        public static readonly string Data = "/data/wow";
        public static readonly string Profile = "/profile/user/wow";
        public static readonly string Guild = "/data/wow/guild/";
        public static string GetRegionedApi(Region r) => r switch
        {
            Region.US => $"https://us{Api}",
            Region.EU => $"https://eu{Api}",
            _ => throw new NotImplementedException()
        };
    }
    public readonly struct Queries
    {
        public static readonly string ClieIdName = "client_id";
        public static readonly string RedirName = "redirect_uri";
        public static readonly string ScopeName = "scope";
        public static readonly string StateName = "state";
        public static readonly string RespTyName = "response_type";

        public static readonly string RedirValue = "https%3A%2F%2Flocalhost";
        public static readonly string ScopeValue = "wow.profile";
        public static readonly string RespTyValue = "code";

        public static readonly string GranTyName = "grant_type";
        public static readonly string ClieCreValue = "client_credentials";
        public static readonly string AuthCoValue = "authorization_code";

        public static readonly string AcceToValue = "access_token";
        public static readonly string NamesName = "namespace";
        public static readonly string RegionName = "region";
        public static readonly string LocaleName = "locale";

        public static readonly string NamesPart = "profile-";

        public static string GetRegion(Region r) => r switch
        {
            Region.US => $"us",
            Region.EU => $"eu",
            _ => throw new NotImplementedException()
        };

        public static string GetLocale(Region r) => r switch
        {
            Region.US => $"en_US",
            Region.EU => "en_GB",
            _ => throw new NotImplementedException()
        };
    }

    public static Task GetAuthorization()
    {
        Uri uri = new UriBuilder(OauthUri.Auth)
            .AddQuery(Queries.ClieIdName, AppConfig.ClientId)
            .AddQuery(Queries.ScopeName, Queries.ScopeValue)
            .AddQuery(Queries.StateName, GetStateBlob())
            .AddQuery(Queries.RedirName, Queries.RedirValue)
            .AddQuery(Queries.RespTyName, Queries.RespTyValue)
            .Uri;
        Process.Start(new ProcessStartInfo(uri.ToString()) { UseShellExecute = true });
        return Task.CompletedTask;
    }
    public static Uri GetAuthorizationUri()
    {
        return new UriBuilder(OauthUri.Auth)
            .AddQuery(Queries.ClieIdName, AppConfig.ClientId)
            .AddQuery(Queries.ScopeName, Queries.ScopeValue)
            .AddQuery(Queries.StateName, GetStateBlob())
            .AddQuery(Queries.RedirName, Queries.RedirValue)
            .AddQuery(Queries.RespTyName, Queries.RespTyValue)
            .Uri;
    }

    public static async Task<TokenResponse> GetUserToken(string auth_code, string scope)
    {
        string auth = $"{AppConfig.ClientId}:{AppConfig.ClientSecret}";
        Uri uri = new UriBuilder(OauthUri.Token)
            .AddQuery(Queries.GranTyName, Queries.AuthCoValue)
            .AddQuery(Queries.RespTyValue, auth_code)
            .AddQuery(Queries.ScopeName, scope)
            .AddQuery(Queries.RedirName, Queries.RedirValue)
            .Uri;
        using HttpClient client = new();
        HttpRequestMessage tokenRequest = new(HttpMethod.Post, uri);
        tokenRequest.Headers.Authorization = new("Basic", auth.ToBase64());
        CancellationToken cancel = new();
        cancel.ThrowIfCancellationRequested();
        try
        {
            var response = await client.SendAsync(tokenRequest, HttpCompletionOption.ResponseContentRead, cancel);
            var body = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    TokenResponse? token = JsonConvert.DeserializeObject<TokenResponse>(body);
                    if (token == null) throw new JsonSerializationException(body);
                    return token;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                default:
                    throw new Exception($"{response.StatusCode}");
            }

        }
        catch (HttpRequestException ex)
        {
            throw ex;
        }
        catch (OperationCanceledException ex2)
        {
            throw ex2;
        }
        throw new Exception();
    }

    public static async Task<TokenResponse> GetToken()
    {
        string auth = $"{AppConfig.ClientId}:{AppConfig.ClientSecret}";
        Uri uri = new UriBuilder(OauthUri.Token)
            .AddQuery(Queries.GranTyName, Queries.ClieCreValue)
            .Uri;
        using HttpClient client = new();
        HttpRequestMessage tokenRequest = new(HttpMethod.Post, uri);
        tokenRequest.Headers.Authorization = new("Basic", auth.ToBase64());
        CancellationToken cancel = new();
        cancel.ThrowIfCancellationRequested();
        try
        {
            var response = await client.SendAsync(tokenRequest, HttpCompletionOption.ResponseContentRead, cancel);
            var body = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    TokenResponse? token = JsonConvert.DeserializeObject<TokenResponse>(body);
                    if (token == null) throw new JsonSerializationException(body);
                    return token;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                default:
                    throw new Exception($"{response.StatusCode}");
            }

        }
        catch (HttpRequestException ex)
        {
            throw ex;
        }
        catch (OperationCanceledException ex2)
        {
            throw ex2;
        }

    }
    public static async Task<T> ApiRequest<T>(Region region, string accessToken, string what) where T : ApiResponseBase
    {
        using HttpClient client = new();
        string part = typeof(T) switch
        {
            Type guild when guild == typeof(GuildApiResponse) => "/data/wow/guild/",
            Type guildActivity when guildActivity == typeof(GuildActivityApiResponse) => "/data/wow/guild/",
            _ => ""
        }; ;
        Uri requestUri = new UriBuilder($"{ApiUri.GetRegionedApi(region)}{part}{what}")
            .AddQuery(Queries.AcceToValue, accessToken)
            .AddQuery(Queries.NamesName,
            $"{Queries.NamesPart}{Queries.GetRegion(region)}")
            //            .AddQuery(Queries.RegionName, Queries.GetRegion(region))
            .AddQuery(Queries.LocaleName, Queries.GetLocale(region))
            .Uri;
        HttpRequestMessage apiRequest = new(HttpMethod.Get, requestUri);
        apiRequest.Headers.Add(Queries.RedirName, Queries.RedirValue);
        apiRequest.Headers.Authorization = new("Bearer", accessToken);
        CancellationToken cancel = new();
        cancel.ThrowIfCancellationRequested();
        try
        {
            var response = await client.SendAsync(apiRequest, HttpCompletionOption.ResponseContentRead, cancel);
            var body = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    T? result = JsonConvert.DeserializeObject<T>(body);
                    if (result == null) throw new JsonSerializationException(body);
                    result.Raw = body;
                    return result;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                default:
                    throw new Exception($"{response.StatusCode}");
            }

        }
        catch (HttpRequestException ex)
        {
            throw ex;
        }
        catch (OperationCanceledException ex2)
        {
            throw ex2;
        }
        throw new Exception();
    }

    internal static string GetStateBlob()
    {
        StringBuilder sb = new();
        sb.Append(DateTime.UtcNow.ToString());
        sb.Append(Environment.TickCount64);
        sb.Append(AppConfig.ClientId);
        return sb.ToString().GetHashCode().ToString();

    }
}
