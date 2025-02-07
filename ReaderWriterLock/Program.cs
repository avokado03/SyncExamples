namespace ReaderWriterLockExample
{
	internal class Program
	{
		/// <summary>
		/// ReaderWriterLockSlim is similar to ReaderWriterLock, 
		/// but it has simplified rules for recursion and for upgrading and downgrading lock state. 
		/// ReaderWriterLockSlim avoids many cases of potential deadlock. 
		/// In addition, the performance of ReaderWriterLockSlim is significantly better than ReaderWriterLock. 
		/// ReaderWriterLockSlim is recommended for all new development.
		/// </summary>
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
