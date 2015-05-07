using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{

    public class Rule:Identifiable
    {
        public List<Table> Tables = new List<Table>();
        //public List<Fact> Facts = new List<Fact>();
        public Func<Object[], bool> Function = null;

        public String RuleValue = "";

        public String ErrorMessageFormat = "";
        public String ErrorMessage
        {
            get;
            set;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
