using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var n = 100000000;

            var duration = MeasureJosephusPerformance(n);
            Console.WriteLine($"Josephus with {n} items took {duration.TotalMinutes} minutes");

            // Josephus with 100000000 items took 0,277631865 minutes
            // 10minutes / 0,277631865 minutes ~ 35   --> with enough RAM:  n = 3500 000 000  would be possible 

            Console.ReadLine();
        }


        public static TimeSpan MeasureJosephusPerformance(int n) {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = JosephusBruteforce(n);
            Console.WriteLine(result);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public static int JosephusBruteforce(int n)
        {
            var sequence = new LinkedList<int>(Enumerable.Range(1, n));

            var nodeToDelete = sequence.First.GetNext();

            while (sequence.Count() > 1)
            {
                var nextNodeToDelete = nodeToDelete.GetNext().GetNext();
                sequence.Remove(nodeToDelete);
                nodeToDelete = nextNodeToDelete;
            }

            return sequence.Single();
        }
    }

    public static class Ring
    {
        public static LinkedListNode<T> GetNext<T>(this LinkedListNode<T> current)
        {
            return current.Next ?? current.List.First;
        }
    }
}
