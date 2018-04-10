using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public class Node
    {
        public string name;

        public int id;
        public List<int> children;
        public int parent = Globals.NULLPARENT;//indicates the root node

        private float T1Position = 0.0f;
        private float T2Position = 0.0f;
        private float A1Position = 0.0f;
        private float A2Position = 0.0f;
        private float A3Position = 0.0f;
        private float B1Position = 0.0f;
        private float B2Position = 0.0f;
        private float B3Position = 0.0f;
        private float C1Position = 0.0f;
        private float C2Position = 0.0f;
        private float C3Position = 0.0f;
        private float D1Position = 0.0f;
        private float D2Position = 0.0f;
        private float D3Position = 0.0f;

        public Node(string name, SetPoint handPosition, int id, List<int> children, int parent)
        {
            this.name = name;
            this.id = id;
            this.children = children;
            this.parent = parent;
            this.setHandPosition(handPosition);
        }

        public void setHandPosition(SetPoint input)
        {
            //needed for error catching
            if (input != null)
            {
                this.T1Position = input.T1Position;
                this.T2Position = input.T2Position;
                this.A1Position = input.A1Position;
                this.A2Position = input.A2Position;
                this.A3Position = input.A3Position;
                this.B1Position = input.B1Position;
                this.B2Position = input.B2Position;
                this.B3Position = input.B3Position;
                this.C1Position = input.C1Position;
                this.C2Position = input.C2Position;
                this.C3Position = input.C3Position;
                this.D1Position = input.D1Position;
                this.D2Position = input.D2Position;
                this.D3Position = input.D3Position;
            }
        }

        public SetPoint getHandPosition()
        {
            return new SetPoint()
            {
                T1Position = this.T1Position,
                T2Position = this.T2Position,
                A1Position = this.A1Position,
                A2Position = this.A2Position,
                A3Position = this.A3Position,
                B1Position = this.B1Position,
                B2Position = this.B2Position,
                B3Position = this.B3Position,
                C1Position = this.C1Position,
                C2Position = this.C2Position,
                C3Position = this.C3Position,
                D1Position = this.D1Position,
                D2Position = this.D2Position,
                D3Position = this.D3Position,
            };
        }
    }
}