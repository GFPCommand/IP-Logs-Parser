using CommandLine;
using IP_Parser;

Options opts = new();

Parser.Default.ParseArguments<Options>(args)
	.WithParsed(o =>
	{
		opts = o;
	})
	.WithNotParsed(e =>
	{
		
	});

