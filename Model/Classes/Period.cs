using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Period
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public DateTime Value 
        {
            get { return StartDate; }
        }

        public override string ToString()
        {
            return String.Format("{0:yyyy-MM-dd}", Value);
        }
    }
}
