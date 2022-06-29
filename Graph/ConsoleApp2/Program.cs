using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            Graph g = new Graph();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddVertex("D");

            //добавление ребер
            g.AddEdge("A", "B", 22);
            g.AddEdge("A", "C", 33);
            g.AddEdge("A", "D", 61);
            g.AddEdge("B", "C", 47);
            g.AddEdge("B", "D", 47);


            List<string[]> k = new List<string[]>(g.FindVertexEdgle()); // вызыва
            foreach (var item in g.FindVertexEdgle())
            {
                if(Fix( k, item[0], item[1]))
                    Console.WriteLine(item[0]+ "  " + item[1] + " " + item[2]);
            }
            Console.ReadKey();

        }

        // У Адаменко слабый ноут, поэтому делали вместе через дискорд, если есть какие-то вопросы или вас что-то не устраивает, то ждем ваших вопросиков и возмущений :)

        static bool Fix(List<string[]> k, string k1, string k2)  
        {
            bool b = true;
            for (int i = 0; i < k.Count; i++)
            {
                if (k[i][0] == k2 && k[i][1] == k1) b = false;
                if (k[i][0] == k1 && k[i][1] == k2) b = true;
            }
            return b;
        }
    }
}
