using System.Text;

namespace IP_Parser
{
	public class IP_Analyzer
	{
		private string _ipStart = string.Empty;
		private string _ipMask = string.Empty;
		private int _ipMaskNum = 32;

		private string[] _adrStart = new string[4];
		private string[] _mask = new string[4];

		private int[] adrStartNums = new int[4];
		private int[] ipCheckNums = new int[4];
		private int[] ipMaskNums = new int[4];

		private readonly int byteCount = 3;

		private long ipAddrStart = 0;
		private long maskAddr = 0;
		private long ipAddr = 0;

		private readonly string baseAddress = "0.0.0.0";

        public IP_Analyzer(string? ipStart, int ipMask)
        {
			_ipStart = string.IsNullOrEmpty(ipStart) ? baseAddress : (ipStart.Split('.').Length > 4 ? baseAddress : ipStart);
			_ipMaskNum = ipMask;

			_ipMask = IP_Mask();

			_mask = _ipMask.Split('.');

			_adrStart = _ipStart.Split('.');

			FillAddressMaskAddress();
		}

		public bool IP_Compare(string ip)
		{
			var ipCheck  = ip.Split('.');

			if (ipCheck.Length > 4) return false;

			ipAddr = 0;

			for (int i = 0; i < 4; i++)
			{
				if (!int.TryParse(ipCheck[i], out ipCheckNums[i]))
				{
					Console.WriteLine($"Error reading {i + 1} byte of IP address.\nPlease, check correctness of {ip}");
					return false;
				}
			}

			for (int i = 0; i < 4; i++)
			{
				ipAddr += ipCheckNums[i] * (long)Math.Pow(256, byteCount - i);
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

		private void FillAddressMaskAddress()
		{
			for (int i = 0; i < 4; i++)
			{
				if (!int.TryParse(_adrStart[i], out adrStartNums[i]))
				{
					Console.WriteLine($"Error reading {i + 1} byte of IP start address.\nPlease, check correctness of {_ipStart}");
				}
				if (!int.TryParse(_mask[i], out ipMaskNums[i]))
				{
					Console.WriteLine($"Error reading {i + 1} byte of IP address mask.\nPlease, check correctness of {_ipMask}");
				}
			}

			for (int i = 0; i < 4; i++)
			{
				ipAddrStart += adrStartNums[i] * (long)Math.Pow(256, byteCount - i);
			}

			for (int i = 0; i < 4; i++)
			{
				maskAddr += ipMaskNums[i] * (long)Math.Pow(256, byteCount - i);
			}
		}
    }
}