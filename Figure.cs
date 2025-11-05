using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UusimangTARpv24
{
    class Figure
    {
        protected List<Point> pList = new List<Point>();
        public Figure(int x, int y)
        {
            pList = new List<Point>()
            {
                new Point(x, y, 'h')
            };
        }
        public Figure() { }
        public void Draw()
        {
            foreach (Point p in pList)
            {
                p.Draw();
            }
        }

        internal bool IsHit(Figure figure)
        {
            foreach (var p in pList)
            {
                if (figure.IsHit(p))
                    return true;
            }
            return false;
        }

        private bool IsHit(Point point)
        {
            foreach (var p in pList)
            {
                if (p.IsHit(point))
                    return true;
            }
            return false;
        }
    }
}
