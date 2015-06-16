using LogicalModel;
using Microsoft.Win32;
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
using Utilities;
using XBRLProcessor;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected Features Features = null;
        
        public MainWindow()
        {
            InitializeComponent();

            var uitw = new UITextWriter(ShowMessage);
            Console.SetOut(uitw);

            Browser_Right.LoadCompleted += Browser_LoadCompleted;
            Browser_Left.LoadCompleted += Browser_LoadCompleted;
            var ofs =new JsClass();
            ofs.UIAction=fromJS;
            Browser_Left.ObjectForScripting = ofs;
            Browser_Right.ObjectForScripting = ofs;
            var mw =this;
            this.Features = new Features(this);

            this.Features.LoadMenu(this.Features.CommandContainer, null);
            this.Features.LoadFeatures(this.Features.FeatureContainer, null);

        }

        void fromJS(object param) 
        {
            var command = String.Format("{0}", param).ToLower();
            Console.WriteLine(command);
            if (command == "table_ready") 
            {
                SetExtension();
            }
            if (command.StartsWith("navigatetocell")) 
            {
                var cell = command.Substring(command.IndexOf(":") + 1);
                var report = cell.Remove(cell.IndexOf("<"));
                var cellspecifiers = cell.TextBetween("<", ">").Split('|');
                var extension = cellspecifiers[0];
                var row = cellspecifiers[1];
                var column = cellspecifiers[2];

                Features.NavigateToCell(report, extension, row, column);
            }

        }

        void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (TabControl_Left.SelectedIndex == 1) 
            {
            }

            //mshtml.HTMLDocument doc;
            //doc = (mshtml.HTMLDocument)Browser.Document;
            //mshtml.HTMLDocumentEvents2_Event iEvent;
            //iEvent = (mshtml.HTMLDocumentEvents2_Event)doc;
            //iEvent.onclick += new mshtml.HTMLDocumentEvents2_onclickEventHandler(HtmlDocumentClickEventHandler);
  
        }

        private bool HtmlDocumentClickEventHandler(mshtml.IHTMLEventObj pEvtObj)
        {
            int z = 0;
            return true;
        }

        private void SetExtension() 
        {
            var table = Table_Tree.SelectedItem as Table;
            var extension = Table_Tree.SelectedItem as LayoutItem;
            if (extension != null)
            {
                table = extension.Table;

                try
                {
                    var extparam = table.CurrentExtension == null ? "" : Utilities.Converters.ToJson(table.CurrentExtension);
                    Browser_Right.InvokeScript("SetExtension", extparam);
                    var instance = Features.CurrentInstance;
                    if (instance != null)
                    {
                        Browser_Right.InvokeScript("LoadInstance", "");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ShowMessage(string content) 
        {
            if (this.Dispatcher.CheckAccess())
            {
                String msg = String.Format("{0:yyyy:MM:dd hh:mm:ss} {1} \r\n", DateTime.Now, content.Trim());
                TB_Console.Text += msg;
                if (TB_Console.LineCount > 3000) 
                {
                    var lines = TB_Console.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1000);
                    var sb = new StringBuilder();
                    foreach (var line in lines) 
                    {
                        sb.AppendLine(line);
                    }
                    TB_Console.Text = sb.ToString();
                }
                TB_Console.Focus();
                TB_Console.CaretIndex = TB_Console.Text.Length;
                TB_Console.ScrollToEnd();
                TB_Console.UpdateLayout();
            }
            else 
            {
                this.Dispatcher.Invoke(() => { ShowMessage(content); });
            }
        }

        private void B_LoadTaxonomy_Click(object sender, RoutedEventArgs e)
        {
            var path = TB_TaxonomyPath.Text;
            Utilities.Threading.ExecuteAsync(() => { Features.LoadTaxonomy(path); });
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

                    Browser_Left.Navigate(file);

                }
            }
        }

        private void B_LoadLabel_Click(object sender, RoutedEventArgs e)
        {
            var table = Table_Tree.SelectedItem as Table;
            if (table != null)
            {
                table.CreateHtmlLayout(true);
            }
            Browser_Right.Refresh();
   
        }

        private void Table_Tree_Node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


        }

        private void TB_LabelFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Features.SearchLabels(TB_LabelFilter.Text);
         
        }

        private void LabelNode_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Table_Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var table = Table_Tree.SelectedItem as Table;
            var extension = Table_Tree.SelectedItem as LayoutItem;
            if (extension != null)
            {
                table = extension.Table;
                table.CurrentExtension = extension;
            }
            if (table != null)
            {
                table.EnsureHtmlLayout();
                Browser_Right.Navigate(table.HtmlPath);
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

        private void B_Test_Click(object sender, RoutedEventArgs e)
        {
            //var x = new List<Utilities.KeyValue<string, string>>();
            //x.Add(new KeyValue<string, string>("a", "111"));
            //x.Add(new KeyValue<string, string>("b", "112"));
  
            //Browser.InvokeScript("test", Utilities.Converters.ToJson(x));

            var table = Table_Tree.SelectedItem as Table;
            var extension = Table_Tree.SelectedItem as LayoutItem;
            if (extension != null)
            {
                table = extension.Table;
                table.CurrentExtension = extension;
            }
            if (table != null)
            {
                table.LayoutCells.Clear();
                table.LoadLayout();
                table.CreateHtmlLayout(true);
                //Browser.Navigate(table.HtmlPath);
                //TB_Title.Text = String.Format("{0} - {1}", table.ID, table.HtmlPath);

            }
        }

        private void TB_FactFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Features.SearchFacts(TB_FactFilter.Text);
        }

        private void TB_CellFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Features.SearchCells(TB_CellFilter.Text);
        }

        private void TB_GeneralFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_GeneralFilter.Text.Length > 1) 
            {
                Features.Search(TB_GeneralFilter.Text);
            }
        }

        private void B_Browser_Back_Click(object sender, RoutedEventArgs e)
        {
            var Browser = Browser_Right.IsFocused ? Browser_Right : Browser_Left;

            if (Browser.CanGoBack) 
            {
                Browser.GoBack();
            }
        }

        private void B_Browser_Forward_Click(object sender, RoutedEventArgs e)
        {
            var Browser = Browser_Right.IsFocused ? Browser_Right : Browser_Left;
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }




    }
}
