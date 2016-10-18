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

namespace Desktop
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
    
        public Settings settings = null;
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
            CB_CheckValidationCells.IsChecked = settings.CheckValidationCells;
            CB_ReDownloadFiles.IsChecked = settings.ReDownloadFiles;
            CB_ReloadFullTaxonomy.IsChecked = settings.ReloadFullTaxonomy;
            CB_ReloadFullTaxonomyButStructure.IsChecked = settings.ReloadFullTaxonomyButStructure;
            CB_ReloadTaxonomyOnInstanceLoaded.IsChecked = settings.ReloadTaxonomyOnInstanceLoaded;
            CB_ValidateOnInstanceLoaded.IsChecked = settings.ValidateOnInstanceLoaded;
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            settings.CheckValidationCells = CB_CheckValidationCells.IsChecked.Value;
            settings.ReDownloadFiles = CB_ReDownloadFiles.IsChecked.Value;
            settings.ReloadFullTaxonomy = CB_ReloadFullTaxonomy.IsChecked.Value;
            settings.ReloadFullTaxonomyButStructure = CB_ReloadFullTaxonomyButStructure.IsChecked.Value;
            if (settings.ReloadFullTaxonomy)
            {
                settings.ReloadFullTaxonomyButStructure = true;
            }
            settings.ReloadTaxonomyOnInstanceLoaded = CB_ReloadTaxonomyOnInstanceLoaded.IsChecked.Value;
            settings.ValidateOnInstanceLoaded = CB_ValidateOnInstanceLoaded.IsChecked.Value;

            //Features.SaveSettings();
        }
    }
}
