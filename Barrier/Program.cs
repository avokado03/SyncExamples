namespace BarrierExample
{
	// <href>https://www.albahari.com/threading/part4.aspx</href>
	internal class Program
	{
		// with post-phase action
		static Barrier _barrier = new Barrier (3, barrier => Console.WriteLine());

		static void Main()
		{
			new Thread(Speak).Start();
			new Thread(Speak).Start();
			new Thread(Speak).Start();
		}

		static void Speak()
		{
			for (int i = 0; i < 5; i++)
			{
				Console.Write(i + " ");
				_barrier.SignalAndWait();
			}
		}
	}
}
