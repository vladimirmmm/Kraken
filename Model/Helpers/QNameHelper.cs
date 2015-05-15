using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Helpers
{
    public class QNameHelpers
    {
        public static string GetFactString(Concept concept, List<Dimension> dimensions) 
        {
            var fb = new FactBase();
            fb.Concept = concept;
            fb.Dimensions = dimensions;
            return fb.GetFactString();
        }

        public static FactBase GetFactBase(string item) 
        {
            var fb = new FactBase();
            fb.SetFromString(item);
            return fb;
        }
    }
}
