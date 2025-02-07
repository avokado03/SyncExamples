namespace LockMonitor
{
	internal class Program
	{
		static object locker = new object();

		static int value = 0;

		static void Main(string[] args)
		{

			for (int i = 0; i < 4; i++)
			{
				var thread1 = new Thread(Increment4);
				thread1.Name = $"Thread [{i}]";
				thread1.Start();
			}

			Console.ReadKey();
		}

		//Race conditions without lock
		static void Increment()
		{
			lock (locker)
			{
				for (int i = 0; i < 10; i++)
				{
					Console.WriteLine($"{Thread.CurrentThread.Name} - {i.ToString()} - {value}");
					++value;
				}
			}
		}

		// equals lock
		static void Increment2()
		{
			bool acquiredLock = false;
			try
			{
				Monitor.Enter(locker, ref acquiredLock);
				for (int i = 0; i < 10; i++)
				{
					Console.WriteLine($"{Thread.CurrentThread.Name} - {i.ToString()} - {value}");
					++value;
				}
			}
			finally
			{
				if(acquiredLock) 
					Monitor.Exit(locker);
			}
		}

		// without lock 1
		static void Increment3()
		{
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine($"{Thread.CurrentThread.Name} - {i.ToString()} - {value}");
				++value;
			}
		}

		// without lock 2
		static void Increment4()
		{
			int x = 1;
			for (int i = 1; i < 6; i++)
			{
				Console.WriteLine($"{Thread.CurrentThread.Name} - {i.ToString()} - {x}");
				x++;
			}
		}
	}
}
