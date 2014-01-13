using System;
using System.Collections.Generic;
using Microsoft.Owin;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace Owin.GoogleAnalytics
{
	public class GoogleAnalyticsMiddleware : OwinMiddleware
	{
		private string _tracker;

		OwinMiddleware _next;
		string trackingCode = "<script>var _gaq=[['_setAccount','UA-000001'],['_trackPageview']];(function(d,t){var g=d.createElement(t),s=d.getElementsByTagName(t)[0];g.src='//www.google-analytics.com/ga.js';s.parentNode.insertBefore(g,s)}(document,'script'))</script>";
		public GoogleAnalyticsMiddleware(OwinMiddleware next, string tracker) : base(next)
		{

			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (string.IsNullOrWhiteSpace(tracker))
			{
				throw new ArgumentNullException("tracker");
			}

			_tracker = tracker;
			_next = next;
		}

		public override async Task Invoke(IOwinContext context)
		{
			context.Response.OnSendingHeaders (async state => {
				try {
					var responseBodyStream = ((IOwinResponse)state).Body;
					var memory = new MemoryStream();
					await responseBodyStream.CopyToAsync(memory);
					string body;
					using (var sr = new StreamReader(memory)){
						body = await sr.ReadToEndAsync();
					}
					var bodyPosition = body.IndexOf ("</body>");
					body.Insert (bodyPosition,trackingCode);
					responseBodyStream.Position = 0;
					var newBodyBuffer = Encoding.UTF8.GetBytes (body);
					await responseBodyStream.WriteAsync (newBodyBuffer, 0, body.Length);
				} catch (Exception ex) {
					Console.WriteLine (ex);
				}

			}, context.Response);

			await _next.Invoke(context);
		}
	}
}
