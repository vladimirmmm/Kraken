﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{
    public class TaxonomyEventArgs : EventArgs 
    {
        public string FilePath = "";
        public TaxonomyEventArgs() 
        {

        }
        public TaxonomyEventArgs(string filepath)
        {
            this.FilePath = filepath;
        }
    }
    public abstract class TaxonomyEngine
    {
        public Instance CurrentInstance = null;
        public Taxonomy CurrentTaxonomy = null;
        public static TaxonomyEngine CurrentEngine = null;

        public delegate void TaxonomyEventHandler(object sender, TaxonomyEventArgs e);
        public event TaxonomyEventHandler TaxonomyLoad;
        public event TaxonomyEventHandler TaxonomyLoadFailed;
        public event TaxonomyEventHandler TaxonomyLoaded;
        public event TaxonomyEventHandler InstanceLoad;
        public event TaxonomyEventHandler InstanceLoadFailed;
        public event TaxonomyEventHandler InstanceLoaded;

        public DateTime? TaxLoadStartDate = null;
        public DateTime? InstLoadStartDate = null;
        public DateTime? ValidationStartDate = null;

        public Boolean IsInstanceLoading = false;

        public string HtmlPath = AppDomain.CurrentDomain.BaseDirectory + "UI.html";
        
        public static string PreparedFolder = "Taxonomies";
        private static string _LocalFolder = @"";
        public static string LocalFolder
        {
            get
            {
                if (String.IsNullOrEmpty(_LocalFolder))
                {
                    var appdatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var folder = String.Format(@"{0}\{1}\", appdatafolder, "XbrlStudio");
                    var taxfolder = folder + "Taxonomies\\";
                    Utilities.FS.EnsurePath(taxfolder);
                    _LocalFolder = taxfolder;
                }
                return _LocalFolder;
            }
            set
            {
                _LocalFolder = value;
            }
        }
        public static string _LogFolder = "";
        public static string LogFolder
        { 
            get
            {
                if (String.IsNullOrEmpty(_LogFolder))
                {
                    _LogFolder=string.Format("{0}Log\\",LocalFolder);
                }
                return _LogFolder;
            }
        }
        public TaxonomyEngine() 
        {

            if (CurrentEngine == null)
            {
                CurrentEngine = this;
            }
            var filename = Utilities.Strings.GetStringForFilename( Utilities.Converters.DateTimeToString(DateTime.Now));
            Logger.LogPath = LogFolder + String.Format("{0}.txt", filename);
            Logger.WriteLine(LocalFolder);
        }
        public virtual bool LoadInstance(string filepath, bool wait) 
        {
            return true;
        }
        public virtual string GetFilePath()
        {
            return "";
        }
        public virtual bool LoadTaxonomy(string filepath)
        {
            return true;

        }

        public virtual bool Trigger_TaxonomyLoad(string filepath)
        {
            if (TaxonomyLoad != null)
            {
                TaxonomyLoad(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_TaxonomyLoadFailed(string filepath)
        {
            if (TaxonomyLoadFailed != null)
            {
                TaxonomyLoadFailed(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_TaxonomyLoaded(string filepath)
        {           
            if (TaxonomyLoaded != null)
            {
                TaxonomyLoaded(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }

        public virtual bool Trigger_InstanceLoad(string filepath)
        {
            if (InstanceLoad != null)
            {
                InstanceLoad(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_InstanceLoadFailed(string filepath)
        {
            if (InstanceLoadFailed != null)
            {
                InstanceLoadFailed(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
        public virtual bool Trigger_InstanceLoaded(string filepath)
        {
            if (InstanceLoaded != null)
            {
                InstanceLoaded(this, new TaxonomyEventArgs(filepath));
            }
            return true;
        }
    }
}
