using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class Persistent<T> where T :class
    {
        public string pathformat = "";

        public LambdaExpression TargetExpression = null;

        public void Load(T target) { }

        public void Save(T target) { }

        public Action Create = null;
    }




}
