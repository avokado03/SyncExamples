namespace ReaderWriterLockExample
{
	internal class Program
	{
		static ReaderWriterLock rwLock = new();
		static List<int> list = new List<int> {1, 2, 3 };

		static void Main(string[] args)
		{
			Parallel.For(0, 10, i => { ReadWrite(); });
			Read(); // 10 points of 4
			Console.ReadLine();
		}

		static void Read()
		{
			rwLock.AcquireReaderLock(TimeSpan.FromSeconds(100));

			try
			{
				Thread.Sleep(TimeSpan.FromSeconds(5));
				Console.WriteLine();
				Console.WriteLine("Thread read sleep...\n");
				foreach (int i in list)
				{
					Console.Write($"{i} ");
				}
				Console.WriteLine();
			}
			finally
			{
				if(rwLock.IsReaderLockHeld) 
					rwLock.ReleaseReaderLock();
			}
		}

		static void Write() 
		{
			rwLock.AcquireWriterLock(TimeSpan.FromSeconds(100));
			try
			{
				Thread.Sleep(TimeSpan.FromSeconds(3));
				Console.WriteLine("Thread write sleep...\n");
				list.Add(4);
			}
			finally
			{
				if( rwLock.IsWriterLockHeld) 
					rwLock.ReleaseWriterLock();
			}
		}

		static void ReadWrite()
		{
			Read();
			Write();
		}
	}
}
