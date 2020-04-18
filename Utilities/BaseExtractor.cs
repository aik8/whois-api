using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace KowWhoisApi.Utilities
{
	class BaseExtractor
	{
		public string Input { get; private set; }
		public string BaseDomain { get; private set; }
		static List<string> tlds = new List<string> { "gr", "ελ", "xn--qxam" };
		private List<string> sub_tlds = new List<string> { "com", "net", "edu", "gov", "mil", "mod", "co" };

		public BaseExtractor(string input)
		{
			Input = input;
		}

		private string extractBase(string input)
		{
			// Split the parts.
			var parts = input.Split('.');

			// Get the length for future reference.
			int num_parts = parts.Length;

			// Determine the TLD, which surely consists of the the last
			// part and maybe the next to last.
			var tld = parts[num_parts - 1];

			return "";
		}
	}
}
