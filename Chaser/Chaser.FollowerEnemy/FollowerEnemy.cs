using Chaser.Common;
using System;

namespace Chaser.Items
{
    public class FollowerEnemy : IGameItem
    {
        public char ItemChar => 'x';

        public MyPoint ProcessMovement(char[,] map, int your_x, int your_y)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            int dx = 0;
            int dy = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x,y] == '*')
                    {
                        dx = x - your_x;
                        dy = y - your_y;
                        if (dx < 0) return MoveDirection.Left;
                        if (dx > 0) return MoveDirection.Right;
                        if (dy < 0) return MoveDirection.Up;
                        if (dy > 0) return MoveDirection.Down;
                    }
                }
            }
            return MoveDirection.None;
        }
    }
}
