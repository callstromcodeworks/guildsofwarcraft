/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.Services;

//TODO add localized strings for other languages

public enum Lang
{
    ENUS
}
internal static class Localizer
{

    private static readonly Dictionary<string, string> TableENUS = new()
    {
        {"players", "players"},
        {"", ""}
    };

    public static string GetLocalizedString(Lang lang, string key)
    {
        var result = lang switch
        {
            Lang.ENUS => TableENUS[key],
            _ => throw new Exception()
        };
        return result;
    }
}
