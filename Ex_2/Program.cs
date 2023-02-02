namespace Ex_2;

internal class Program
{
    private static void Main(string[] args)
    {
        var waitTask = new Task(() => Wait(3000));

        var printTask = waitTask.ContinueWith(Print);

        waitTask.Start();

        printTask.Wait();

        Thread.Sleep(1000);
        Console.WriteLine("Какая-то задача метода мейн");
    }

    //static async Task Main()
    //{
    //    await WaitAsync(2000);
    //}

    private static async Task WaitAsync(int milliseconds)
    {
        await Task.Delay(milliseconds);
        Console.WriteLine("Hello from callback");
    }

    private static void Wait(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }

    private static void Print(Task t)
    {
        Console.WriteLine("Hello from callback");
    }
}