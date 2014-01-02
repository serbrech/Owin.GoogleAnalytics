using System;
using System.Collections.Generic;
using Microsoft.Owin;
using System.IO;
using System.Threading.Tasks;

namespace Owin.GoogleAnalytics
{
	public class GoogleAnalyticsMiddleware
	{
		private string _tracker;

		Func<IDictionary<string, object>, Task> _next;

		public GoogleAnalyticsMiddleware(Func<IDictionary<string, object>, Task> next, string tracker)
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

		public async Task Invoke(IDictionary<string, object> environment)
		{
			if (environment == null)
			{
				throw new ArgumentNullException("environment");
			}

			var context = new OwinContext(environment);
			var requestBodyStream = context.Request.Body ?? Stream.Null;
			var responseBodyStream = context.Response.Body;

			await _next(environment);
		}
	}
}
