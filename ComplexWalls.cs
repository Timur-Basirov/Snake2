using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{

    class ComplexWalls : Walls
    {
        public ComplexWalls(int mapWidth, int mapHeight) : base(mapWidth, mapHeight)
        {

            HorizontalLine upW1 = new HorizontalLine(5, mapWidth - 64, 6, '█');
            VerticalLine leftW1 = new VerticalLine(3, mapHeight - 19, 17, '█');
            VerticalLine leftW2 = new VerticalLine(2, mapHeight - 18, 21, '█');
            VerticalLine leftW3 = new VerticalLine(3, mapHeight - 15, 40, '█');
            HorizontalLine upW2 = new HorizontalLine(22, mapWidth - 41, 4, '█');
            HorizontalLine upW3 = new HorizontalLine(30, mapWidth - 41, 8, '█');
            HorizontalLine upW4 = new HorizontalLine(41, mapWidth - 27, 10, '█');
            VerticalLine leftW4 = new VerticalLine(5, mapHeight - 18, 48, '█');
            HorizontalLine upW5 = new HorizontalLine(49, mapWidth - 13, 5, '█');
            VerticalLine leftW5 = new VerticalLine(2, mapHeight - 21, 57, '█');
            VerticalLine leftW6 = new VerticalLine(6, mapHeight - 19, 67, '█');
            VerticalLine leftW7 = new VerticalLine(9, mapHeight - 9, 67, '█');
            VerticalLine leftW8 = new VerticalLine(14, mapHeight - 7, 44, '█');
            HorizontalLine upW6 = new HorizontalLine(68, mapWidth - 5, 9, '█');
            HorizontalLine upW7 = new HorizontalLine(59, mapWidth - 14, 15, '█');
            HorizontalLine upW8 = new HorizontalLine(25, mapWidth - 25, 19, '█');
            VerticalLine leftW9 = new VerticalLine(15, mapHeight - 3, 25, '█');
            VerticalLine leftw10 = new VerticalLine(20, mapHeight - 4, 39, '█');
            HorizontalLine upW9 = new HorizontalLine(60, mapWidth - 10, 22, '█');
            VerticalLine leftw11 = new VerticalLine(20, mapHeight - 4, 60, '█');
            HorizontalLine upW10 = new HorizontalLine(66, mapWidth - 6, 19, '█');
            VerticalLine leftw12 = new VerticalLine(20, mapHeight - 4, 74, '█');
            VerticalLine leftw13 = new VerticalLine(13, mapHeight - 5, 19, '█');
            HorizontalLine upW11 = new HorizontalLine(10, mapWidth - 61, 12, '█');

            wallList.Add(upW1);
            wallList.Add(leftW1);
            wallList.Add(leftW2);
            wallList.Add(leftW3);
            wallList.Add(upW2);
            wallList.Add(upW3);
            wallList.Add(upW4);
            wallList.Add(leftW4);
            wallList.Add(upW5);
            wallList.Add(leftW5);
            wallList.Add(leftW6);
            wallList.Add(leftW7);
            wallList.Add(leftW8);
            wallList.Add(upW6);
            wallList.Add(upW7);
            wallList.Add(upW8);
            wallList.Add(leftW9);
            wallList.Add(leftw10);
            wallList.Add(upW9);
            wallList.Add(leftw11);
            wallList.Add(upW10);
            wallList.Add(leftw12);
            wallList.Add(leftw13);
            wallList.Add(upW11);
        }
        public override bool IsHit(Figure figure)
        {
            foreach (var wall in wallList)
            {
                if (wall.IsHit(figure))
                {
                    return true;
                }
            }
            return false;
        }
    }
    
}