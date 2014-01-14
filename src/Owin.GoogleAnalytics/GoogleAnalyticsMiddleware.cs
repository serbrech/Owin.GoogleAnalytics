using System;
using System.Collections.Generic;
using Microsoft.Owin;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Owin.Logging;

namespace Owin.GoogleAnalytics
{
	public class GoogleAnalyticsMiddleware : OwinMiddleware
	{

		public string Tracker {	get; set; }
		public ILogger Logger {	get; set; }
		public string TrackingCode { get; set; }

		public GoogleAnalyticsMiddleware(OwinMiddleware next, string tracker, ILogger logger) : base(next)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (string.IsNullOrWhiteSpace(tracker))
			{
				throw new ArgumentNullException("tracker");
			}

			Tracker = tracker;
			TrackingCode = string.Format ("<script>var _gaq=[['_setAccount','{0}'],['_trackPageview']];(function(d,t){{var g=d.createElement(t),s=d.getElementsByTagName(t)[0];g.src='//www.google-analytics.com/ga.js';s.parentNode.insertBefore(g,s)}}(document,'script'))</script>", Tracker);
			Logger = logger;

		}

		public override async Task Invoke(IOwinContext context)
		{
			context.Response.Body = new PreBodyTagInjectionStream (TrackingCode, context.Response.Body, Encoding.UTF8, context.Request.Uri.ToString(), Logger);
			await Next.Invoke(context);
		}
	}
}
