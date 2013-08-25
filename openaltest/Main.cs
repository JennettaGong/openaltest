using System;
using OpenTK;
using OpenTK.Audio;

namespace openaltest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			uint helloBuffer,helloSource;
			Alut.Init ();
			helloBuffer = Alut.CreateBufferFromFile("/home/carrotsoft/For_You.wav");
			AL.GenSources (1,out helloSource);
			AL.Source (helloSource,ALSourcei.Buffer,(int)helloBuffer);
			AL.SourcePlay(helloSource);
			Console.WriteLine("AL.SourcePlay(...) ran.");
			System.Threading.Thread.Sleep(5000);
			
			
		}
	}
}
