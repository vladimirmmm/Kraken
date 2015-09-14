using LogicalModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            System.Threading.ThreadPool.SetMaxThreads(2, 2);

            //var uitw = new UITextWriter(ShowMessage);
            //Console.SetOut(uitw);
            Utilities.Logger.action = ShowMessage;

            Browser.LoadCompleted += Browser_LoadCompleted;
            var ofs =new JsClass();
            ofs.UIAction=fromJS;
            Browser.ObjectForScripting = ofs;
            var mw =this;
            var ui = new WindowUIService(this);
      
            this.Features = new Features(ui);

            LoadMenu(this.Features.CommandContainer, null);
            //this.Features.LoadFeatures(this.Features.FeatureContainer, null);

            //tet();

        }

        public void ShowSettings()
        {
            if (this.Dispatcher.CheckAccess())
            {
                var window = new SettingsWindow();
                window.SetFeatures(Features);
                window.Show();
            }
            else
            {
                this.Dispatcher.Invoke(() => { ShowSettings(); });

            }
        }

        public void LoadMenu(MenuCommand command, MenuItem menuitem)
        {
            if (this.Dispatcher.CheckAccess())
            {
                ItemCollection itemcollection = null;
                if (menuitem == null)
                {
                    itemcollection = this.Menu.Items;
                    this.Menu.Items.Clear();
                }
                else
                {
                    itemcollection = menuitem.Items;
                }

                foreach (var c in command.Children)
                {
                    var m = new MenuItem();
                    m.Header = c.DisplayName.Replace("_", "__");
                    m.IsEnabled = c.Enabled;
                    if (c.Children.Count == 0)
                    {
                        m.Click += (s, e) => { c.ExecuteAsync(); };
                    }
                    itemcollection.Add(m);
                    LoadMenu(c, m);
                }
            }
            else
            {
                this.Dispatcher.Invoke(() => { LoadMenu(command, menuitem); });

            }

        }
        public void tet()
        {
            var l = new List<LogicalModel.Base.FactBase>();
            var c =  200000;
            for (int i = 0; i < c; i++)
            {
                var fact = new LogicalModel.Base.FactBase();
                //fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[eba_dim:LEC]eba_typ:LE,[eba_dim:MCY]eba_MC:x27,[eba_dim:TRI]eba_TR:x6,");
                fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[*:LEC]eba_typ:LE:1234,[*:MCY]eba_MC:x27,[*:TRI]eba_TR:x6,");
              
                l.Add(fact);
            }

            var x = 0;
            Logger.WriteLine("take!!");
            //System.Threading.Thread.Sleep(20 * 1000);

            foreach (var fact in l)
            {
                fact.ClearObjects();
            }
            var zz = 0;
        }

        private object slocker = new object();
        void fromJS(object param) 
        {
            var request = new Message();
            var response = new Message();

            try
            {
                request = Utilities.Converters.JsonTo<Message>(param.ToString());
            }
            catch (Exception ex) 
            {
                response.Error = ex.Message;
                response.Category = "error";
                Logger.WriteLine(String.Format("Error at {0} - {1}: {2}", request.Id, request.Url, ex.Message));
                Features.ToUI(response);

            }
            var ajaxtag = "ajax";
            if (request.Category == "notification")
            {
                lock (slocker)
                {
                    Logger.WriteLine(request.Data);
                }
            }



            //Action a = ()=>{
            //      response = this.Features.ProcessRequest(request);
            //        Features.ToUI(response);
            //};
         

            if (request.Category == ajaxtag) 
            {
                try
                {
                    //response = this.Features.ProcessRequest(request);
                    //Features.ToUI(response);

               
                        response = this.Features.ProcessRequest(request);
                        Features.ToUI(response);


                }
                catch (Exception ex)
                {
                    response.Error = ex.Message + "\r\n";
                    Logger.WriteLine(String.Format("Error at Features.ProcessRequest. Reqest: {0} > {1}", request, ex.Message));
                    Features.ToUI(response);

                }
                /*
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    try
                    {
                        response = this.Features.ProcessRequest(request);
                        Features.ToUI(response);

                    }
                    catch (Exception ex)
                    {
                        response.Error = ex.Message + "\r\n";
                        Logger.WriteLine(String.Format("Error at Features.ProcessRequest. Reqest: {0} > {1}", request, ex.Message));
                        Features.ToUI(response);

                    }
                }).Start();
                */

            }

        }

        void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {

        }

        private bool HtmlDocumentClickEventHandler(mshtml.IHTMLEventObj pEvtObj)
        {
    
            return true;
        }
        private object ConsoleLocker = new Object();
        private void ShowMessage(string content) 
        {
            if (this.Dispatcher.CheckAccess())
            {
                lock (ConsoleLocker)
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

     

                }
            }
        }

        private void B_LoadLabel_Click(object sender, RoutedEventArgs e)
        {
    
            try
            {
                dynamic doc = Browser.Document;

                var htmlText = doc.documentElement.InnerHtml;
                System.IO.File.WriteAllText(this.Features.Engine.HtmlPath.Replace(".html", "_Current.html"), htmlText);
                doc = null;
            }
            catch (Exception ex) 
            {
                Logger.WriteLine("Failed to save the document! " + ex.Message);
            }
            Browser.Refresh();
   
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
                Browser.Navigate(table.FullHtmlPath);
                TB_Title.Text = String.Format("{0} - {1}", table.ID, table.FullHtmlPath);

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
            //Features.SearchFacts(TB_FactFilter.Text);
        }

        private void TB_CellFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Features.SearchCells(TB_CellFilter.Text);
        }

        private void TB_GeneralFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (TB_GeneralFilter.Text.Length > 1) 
            //{
            //    Features.Search(TB_GeneralFilter.Text);
            //}
        }

        private void B_Browser_Back_Click(object sender, RoutedEventArgs e)
        {
       
            if (Browser.CanGoBack) 
            {
                Browser.GoBack();
            }
        }

        private void B_Browser_Forward_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }




    }
}
