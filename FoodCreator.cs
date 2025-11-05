using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UusimangTARpv24
{
    class FoodCreator
    {
        int mapWidht;
        int mapHeight;
        char sym;
        Walls cw;

        Random random = new Random();

        public FoodCreator(int mapWidth, int mapHeight, char sym, Walls cw)
        {
            this.mapWidht = mapWidth;
            this.mapHeight = mapHeight;
            this.sym = sym;
            this.cw = cw;
        }

        public Point CreateFood()
        {
            int x, y;
            while (true)
            {
                x = random.Next(2, mapWidht - 2);
                y = random.Next(2, mapHeight - 2);
                if (!cw.IsHit(new Figure(x, y)))
                {
                    break;
                }
            }
            return new Point(x, y, sym);
        }
    }
}
