/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.BlizzardApi
{
    public class TokenResponse
    {
#pragma warning disable IDE1006 // Naming Styles
        public string access_token { get; set; } = string.Empty;
        public string token_type { get; set; } = string.Empty;
        public string expires_in { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
#pragma warning restore IDE1006 // Naming Styles
    }
}
