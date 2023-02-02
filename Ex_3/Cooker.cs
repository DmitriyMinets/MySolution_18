using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_3
{
    public class Cooker
    {
        private static SemaphoreSlim sem;
        private Task[] task;
        private int _countCooker;

        public Cooker(int countCooker)
        {
            _countCooker = countCooker;
            sem = new SemaphoreSlim(countCooker);
            task = new Task[countCooker];
        }

        public async void Show(int countPizza)
        {
            while (countPizza > 0)
            {
                for (int i = 0; i < task.Length; i++)
                {
                    await CreatePizzaAsycn();
                }

                sem.Wait();
                sem.Release();
                countPizza--;
            }
        }

        async Task CreatePizzaAsycn()
        {
            Console.WriteLine("Готовка пиццы началась");
            await Task.Run(() => CreatePizza());
            Console.WriteLine("Пицца приготовилась");
        }
        private void CreatePizza()
        {
            Thread.Sleep(2000);
        }
    }
}
