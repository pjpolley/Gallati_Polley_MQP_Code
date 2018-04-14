using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public class Threshhold
    {

        public double min;
        public double max;

        public Threshhold(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        public bool Contains(double input)
        {
            if(input >= min && input < max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Set(double INmin, double INmax)
        {
            this.min = INmin;
            this.max = INmax;
        }
    }
}
