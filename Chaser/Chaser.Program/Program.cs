using Chaser.Common;
using Chaser.Items;
using System;
using System.IO;
using System.Reflection;

namespace Chaser.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            static void LoadDll(Game g)
            {
                string[] plugins = Directory.GetFiles(Environment.CurrentDirectory, "*.dll");
                foreach (string file in plugins)
                {
                    Assembly asm = Assembly.LoadFile(file);
                    foreach (Type currentType in asm.GetTypes())
                    {
                        if (currentType.GetInterface(nameof(IGameItem)) != null)
                        {
                            Console.WriteLine($">>> Loading plugin: {currentType}");
                            object instance = Activator.CreateInstance(currentType);
                            g.AddPlayer(instance as IGameItem);
                        }
                    }
                }
                Console.WriteLine("Press ENTER to start");
                Console.ReadLine();
            }
            Console.CursorVisible = false;
            Game g = new Game(10, 10);
            //g.AddPlayer(new RandomEnemy());
            //g.AddPlayer(new FollowerEnemy());
            //g.AddPlayer(new UserPlayer());
            LoadDll(g);
            while (true)
            {
                g.OneTick();
                Console.Clear();
                Console.WriteLine(g.ToString());
                System.Threading.Thread.Sleep(300);
            }
        }
    }
}
