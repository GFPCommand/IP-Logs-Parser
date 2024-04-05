using CommandLine;

namespace IP_Parser
{
	class Options
	{
		[Option(longName: "file-log", Required = true, HelpText = "File log path")]
		public string FileLog { get; set; }

		[Option(longName: "file-out", Required = true, HelpText = "File out path")]
		public string FileOut { get; set; }

		[Option(longName: "address-start", Required = false, HelpText = "IP address start value")]
		public int AddressStart { get; set; } = 0;

		[Option(longName: "address-mask", Required = false, HelpText = "IP address mask")]
		public int AddressMask { get; set; } = 255;
    }
}