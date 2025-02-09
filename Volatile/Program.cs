namespace VolatileKeywordDemo
{
	// RELEASE x86
	//it will enter into the loop, after 20 milliseconds it will set the _loop variable value to false.
	//But even after the loop value is set to False, the while loop is not exited.
	//That means the thread (thread1) is still thinking that the _loop variable value is True.
	//It means the value that we set inside the Main method (setting _loop variable to False)
	//is not getting reflected inside the thread1(i.e.inside the SomeMethod).
	class Program
	{
		//Loop Varible
		private volatile bool _loop = true;
		static void Main(string[] args)
		{
			//Calling the SomeMethod in a Multi-threaded manner
			Program obj1 = new Program();
			Thread thread1 = new Thread(SomeMethod);
			thread1.Start(obj1);
			//Pauses for 200 MS
			Thread.Sleep(200);
			//Setting the _loop value as false
			obj1._loop = false;
			Console.WriteLine("Step2:- _loop value set to False");
			Console.ReadKey();
		}
		//Simple Method
		public static void SomeMethod(object obj1)
		{
			Program obj = (Program)obj1;
			Console.WriteLine("Step1:- Entered into the Loop");
			while(obj._loop)
			{
			}
			Console.WriteLine("Step3:- Existed From the Loop");
		}
    }
}
