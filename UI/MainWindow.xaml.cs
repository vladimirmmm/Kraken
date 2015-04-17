using LogicalModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using XBRLProcessor;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XbrlEngine Engine = new XbrlEngine();
        public MainWindow()
        {
            InitializeComponent();
            TB_TaxonomyPath.Text = @"C:\My\Tasks\!Tools\Taxonomies\XBRl taxonomy 2.2\XBRL Taxonomy and Supporting Documents.2.2\Taxonomy\2.2.0.0\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2013-02\2014-07-31\mod\corep_ind.xsd";
            //TB_TaxonomyPath.Text = @"C:\My\Tasks\!Tools\Taxonomies\XBRl taxonomy 2.2\XBRL Taxonomy and Supporting Documents.2.2\Taxonomy\2.2.0.0\www.eba.europa.eu\eu\fr\xbrl\crr\fws\finrep\its-2013-03\2014-07-31\mod\finrep_con_ifrs.xsd";
            Taxonomy.Console = ShowMessage;
        }

        private void ShowMessage(string content) 
        {
            String msg = String.Format("{0:yyyy:MM:dd hh:mm:ss} {1} \r\n", DateTime.Now, content.Trim());
            TB_Console.Text += msg;
            TB_Console.Focus();
            TB_Console.CaretIndex = TB_Console.Text.Length;
            TB_Console.ScrollToEnd();
            TB_Console.UpdateLayout();
            //Console.WriteLine(msg);
        }

        private void B_LoadTaxonomy_Click(object sender, RoutedEventArgs e)
        {
            Engine.LoadTaxonomy(TB_TaxonomyPath.Text);
            XML_Tree.ItemsSource = new List<TaxonomyDocument>() { Engine.CurrentTaxonomy.EntryDocument };
            Table_Tree.ItemsSource = Engine.CurrentTaxonomy.Tables;

        }

        private void XML_Tree_Node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var td = (TaxonomyDocument)XML_Tree.SelectedItem;
            if (td != null)
            {
                if (e.ClickCount == 1)
                {

                }
                if (e.ClickCount == 2)
                {
                    TB_Title.Text = String.Format("{0} - {1}", td.FileName, td.LocalPath);
                    var content = td.Content;
                    var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var file = folder + "\\xtree-temp.xml";
                    var securitytag = "<!-- saved from url=(0014)about:internet -->\r\n";
                    System.IO.File.WriteAllText(file, securitytag + content);

                    Browser.Navigate(file);

                }
            }
        }

        private void B_LoadLabel_Click(object sender, RoutedEventArgs e)
        {
            var table = (Table)Table_Tree.SelectedItem;
            if (table != null)
            {
                table.CreateHtmlLayout();
            }

            
        }

        private void Table_Tree_Node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


        }

        private void TB_LabelFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchpattern = TB_LabelFilter.Text;
            if (searchpattern.Length > 1)
            {
                LV_labels.ItemsSource = Engine.CurrentTaxonomy.TaxonomyLabels.Where(i => i.DisplayName.Contains(searchpattern));
            }
        }

        private void LabelNode_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Table_Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var table = (Table)Table_Tree.SelectedItem;
            if (table != null)
            {
                Browser.Navigate(table.HtmlPath);
                TB_Title.Text = String.Format("{0} - {1}", table.ID, table.HtmlPath);

            }
        }

        private void XML_Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
              var td = (TaxonomyDocument)XML_Tree.SelectedItem;
              if (td != null)
              {
                  TB_TreeDocumentTitle.Text = String.Format("{0} - {1}", td.FileName, td.LocalPath);
              }
        }
    }
}
