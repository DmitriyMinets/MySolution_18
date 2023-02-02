namespace Ex_1;

internal class Program
{
    private static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();

        ThreadPool.QueueUserWorkItem(_ => Count(cts.Token, 50));

        Console.WriteLine(@"Нажмите  <Enter> для завершения операции");

        Console.ReadLine();

        cts.Cancel();
    }

    public static void Count(CancellationToken token, int countTo)
    {
        for (var i = 0; i < countTo; i++)
        {
            if (token.IsCancellationRequested) break;

            Console.WriteLine(i);
            Thread.Sleep(200);
        }
    }
}