using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
    
        public Settings Settings = null;
        public SettingsWindow()
        {
            InitializeComponent();
        }
        public void SetFeatures()
        {
            //this.Features = features;
            //this.Settings = features.Settings;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CB_CheckValidationCells.IsChecked = Settings.CheckValidationCells;
            CB_ReDownloadFiles.IsChecked = Settings.ReDownloadFiles;
            CB_ReloadFullTaxonomy.IsChecked = Settings.ReloadFullTaxonomy;
            CB_ReloadFullTaxonomyButStructure.IsChecked = Settings.ReloadFullTaxonomyButStructure;
            CB_ReloadTaxonomyOnInstanceLoaded.IsChecked = Settings.ReloadTaxonomyOnInstanceLoaded;
            CB_ValidateOnInstanceLoaded.IsChecked = Settings.ValidateOnInstanceLoaded;
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            Settings.CheckValidationCells = CB_CheckValidationCells.IsChecked.Value;
            Settings.ReDownloadFiles = CB_ReDownloadFiles.IsChecked.Value;
            Settings.ReloadFullTaxonomy = CB_ReloadFullTaxonomy.IsChecked.Value;
            Settings.ReloadFullTaxonomyButStructure = CB_ReloadFullTaxonomyButStructure.IsChecked.Value;
            if (Settings.ReloadFullTaxonomy)
            {
                Settings.ReloadFullTaxonomyButStructure = true;
            }
            Settings.ReloadTaxonomyOnInstanceLoaded = CB_ReloadTaxonomyOnInstanceLoaded.IsChecked.Value;
            Settings.ValidateOnInstanceLoaded = CB_ValidateOnInstanceLoaded.IsChecked.Value;

            //Features.SaveSettings();
        }
    }
}
