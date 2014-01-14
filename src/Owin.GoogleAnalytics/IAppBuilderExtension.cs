using System;
using System.Collections.Generic;
using Microsoft.Owin;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Owin.Logging;

namespace Owin.GoogleAnalytics
{
	public static class IAppBuilderExtension {
		public static IAppBuilder UseGoogleAnalytics(this IAppBuilder appBuilder, string tracker){
			ILogger middlewareLogger = appBuilder.CreateLogger<GoogleAnalyticsMiddleware>();
			return appBuilder.Use<GoogleAnalyticsMiddleware>(tracker, middlewareLogger);
		}
	}
	
}
