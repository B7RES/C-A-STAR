using System;

namespace AStarAlgorithm
{
    public class Node
    {
        public int G { get; set; }
        public int H { get; set; }
        public int F { get; set; }

        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public Node Parent { get; set; }

        public Node(int y,int x)
        {
            XCoord = x;
            YCoord = y;
        }
        public void GetH()
        {
            H = Math.Abs(XCoord - Program.end.XCoord) + Math.Abs(YCoord - Program.end.YCoord);
        }
        public void GetG()
        {
            if (XCoord == Parent.XCoord || YCoord == Parent.YCoord) G = Parent.G + 10;
            else G = Parent.G + 14;
        }
        public void GetF()
        {
            F = H + G;
        }
    }
}
