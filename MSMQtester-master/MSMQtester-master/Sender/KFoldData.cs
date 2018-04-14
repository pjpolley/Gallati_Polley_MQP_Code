using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public class KFoldData
    {
        public int Breadth { get; }
        public int Depth { get; }
        public double TrainingSpeed { get;  }
        public int Iterations { get; }
        public double K { get; }
    


        public KFoldData(int breadth, int depth, double trainingSpeed, int iterations, double k)
        {
            this.Breadth = breadth;
            this.Depth = depth;
            this.TrainingSpeed = trainingSpeed;
            this.Iterations = iterations;
            this.K = k;
        }
    }
}
