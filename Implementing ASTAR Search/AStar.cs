using System;
using System.Collections.Generic;
using System.Linq;

namespace AStarAlgorithm
{
    public class Program
    {
        public static string[,] map =
        {
            {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "1", "1", "1", "1", "0", "0", "0", "0", "0"},
            {"0", "0", "0", "0", "1", "0", "0", "1", "0", "0"},
            {"0", "0", "0", "0", "1", "1", "1", "1", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "1", "0", "0"},
            {"0", "0", "0", "0", "1", "0", "0", "1", "0", "0"},
            {"0", "0", "0", "0", "1", "1", "1", "1", "0", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
        };
        public static Node root = new Node(3,2);
        public static Node end = new Node(3,9);
        static void Main()
        {
            root.G = 0;
            var Open = new List<Node>();
            var Close = new List<Node>();
            Open.Add(root);
            bool endContain;
            do
            {
                endContain = false;
                Open = Open.OrderBy(x => x.F).ToList();
                var current = Open[0];
                Open.Remove(current); Close.Add(current);
                for (int i = 0; i < Close.Count; i++) if (Close[i].XCoord == end.XCoord && Close[i].YCoord == end.YCoord) endContain = true;
                if (endContain) break;

                for (int i = current.YCoord - 1; i <= current.YCoord + 1; i++)
                {
                    for (int j = current.XCoord - 1; j <= current.XCoord + 1; j++)
                    {
                        if (i < 0 || j < 0 || i >= map.GetLength(0) || j >= map.GetLength(1)) continue;
                        if (map[i, j] == "1" || (current.XCoord == j && current.YCoord == i)) continue;
                        var child = new Node(i, j);
                        bool closeContain = false;
                        for (int k = 0; k < Close.Count; k++) if (Close[k].XCoord == child.XCoord && Close[k].YCoord == child.YCoord) closeContain = true;
                        if (closeContain) continue;
                        bool openContain = false;
                        for (int k = 0; k < Open.Count; k++)if (Open[k].XCoord == child.XCoord && Open[k].YCoord == child.YCoord) openContain = true;
                        if (!openContain)
                        {
                            child.Parent = current; child.GetG(); child.GetH(); child.GetF(); 
                            Open.Add(child);
                        }
                    }
                }
                Console.Clear();
            } while (!endContain);

            var pathVariable = Close[Close.Count - 1];
            do
            {
                map[pathVariable.YCoord, pathVariable.XCoord] = "X";
                pathVariable = pathVariable.Parent;
            } while (pathVariable.Parent != null);
            map[root.YCoord, root.XCoord] = "S";
            map[end.YCoord, end.XCoord] = "E";
            PrintMap();
            Console.Read();
        }

        public static void PrintMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == "X" || map[i,j] == "E" || map[i,j] == "S")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(map[i, j] + " ");
                    }
                    else if (map[i, j] == "1")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(map[i, j] + " ");
                    }
                    else Console.Write(map[i, j] + " ");
                    Console.BackgroundColor = ConsoleColor.Black;
                        


                }
                Console.WriteLine();
            }
        }
    }
}
