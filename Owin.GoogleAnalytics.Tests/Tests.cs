using System;
using NUnit.Framework;
using System.Net.Http;
using Microsoft.Owin.Testing;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace Owin.GoogleAnalytics.Tests
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void Return200()
		{
			var client = CreateHttpClient ();
			var response =	client.GetAsync("http://localhost/");
			var result = response.GetAwaiter ().GetResult ();
			result.StatusCode.Should ().Be (HttpStatusCode.OK);
		}

		private HttpClient CreateHttpClient()
		{
			return TestServer.Create(builder => 
				builder
				.Use<GoogleAnalyticsMiddleware>("ABCD")
				.Run(ctx => {
					return ctx.Response.WriteAsync("<html><body></body></html>");
			})).HttpClient;
		}
	}
}

