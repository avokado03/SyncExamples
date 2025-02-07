namespace Semaphore_SemaphoreSlim
{
	internal class Visitor
	{
		//Здесь вся функциональность сосредоточена в классе Visitor. 
		//Так как семафор должны видеть все потоки, то поле _semaphore объявлено как static.

		/// <summary>
		/// The SemaphoreSlim is a lightweight alternative to the Semaphore class that doesn't use Windows kernel semaphores. 
		/// Unlike the Semaphore class, the SemaphoreSlim class doesn't support named system semaphores. 
		/// You can use it as a local semaphore only. 
		/// The SemaphoreSlim class is the recommended semaphore for synchronization within a single app.
		/// </summary>
		static Semaphore _semaphore = new Semaphore(3, 3);
		private readonly Thread myThread;
		public Visitor(int i)
		{
			myThread = new Thread(Fun)
			{
				Name = $"Посетитель #{i}"
			};
			myThread.Start();
		}
		public static void Fun()
		{
			_semaphore.WaitOne();
			Console.WriteLine($"{Thread.CurrentThread.Name} входит в клуб");
			Console.WriteLine($"{Thread.CurrentThread.Name} веселится");
			Thread.Sleep(1000);
			Console.WriteLine($"{Thread.CurrentThread.Name} покидает клуб");
			_semaphore.Release();
		}
	}
}
