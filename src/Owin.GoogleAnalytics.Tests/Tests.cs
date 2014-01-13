using System;
using NUnit.Framework;
using System.Net.Http;
using Microsoft.Owin.Testing;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using FluentAssertions.Primitives;

namespace Owin.GoogleAnalytics.Tests
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public async Task status_code_is_200()
		{
			var client = CreateHttpClient ();
			var response = await client.GetAsync("http://localhost/");
			response.StatusCode.Should ().Be (HttpStatusCode.OK);
		}

		[Test]
		public async Task embed_tracking_code_at_the_end_of_html_body()
		{
			var client = CreateHttpClient ();
			var response = await client.GetAsync("http://localhost/");
			var content = await response.Content.ReadAsStringAsync ();
			content.Should ().EmbedTrackingCode("UA-0000000-1");
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

