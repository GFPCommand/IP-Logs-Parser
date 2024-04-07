using CommandLine;
using System.Text.Json.Serialization;

namespace IP_Parser
{
	public class Options
	{
		[Option(longName: "file-log", Required = true, HelpText = "File log path")]
		public string FileLog { get; set; }

		[Option(longName: "file-out", Required = true, HelpText = "File out path")]
		public string FileOut { get; set; }

		[Option(longName: "address-start", Required = false, HelpText = "IP address start value")]
		public string AddressStart { get; set; } = string.Empty;

		[Option(longName: "address-mask", Required = false, HelpText = "IP address mask")]
		public int AddressMask { get; set; } = -1;

		[Option(shortName: 'c', longName: "config", Required = false, HelpText = "Set config file path (JSON)")]
		[JsonIgnore]
		public string ConfigFile { get; set; }
    }
}