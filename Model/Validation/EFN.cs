using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public partial class Functions
    {
        //if ($cond1) then $cond2 else not($cond2)
        public bool EFN_IFF(bool cond1, bool cond2) 
        {
            return cond1 ? cond2 : !cond2;

        }
        //if ($precond) then $test else true()
        public bool EFN_IMP(bool precond, bool test)
        {
            return precond ? test : true;
        }
    }
}
