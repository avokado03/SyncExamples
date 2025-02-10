namespace InterlockedExample
{
	internal class Program
	{
		static void Main(string[] args)
		{
			WithoutInterlocked();
			Console.WriteLine();
			WithInterlocked();
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
	}
}
