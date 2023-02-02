using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_3
{
    internal class Pizzeria
    {
        private int _amtWorker;
        private SemaphoreSlim _semaphore;
        Task[] _tasks;
        public Pizzeria(int amtWorker)
        {
            _amtWorker = amtWorker;
            _semaphore = new SemaphoreSlim(amtWorker);
            _tasks = new Task[amtWorker];
        }

        public async Task MakingPizzaAsync(int amountPizza)
        {
            while (amountPizza > 0)
            {
                for (int i = 0; i < _tasks.Length; i++)
                {
                    _tasks[i] = MakingPizza();
                    await _tasks[i];
                }
                amountPizza -= _amtWorker;
            }
        }

        async Task MakingPizza()
        {
            Console.WriteLine("Приготовление пиццы началось"); 
            await Task.Delay(2000);
            Console.WriteLine("Пицца готова");
        }
    }
}
