using CommandLine;
using IP_Parser;

Options opts = new();
FileIOManager manager;

int mask = 32;

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

if (opts.AddressStart.Split('.').Length > 4)
{
	Console.WriteLine("IPv4 address cannot contains more than 4 values");
	return;
}

if (!string.IsNullOrEmpty(opts.AddressStart) && opts.AddressStart.Split('.').Length < 4)
{
	Console.WriteLine("IPv4 address cannot contains less than 4 values");
	return;
}

if (!File.Exists(opts.FileLog))
{
	Console.WriteLine("Log file not found!");
	return;
}

if ((opts.AddressMask >= 0 && opts.AddressMask <= 32))
	mask = opts.AddressMask;
else if (opts.AddressMask == -1)
	mask = 32;
else
	Console.WriteLine("Error mask value. Program will use default value: 32.");

manager = new(opts.AddressStart, mask);

manager.Read_IP(opts.FileLog);
manager.Write_IP(opts.FileOut);