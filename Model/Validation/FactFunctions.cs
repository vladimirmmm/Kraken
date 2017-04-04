using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public partial class Functions
    {
        public Instance Instance = null;

        

        public int Count(ValidationParameter a) 
        {
            return a.CurrentFacts.Count;
        }
        

        



    }
}
