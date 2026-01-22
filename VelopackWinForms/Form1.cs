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
                var source = new GithubSource("https://github.com/jxin-dev/VelopackWinForms", null, false);
                var mgr = new UpdateManager(source);

                // 1. Check for new version 
                var newVersion = await mgr.CheckForUpdatesAsync();
                if (newVersion == null)
                {
                    MessageBox.Show("Application is up-to-date.","Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; 
                }

                // 2. Download the update 
                await mgr.DownloadUpdatesAsync(newVersion);

                // 3. Install and restart 
                mgr.ApplyUpdatesAndRestart(newVersion);
            }
            catch (Velopack.Exceptions.NotInstalledException)
            {
                MessageBox.Show("Debug mode – updates disabled.", "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to access the update server.\nPlease check your internet connection or try again later.", "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
    }
}
