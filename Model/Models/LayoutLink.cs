using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Models
{
    public class LayoutLink
    {
        public LayoutItem From = null;
        public LayoutItem To = null;
        public int Order = 0;
        public string Role = "";
        public void test() 
        {
            var s = new List<string>() { "[eba_dim:MCY]eba_MC:x415", "[eba_dim:MCS]eba_MC:x990" };
            var s2 = new List<string>() { "y", "s" };
            var z = s.OrderBy(i => i).ToList();
            var z2 = s2.OrderBy(i => i).ToList();
            s = s.OrderBy(i => i).ToList();
            var x1 = s.OrderBy(i => i, StringComparer.InvariantCulture).ToList();
            var x2 = s.OrderBy(i => i, StringComparer.CurrentCulture).ToList();
            var x3 = s.OrderBy(i => i, StringComparer.Ordinal).ToList();
            var x4 = s.OrderBy(i => i, StringComparer.OrdinalIgnoreCase).ToList();

        }
    }
}
