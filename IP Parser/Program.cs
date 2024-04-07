using CommandLine;
using IP_Parser;

Options opts = new();
FileIOManager manager = new();

Parser.Default.ParseArguments<Options>(args)
	.WithParsed(o =>
	{
		opts = o;
	});

if (!string.IsNullOrEmpty(opts.ConfigFile))
{
	if (File.Exists(opts.ConfigFile))
	{
		ConfigLoader config = new(opts.ConfigFile);

		opts = config.ReadConfiguration();
	}
	else
	{
		Console.WriteLine("Config file not found!");
	}
}

if (string.IsNullOrEmpty(opts.AddressStart) && opts.AddressMask != -1)
{
	Console.WriteLine("You're using mask without --address-start option");
	return;
}

if (!File.Exists(opts.FileLog))
{
	Console.WriteLine("Log file not found!");
	return;
}

if (!string.IsNullOrEmpty(opts.AddressStart))
	manager.analyzer.IPStart = opts.AddressStart;

if (opts.AddressMask >= 0 && opts.AddressMask <= 32)
{
	manager.analyzer.IPMask = opts.AddressMask;
}

manager.Read_IP(opts.FileLog);
manager.Write_IP(opts.FileOut);