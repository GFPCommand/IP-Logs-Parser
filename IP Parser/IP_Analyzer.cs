using System.Text;

namespace IP_Parser
{
	public class IP_Analyzer
	{
		private string _ipStart = string.Empty;
		private int _ipMaskNum = 32;
		private string _ipMask = "255.255.255.255";

		int[] adrStartNums = new int[4];
		int[] ipCheckNums = new int[4];
		int[] ipMaskNums = new int[4];

        public IP_Analyzer()
        {
			_ipStart = "0.0.0.0";
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
			var adrStart = _ipStart.Split('.');
			var ipCheck  = ip.Split('.');
			var mask = _ipMask.Split('.');

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

			if (ipCheckNums[0] < adrStartNums[0]) return false;
			if (ipCheckNums[1] < adrStartNums[1]) return false;
			if (ipCheckNums[2] < adrStartNums[2]) return false;
			if (ipCheckNums[3] < adrStartNums[3]) return false;

			return true;
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