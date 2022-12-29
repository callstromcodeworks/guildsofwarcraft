/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using Newtonsoft.Json;

namespace CCW.GoW.DataService;
public class ApplicationConfig
{
    private ApplicationConfig() { }
    public static ApplicationConfig GetConfig()
    {
        ApplicationConfig? config;
        try
        {
            config = JsonConvert.DeserializeObject<ApplicationConfig>(File.ReadAllText($"config.json"));
        }
        catch (FileNotFoundException)
        {
            config = new ApplicationConfig();
        }
        if (config == null) throw new NullReferenceException(nameof(config));
        return config;
    }
    public string DiscordToken { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;

#if DEBUG
    public string TempAuthCode { get; set; } = string.Empty;
    public string TempAuthToken { get; set; } = string.Empty;
    public string TempGenericToken { get; set; } = string.Empty;
#endif
}
