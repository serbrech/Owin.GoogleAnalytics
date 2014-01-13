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

	public static class ShouldExtension{
		public static AndConstraint<StringAssertions> EmbedTrackingCode(this StringAssertions assertion, string trackingCode){
			string pattern = string.Format(@"<script.*{0}.*</script>\s?</body>", trackingCode);
			return assertion.MatchRegex (pattern);
		}
	}
}
