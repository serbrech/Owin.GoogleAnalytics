using System;
using Microsoft.Owin.Hosting;
using Owin;

namespace NancyTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var url = "http://+:8080";

			using (WebApp.Start<Startup> (url)) {
				Console.WriteLine ("Running on http://localhost:8080", url);
				Console.WriteLine ("Press enter to exit");
				Console.ReadLine ();
			}
		}

	}

	public class Startup{
		public void Configuration(IAppBuilder app)
		{

			//app.UseWelcomePage ();
			app.Use<Owin.GoogleAnalytics.GoogleAnalyticsMiddleware> ("UA-0000001");
			app.UseNancy();
		}
	}
}
