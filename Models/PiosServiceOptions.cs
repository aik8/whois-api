namespace KowWhoisApi.Models
{
	public class PiosServiceOptions
	{
		public const string PiosService = "PiosService";

		public int CacheTtl { get; set; }
		public bool Secure { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public string Path { get; set; }
	}
}
