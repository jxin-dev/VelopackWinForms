using Velopack;
using Velopack.Sources;

namespace VelopackWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnUpdateApp_Click(object sender, EventArgs e)
        {
            await UpdateMyApp();
        }

        private async Task UpdateMyApp()
        {
            try
            {
                // Point to your GitHub Repository 
                var source = new GithubSource("https://github.com/jxin-dev/SampleAppWithVelopackAutoUpdater", null, false);
                var mgr = new UpdateManager(source);

                // 1. Check for new version 
                var newVersion = await mgr.CheckForUpdatesAsync();
                if (newVersion == null) return;

                // 2. Download the update 
                await mgr.DownloadUpdatesAsync(newVersion);

                // 3. Install and restart 
                mgr.ApplyUpdatesAndRestart(newVersion);
            }
            catch (Velopack.Exceptions.NotInstalledException)
            {
                Text += " (Debug mode – updates disabled)";
            }
            catch (Exception ex)
            {
                Text += ex.Message;
            }

           
        }
    }
}
