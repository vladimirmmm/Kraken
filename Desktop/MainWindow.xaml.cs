using BaseModel;
using Engine;
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

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppEngine engine = new AppEngine();
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.ThreadPool.SetMaxThreads(2, 2);

            //var uitw = new UITextWriter(ShowMessage);
            //Console.SetOut(uitw);
            //Utilities.Logger.action = ShowMessage;

            Browser.LoadCompleted += Browser_LoadCompleted;
            var ofs =new JsClass();
            ofs.UIAction=fromJS;
            Browser.ObjectForScripting = ofs;
            var mw =this;
            UIService ui = new WindowUIService(this);
            engine.Start(ui);
         



        }

        public void ShowSettings()
        {
            if (this.Dispatcher.CheckAccess())
            {
                //var window = new SettingsWindow();
                //window.SetFeatures(Features);
                //window.Show();
            }
            else
            {
                this.Dispatcher.Invoke(() => { ShowSettings(); });

            }
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
                engine.Features.UI.ToUI(response);

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

               
                        response = engine.Features.ProcessRequest(request);
                        engine.Features.UI.ToUI(response);


                }
                catch (Exception ex)
                {
                    response.Error = ex.Message + "\r\n";
                    Logger.WriteLine(String.Format("Error at Features.ProcessRequest. Reqest: {0} > {1}", request, ex.Message));
                    engine.Features.UI.ToUI(response);

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
            isuiloaded = true;
        }

        private bool HtmlDocumentClickEventHandler(mshtml.IHTMLEventObj pEvtObj)
        {
    
            return true;
        }
        private object ConsoleLocker = new Object();
        private StringBuilder loggbuilder = new StringBuilder();

        private bool isuiloaded = false;
        
        public void ShowMessage(string content) 
        {
            if (this.Dispatcher.CheckAccess())
            {
                String msg = String.Format("{0:yyyy:MM:dd hh:mm:ss} {1} \r\n", DateTime.Now, content.Trim());

                if (engine.Features == null || !isuiloaded)
                {
                    loggbuilder.Append(msg);
                }
                else 
                {
                    var message = new Message();
                    message.Category = "notfication";
                    message.Data = loggbuilder.ToString()+ msg;
                    loggbuilder.Clear();
                    engine.Features.UI.ToUI(message);

                }
                //lock (ConsoleLocker)
                //{
                //    TB_Console.Text += msg;
                //    if (TB_Console.LineCount > 3000)
                //    {
                //        var lines = TB_Console.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1000);
                //        var sb = new StringBuilder();
                //        foreach (var line in lines)
                //        {
                //            sb.AppendLine(line);
                //        }
                //        TB_Console.Text = sb.ToString();
                //    }
                //    TB_Console.Focus();
                //    TB_Console.CaretIndex = TB_Console.Text.Length;
                //    TB_Console.ScrollToEnd();
                //    TB_Console.UpdateLayout();
                //}
            }
            else 
            {
                this.Dispatcher.Invoke(() => { ShowMessage(content); });
            }
        }

        private void B_LoadTaxonomy_Click(object sender, RoutedEventArgs e)
        {
            var taxonomy = engine.Features.Engine.CurrentTaxonomy;
            if (taxonomy != null)
            {
                Utilities.Threading.ExecuteAsync(() => { engine.Features.LoadTaxonomy(taxonomy.EntryDocument.LocalPath); });
            }
        }
  

        private void B_LoadLabel_Click(object sender, RoutedEventArgs e)
        {
    
            try
            {
                dynamic doc = Browser.Document;

                var htmlText = doc.documentElement.InnerHtml;
                System.IO.File.WriteAllText(engine.Features.Engine.HtmlPath.Replace(".html", "_Current.html"), htmlText);
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
         
        }

        private void LabelNode_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }



        private void B_Test_Click(object sender, RoutedEventArgs e)
        {
         
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

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5) 
            {
                Browser.Refresh();
            }
        }




    }
}
