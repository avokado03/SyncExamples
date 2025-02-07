namespace MutexExample
{
	internal class Program
	{
		static void Main(string[] args)
		{
			bool firstInstance;
			Mutex mutex = new Mutex(false, "my_named_mutex", out firstInstance);
			if (!firstInstance)
			{
				Console.WriteLine("Уже запущена копия приложения!");
				Console.ReadKey();
			}
			else
			{
				int x;
				Mutex local_mutex = new Mutex();
				object locker = new object();
				for (int i = 1; i < 6; i++)
				{
					Thread thread = new Thread(new ThreadStart(Func));
					thread.Name = i.ToString();
					thread.Start();
				}
				Console.ReadKey();
				void Func()
				{
					local_mutex.WaitOne();
					x = 0;
					for (int i = 0; i < 10; i++)
					{
						x++;
						Console.WriteLine($"Поток {Thread.CurrentThread.Name} вывел число {x}");
						Thread.Sleep(100);
					}
					local_mutex.ReleaseMutex();
				}
			}
			GC.KeepAlive(mutex);
		}
	}
}
