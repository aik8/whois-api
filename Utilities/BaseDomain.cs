using System.Collections.Generic;
using System.Globalization;
using System;
using System.Text.RegularExpressions;

namespace GrWhoisApi.Utilities
{
	/// <summary>
	/// Represents a base domain, extracted from a provided hostname.
	/// </summary>
	class BaseDomain
	{
		/// <summary>
		/// A list of valid Greek TLDs.
		/// </summary>
		private static List<string> GreekTlds = new List<string> { "gr", "ελ" };

		/// <summary>
		/// A list of valid Greek sub-TLDs.
		/// </summary>
		private static List<string> GreekSubTlds = new List<string> { "com", "net", "edu", "gov", "mil", "mod", "co" };

		public string Value { get; private set; }
		public bool IsValid { get => Value != null; }

		public override string ToString() => Value;

		public BaseDomain(string hostname)
		{
			// Check if there is actually input.
			if (String.IsNullOrEmpty(hostname))
			{
				throw new ArgumentNullException("input", "Expected some input here.");
			}

			// Validate and convert the input to a full, Unicode hostname.
			Value = ToUnicode(hostname);

			// If the hostname was valid, extract the base domain from it.
			if (IsValid) Value = ExtractBase(Value);
		}

		/// <summary>
		/// Converts the given domain name to Unicode.
		/// </summary>
		/// <param name="domain">A domain name (or hostname) in Unicode or Punycode.</param>
		/// <returns>A string containing the Unicode domain or null, if the input was invalid.</returns>
		private string ToUnicode(string domain)
		{
			IdnMapping idn = new IdnMapping();
			string unicode;

			try
			{
				// Convert to Punycode and then back to Unicode. This maneuver
				// ensures that the input is converted to Unicode, as the Greek
				// registry does not accept Punycode as input.
				var puny = idn.GetAscii(domain);
				unicode = idn.GetUnicode(puny);
			}
			catch (ArgumentException)
			{
				// Uh-oh! Something's wrong here.
				return null;
			}

			// Mitigate monkey-typing effects.
			bool containsWhitespace = Regex.IsMatch(unicode, @"\s+");

			// If the input does not pass the final test, leave it.
			if (containsWhitespace) return null;

			// We've gotten so far. Let's face it, it's valid.
			return unicode;
		}

		/// <summary>
		/// Extract the base domain from the given hostname.
		/// </summary>
		/// <param name="input">The input from which the base domain extraction will be attempted.</param>
		/// <returns>The resulting base domain.</returns>
		private string ExtractBase(string input)
		{
			// Split the parts.
			var parts = input.Split('.');

			// Get the length for future reference.
			int length = parts.Length;

			// Is it an actual domain, or just the TLD?
			if (length < 2) return null;

			// Determine the TLD, which surely consists of the the last
			// part and maybe the next to last.
			var tld = parts[length - 1];

			// Is it Greek?
			if (!IsGreekTld(tld)) return null;

			// Does it use a subTLD?
			var subtld = parts[length - 2];
			bool sub = IsGreekSubTld(subtld);

			// Take the necessary parts to build the base domain.
			return sub
			? String.Join('.', new string[] { parts[length - 3], subtld, tld })
			: String.Join('.', new string[] { subtld, tld });
		}

		/// <summary>
		/// Determines if the given part is a Greek TLD.
		/// </summary>
		/// <param name="part">The part to be checked.</param>
		/// <returns>True or False</returns>
		private bool IsGreekTld(string part)
		{
			bool is_gr = false;
			foreach (var tld in GreekTlds) is_gr ^= tld.Equals(part);
			return is_gr;
		}

		/// <summary>
		/// Determines if the given part is a Greek subTLD.
		/// </summary>
		/// <param name="part">The part to be checked.</param>
		/// <returns>True or False</returns>
		private bool IsGreekSubTld(string part)
		{
			bool is_sub = false;
			foreach (var sub in GreekSubTlds) is_sub ^= sub.Equals(part);
			return is_sub;
		}
	}
}
