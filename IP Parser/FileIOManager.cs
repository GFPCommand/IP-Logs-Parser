namespace IP_Parser
{
	public class FileIOManager
	{
		private Dictionary<string, int> ips = [];

		public IP_Analyzer analyzer = new();

		public void Read_IP(string fileLog)
		{
			foreach (var item in File.ReadLines(fileLog))
			{
				var ip_address_info = item.Split(' ');

				if (analyzer.IP_Compare(ip_address_info[0]))
				{
					if (ips.ContainsKey(ip_address_info[0]))
					{
						ips[ip_address_info[0]]++;
					}
					else ips.Add(ip_address_info[0], 1);
				}
			}
		}

		public void Write_IP(string fileOut)
		{
			using StreamWriter writer = new(fileOut);
			foreach (var item in ips)
			{
				writer.WriteLine($"{item.Key} : {item.Value}");
			}
		}
	}
}