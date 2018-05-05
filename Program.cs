using System;
using System.IO;
using System.Threading;

/** <remark>
 * Perform the previous exercise considering that the thread writing numbers to file has more priority.
 * Give 10 seconds to perform writing operation and calculate number of strings written to both files.
 *
 * Выполните предыдущее упражнение, считая, что поток с записываемыми в файл числами имеет более высокий приоритет.
 * Дайте 10 секунд для выполнения операции записи и вычислите количество строк, записанных в оба файла.
</remark> */


namespace _20180329_Task2_Thread
{
    internal class Program
    {
        private static readonly Random random = new Random();
        private static bool stop = true;   // Flag to stop recording

        private static void FirstTread()
        {
            var first_out = new StreamWriter(@"FirstTread.txt", false);
            int firstCount = 0;

            while(stop)
            {
                first_out.WriteLine(random.Next());
                firstCount++;
            }
            first_out.Close();

            Console.WriteLine("Number of strings in the first thread: {0}", firstCount);
        }

        private static void SecondTread()
        {
            var second_out = new StreamWriter(@"SecondTread.txt", false);
            int secondCount = 0;

            while (stop)
            {
                second_out.WriteLine("Hello!");
                secondCount++;
            }
            second_out.Close();

            Console.WriteLine("Number of strings in the second thread: {0}", secondCount);
        }

        private static void Main()
        {
            // The first thread
            var firstThread = new Thread(FirstTread) {Priority = ThreadPriority.Highest};

            // The second thread
            var secondThread = new Thread(SecondTread) {Priority = ThreadPriority.Lowest};

            firstThread.Start();
            secondThread.Start();

            // Время выполнения операции записи и вычислите количество строк
            // При установке 10 секунд размеры файлов составляют несколько Гигабайт!!!
            firstThread.Join(TimeSpan.FromMilliseconds(30));
            secondThread.Join(TimeSpan.FromMilliseconds(30));
            stop = false;

            Console.WriteLine("Check contents of the created files.");

            Console.ReadKey();
        }
    }
}