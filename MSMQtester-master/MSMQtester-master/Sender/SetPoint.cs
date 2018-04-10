using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public class SetPoint
    {
        public float T1Position = 0.0f;
        public float T2Position = 0.0f;
        public float A1Position = 0.0f;
        public float A2Position = 0.0f;
        public float A3Position = 0.0f;
        public float B1Position = 0.0f;
        public float B2Position = 0.0f;
        public float B3Position = 0.0f;
        public float C1Position = 0.0f;
        public float C2Position = 0.0f;
        public float C3Position = 0.0f;
        public float D1Position = 0.0f;
        public float D2Position = 0.0f;
        public float D3Position = 0.0f;

        public SetPoint()
        {
            this.T1Position = 0.0f;
            this.T2Position = 0.0f;
            this.A1Position = 0.0f;
            this.A2Position = 0.0f;
            this.A3Position = 0.0f;
            this.B1Position = 0.0f;
            this.B2Position = 0.0f;
            this.B3Position = 0.0f;
            this.C1Position = 0.0f;
            this.C2Position = 0.0f;
            this.C3Position = 0.0f;
            this.D1Position = 0.0f;
            this.D2Position = 0.0f;
            this.D3Position = 0.0f;
        }

        public SetPoint(float T1, float T2, float A1, float A2, float A3, float B1, float B2, float B3, float C1, float C2, float C3, float D1, float D2, float D3)
        {
            this.A1Position = A1;
            this.A2Position = A2;
            this.A3Position = A3;
            this.B1Position = B1;
            this.B2Position = B2;
            this.B3Position = B3;
            this.C1Position = C1;
            this.C2Position = C2;
            this.C3Position = C3;
            this.D1Position = D1;
            this.D2Position = D2;
            this.D3Position = D3;
            this.T1Position = T1;
            this.T2Position = T2;
        }
    }
}
