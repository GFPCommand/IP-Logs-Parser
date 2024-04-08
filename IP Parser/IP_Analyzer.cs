using System.Text;

namespace IP_Parser
{
	public class IP_Analyzer
	{
		private string _ipStart = string.Empty;
		private string _ipMask = string.Empty;
		private int _ipMaskNum = 32;

		int[] adrStartNums = new int[4];
		int[] ipCheckNums = new int[4];
		int[] ipMaskNums = new int[4];

        public IP_Analyzer()
        {
			_ipStart = "0.0.0.0";
			_ipMask = "255.255.255.255";
		}

		public string IPStart 
		{ 
			set {
				_ipStart = value;
			} 
		}

		public int IPMask
		{
			set
			{
				_ipMaskNum = value;

				_ipMask = IP_Mask();
			}
		}

		public bool IP_Compare(string ip)
		{
			//TODO: вынести расчет для начального адреса и маски
			var adrStart = _ipStart.Split('.');
			var ipCheck  = ip.Split('.');

			if (ipCheck.Length > 4) return false;

			var mask = _ipMask.Split('.');

			int byteCount = 3;

			long ipAddrStart = 0;
			long ipAddr = 0;
			long maskAddr = 0;

			for (int i = 0; i < 4; i++)
			{
				if (!int.TryParse(adrStart[i], out adrStartNums[i]))
				{
					Console.WriteLine($"Error reading {i + 1} byte of IP start address.\nPlease, check correctness of {_ipStart}");
					return false;
				}
				if (!int.TryParse(ipCheck[i], out ipCheckNums[i]))
				{
					Console.WriteLine($"Error reading {i + 1} byte of IP address.\nPlease, check correctness of {ip}");
					return false;
				}
				if (!int.TryParse(mask[i], out ipMaskNums[i]))
				{
					Console.WriteLine($"Error reading {i + 1} byte of IP address mask.\nPlease, check correctness of {_ipMask}");
					return false;
				}
			}

			for (int i = 0; i < 4; i++)
			{
				ipAddrStart += adrStartNums[i] * (long)Math.Pow(256, byteCount - i);
			}

			for (int i = 0; i < 4; i++)
			{
				ipAddr += ipCheckNums[i] * (long)Math.Pow(256, byteCount - i);
			}

			for (int i = 0; i < 4; i++)
			{
				maskAddr += ipMaskNums[i] * (long)Math.Pow(256, byteCount - i);
			}

			if (ipAddr >= ipAddrStart && ipAddr <= maskAddr) return true;

			return false;
		}

		private string IP_Mask()
		{
			StringBuilder byteMask = new();

			StringBuilder resultMask = new();

			for (int i = 0; i < 32; i++)
			{
				if (i % 8 == 0) byteMask.Append('.');
				if (i < _ipMaskNum) byteMask.Append('1');
				else byteMask.Append('0');
			}

			byteMask.Remove(0, 1);

			var splitMask = byteMask.ToString().Split('.');

			for (int i = 0; i < splitMask.Length; i++)
			{
				resultMask.Append($"{NumericalConverter.BinaryToDecimal(splitMask[i])}.");
			}

			resultMask.Remove(resultMask.Length - 1, 1);

			return resultMask.ToString();
		}
    }
}