using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class PersistentBase 
    {
        public List<PersistentBase> Dependencies = new List<PersistentBase>();
        private Action _Load = null;
        private Action _Create = null;
        private Action _Save = null;
        private Action _Delete = null;

        public virtual void Load() {
            _Load();
        }

        public virtual void Create() {
            _Create();

        }

        public virtual void Save() 
        {
            _Save();
        }
        public PersistentBase(Action Load, Action Save, Action Create,Action Delete)
        {
            _Load = Load;
            _Save = Save;
            _Create = Create;
            _Delete = Delete;
        }
    }
    public class Persistent<T> : PersistentBase where T : class
    {
        public string pathformat = "";
        public Func<T> Accessor = null;





        public override void Load() {
            base.Load();
        }

        public override void Create() {
            base.Create();

        }

        public override void Save() {
            base.Save();
     
        }

        public Persistent(Func<T> Accessor, Action Load, Action Save, Action Create, Action Delete):base(Load,Save,Create,Delete)
        {
            
            this.Accessor = Accessor;
      
        }
 
        public static void test() 
        {
    

        }

    }

    //public class PersistentList<T> : Persistent<T> where T : IList
    //{
    //    public PersistentList(Func<T> Accessor):base(Accessor,null,null,null,null) 
    //    { 

    //    }

    //    public override void Load()
    //    {
    //        if (Accessor().Count > 0) 
    //        {
    //            Accessor().Clear();
    //        }
    //        //check for persisted data
    //        //if exists the load it
    //        //if not exists then Create and Save and Load

       
    //    }

    //    public override void Create()
    //    {
    //        base.Create();

    //    }

    //    public override void Save()
    //    {
    //        base.Save();

    //    }
    //}


    //public class XTest 
    //{
    //    public void test() 
    //    {
    //        var l = new List<string>();
    //        var p = new PersistentList<List<string>>(()=>l);
    //    //{
    //    //    var p = new PersistentBase(null,null,null);
    //    //    p.z.Add("sf");
    //    //    var c = new Persistent<List<string>>(() => p.z);
    //    //    c.Save();
    //    }
    //}

}
