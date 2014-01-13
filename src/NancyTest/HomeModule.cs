using System;

namespace NancyTest
{
	public class HomeModule : Nancy.NancyModule
	{
		public HomeModule ()
		{
			Get ["/home"] = _ => "<html><body><h1>Hello</h1></body></html>";
		}
	}
}

