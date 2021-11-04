using Chaser.Common;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Chaser.Items
{
    public class UserPlayer : IGameItem
    {

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        Dictionary<ConsoleKey, MyPoint> movements;
        public UserPlayer()
        {
            movements = new Dictionary<ConsoleKey, MyPoint>();
            movements.Add(ConsoleKey.W, MoveDirection.Up);
            movements[ConsoleKey.S] = MoveDirection.Down;
            movements[ConsoleKey.A] = MoveDirection.Left;
            movements[ConsoleKey.D] = MoveDirection.Right;
        }
        public char ItemChar => '*';
        public MyPoint ProcessMovement(char[,] map, int your_x, int your_y)
        {
            foreach (var item in movements)
            {
                if (GetAsyncKeyState((int)item.Key) != 0)
                {
                    return item.Value;
                }
            }
            return MoveDirection.None;
        }
    }
}
