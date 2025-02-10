namespace InterlockedExample
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("--Increment--");
			WithoutInterlocked();
			Console.WriteLine();
			WithInterlocked();
			Console.WriteLine("\n --Exchange--");
			Exchange();
			Console.ReadLine();
		}

		static void WithoutInterlocked()
		{
			var IncrementValue = 0;
			Parallel.For(0, 100000, _ =>
			{
				//Incrementing the value
				IncrementValue++;
			});
			Console.WriteLine("Expected Result: 100000");
			Console.WriteLine($"Actual Result: {IncrementValue}");
		}

		static void WithInterlocked()
		{
			var IncrementValue = 0;
			Parallel.For(0, 100000, _ =>
			{
				//Incrementing the value
				Interlocked.Increment(ref IncrementValue);
			});
			Console.WriteLine("Expected Result: 100000");
			Console.WriteLine($"Actual Result Interlocked: {IncrementValue}");
		}

		static long x = 0;
		static void Exchange()
		{
			Thread thread1 = new Thread(new ThreadStart(SomeMethod));
			thread1.Start();
			thread1.Join();
			// Written [20]
			Console.WriteLine("Exchange 20: {0}",Interlocked.Exchange(ref x, 20));
			Console.ReadKey();
		}

		static void SomeMethod()
		{
			// Replace x with 20.
			Interlocked.Exchange(ref x, 20);
			// CompareExchange: if x is 20, then change to current DateTime.Now.Day or any integer variable.
			//long result = Interlocked.CompareExchange(ref Program.x, DateTime.Now.Day, 20);
			long result = Interlocked.CompareExchange(ref x, 50, 20);
			// Returns original value from CompareExchange
			Console.WriteLine("Exchange 50: {0}", result);
		}
	}
}
