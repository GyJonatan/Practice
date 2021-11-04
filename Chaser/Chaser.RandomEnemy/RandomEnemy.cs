using Chaser.Common;
using System;

namespace Chaser.Items
{
    public class RandomEnemy : IGameItem
    {
        static Random rnd = new Random();
        public char ItemChar => '+';

        public MyPoint ProcessMovement(char[,] map, int your_x, int your_y)
        {
            return new MyPoint(rnd.Next(-1, 2), rnd.Next(-1, 2));
        }
    }
}
