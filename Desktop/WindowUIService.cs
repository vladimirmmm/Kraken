using BaseModel;
using Engine;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utilities;

namespace Desktop
{
    public class WindowUIService : UIService
    {

        private Window mw = null;
        public WindowUIService(MainWindow mw)
        {
            this.mw = mw;
            DispatcherCheckAccess = () => { return mw.Dispatcher.CheckAccess(); };
            DispatcherInvoke = (action) => { mw.Dispatcher.Invoke(action); };
            BrowserInvokeScript = (script, parameters) => { mw.Browser.InvokeScript(script, parameters); };
            BrowserNavigate = (url) => { mw.Browser.Navigate(url); };
            BrowserRefresh = () => { mw.Browser.Refresh(); };
            //LoadMenu = (command, item) => { mw.LoadMenu(command, item); };
            Close = () => { mw.Close(); };
            ShowSettings = () => { mw.ShowSettings(); };
            Log = (string s) => { mw.ShowMessage(s); };
            ProcessRequest = (m) => { Features.ProcessRequest(m); };
            BrowseFile = (folder, file) => { return BrowsForFile("", ""); };
            BrowseFolder = (folder) => { return BrowsForFolder(""); };
            ToUI = (Message m) => ToWindowUI(m);
            Utilities.Logger.action = Log;


        }
        private object BrowserLocker = new Object();

        public void ToWindowUI(Message msg)
        {
            if (this.DispatcherCheckAccess())
            {
                lock (BrowserLocker)
                {
                    var id = msg.Url;
                    try
                    {
                        BrowserInvokeScript("Communication_Listener", Utilities.Converters.ToJson(msg));
                    }
                    catch (Exception ex)
                    {
                        var str = ex.Message;
                        Logger.WriteLine("Error at Features.ToUI (" + id + "): " + str);
                    }
                }
            }
            else
            {
                DispatcherInvoke(() => { ToWindowUI(msg); });
            }
        }

        public static string BrowsForFolder(string folder)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = Features.GetSettingValue("LastFolder");
            dlg.CheckFileExists = false;
            dlg.CheckPathExists = false;
            dlg.ValidateNames = false;
            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.FileName = "Folder.";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                folder = Utilities.Strings.GetFolder(filename);
                Features.SetSettingValue("LastFolder", folder);
                return folder;
            }
            return "";
        }
        
        public static string BrowsForFile(string folder, string proposedfilename)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = Features.GetSettingValue("LastFolder");
            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.ValidateNames = false;
            dlg.CheckPathExists = false;
            dlg.CheckFileExists = false;
            dlg.FileName = proposedfilename;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                folder = Utilities.Strings.GetFolder(filename);
                Features.SetSettingValue("LastFolder", folder);
                return filename;
            }
            return "";
        }

    }
}
