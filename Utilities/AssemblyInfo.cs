using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class AssemblyBuildInfo
    {
        public virtual String BuildID { get { return ""; } }
    }

    public class Info : AssemblyBuildInfo 
    {
        public override string BuildID { get { return "Utilities"; } }
    }
}
