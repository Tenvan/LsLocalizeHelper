using System.Windows;
using System.Windows.Forms;

using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views.Settings
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public SettingsDialog()
        {
            InitializeComponent();
            this.TextBoxModsPath.Text = SettingsManager.Settings?.ModsPath;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            SettingsManager.Settings!.ModsPath = this.TextBoxModsPath.Text;
            SettingsManager.Save();
            this.DialogResult                 = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonBrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.TextBoxModsPath.Text = dialog.SelectedPath;
            }
        }
    }
}
