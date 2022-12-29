/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using CCW.GoW.BlizzardApi;
using System.Text;

namespace CCW.GoW
{
    public partial class DebugTools : Form
    {
        public DebugTools()
        {
            InitializeComponent();
        }

        private void GetAuthCodeButton_Click(object sender, EventArgs e)
        {
            Form browserWindow = new()
            {
                Width = 1024,
                Height = 768,
                StartPosition = FormStartPosition.CenterScreen
            };
            WebBrowser browser = new()
            {
                Parent = browserWindow,
                Top = browserWindow.Top,
                Left = browserWindow.Left,
                Width = browserWindow.Width,
                Height = browserWindow.Height,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ScriptErrorsSuppressed = true

            };
            browser.Navigated += (sender, e) =>
            {
                if (e.Url == null) return;
                if (e.Url.Host.Contains("localhost"))
                {
                    var code = e.Url.Query.Split("code=")[1].Split('&')[0];
                    authCodeTextBox.Text = code;
                    browserWindow.Close();
                }
                browserWindow.Text = e.Url.ToString();
            };
            browser.Navigate(BlizzApiHandler.GetAuthorizationUri());
            browserWindow.ShowDialog();
        }

        private void GetTokenButton_Click(object sender, EventArgs e)
        {
            if (authCodeTextBox.Text.Length == 0) return;
            var code = authCodeTextBox.Text;
            Task.Run(() =>
            {
                var resp = BlizzApiHandler.GetUserToken(code, BlizzApiHandler.Queries.ScopeValue);
                if (resp != null && resp.Result != null) updateResult(resp.Result.access_token);
            });

            void updateResult(string value)
            {
                if (tokenTextBox.InvokeRequired) tokenTextBox.Invoke(delegate { updateResult(value); });
                else tokenTextBox.Text = value;
            }
        }

        private void GetCliCredTokenButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var resp = BlizzApiHandler.GetToken();
                if (resp != null && resp.Result != null) updateResult(resp.Result.access_token);
            });

            void updateResult(string value)
            {
                if (clientCredTokenTextBox.InvokeRequired) clientCredTokenTextBox.Invoke(delegate { updateResult(value); });
                else clientCredTokenTextBox.Text = value;
            }
        }

        private void SendRequestButton_Click(object sender, EventArgs e)
        {
            if (tokenTextBox.Text == null || 
                tokenTextBox.Text == string.Empty || 
                clientCredTokenTextBox.Text == null || 
                clientCredTokenTextBox.Text == string.Empty) return;
            Task.Run(() =>
            {
                // var resp = BlizzApiHandler.ApiRequest<GuildApiResponse>(BlizzApiHandler.Region.US, tokenTextBox.Text, "tichondrius/diabolic");
                var resp = BlizzApiHandler.ApiRequest<GuildActivityApiResponse>(BlizzApiHandler.Region.US, tokenTextBox.Text, "tichondrius/diabolic/activity");
                var api = resp.Result;
                StringBuilder sb = new StringBuilder()
                .AppendLine("Links -> Self -> href")
                .AppendLine(api._links.Self.Href)
                .AppendLine("Guild")
                .AppendLine($"Key -> href {api.Guild.Key.Href}")
                .AppendLine($"Id {api.Guild.Id}")
                .AppendLine("Realm")
                .AppendLine($"Key -> href {api.Guild.Realm.Key.Href}")
                .AppendLine($"Name {api.Guild.Realm.Name}")
                .AppendLine($"Id {api.Guild.Realm.Id}")
                .AppendLine($"Slug {api.Guild.Realm.Slug}")
                .AppendLine("Faction")
                .AppendLine($"Type {api.Guild.Faction.Type}")
                .AppendLine($"Name {api.Guild.Faction.Name}");
                foreach (var characterAchievement in api.Activities)
                {
                    sb.AppendLine("Character Achievement")
                    .AppendLine("Character")
                    .AppendLine($"Key -> href {characterAchievement.Character.Key.Href}")
                    .AppendLine($"Name {characterAchievement.Character.Name}")
                    .AppendLine($"Id {characterAchievement.Character.Id}")
                    .AppendLine("Realm")
                    .AppendLine($"Key -> href {characterAchievement.Character.Realm.Key.Href}")
                    .AppendLine($"Name {characterAchievement.Character.Realm.Name}")
                    .AppendLine($"Id {characterAchievement.Character.Realm.Id}")
                    .AppendLine($"Slug {characterAchievement.Character.Realm.Slug}")
                    .AppendLine("Achievement")
                    .AppendLine($"Key -> href {characterAchievement.Achievement.Key.Href}")
                    .AppendLine($"Name {characterAchievement.Achievement.Name}")
                    .AppendLine($"Id {characterAchievement.Achievement.Id}")
                    .AppendLine($"Activity -> Type {characterAchievement.Activity.Type}")
                    .AppendLine($"Timestamp {characterAchievement.Timestamp}");
                }
                updateResult(sb.ToString());

                /*
                 * 
                .AppendLine("Links -> Self -> href")
                .AppendLine(api._links.Self.href)
                .AppendLine($"Id {api.Id}")
                .AppendLine($"Name {api.Name}")
                .AppendLine("Faction")
                .AppendLine($"Type {api.Faction.type}")
                .AppendLine($"Name {api.Faction.name}")
                .AppendLine("Realm")
                .AppendLine("Key")
                .AppendLine($"href {api.Realm.Key.href}")
                .AppendLine($"Name {api.Realm.Name}")
                .AppendLine($"Id {api.Realm.Id}")
                .AppendLine($"Slug {api.Realm.Slug}")
                .AppendLine("Roster")
                .AppendLine($"href {api.Roster.href}");
                 * 
                 */
            });

            void updateResult(string value)
            {
                if (queryResultsTextBox.InvokeRequired) queryResultsTextBox.Invoke(delegate { updateResult(value); });
                else queryResultsTextBox.Text = value;
            }
        }
    }
}
