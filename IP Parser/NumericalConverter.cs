namespace IP_Parser
{
	public static class NumericalConverter
	{
		public static short BinaryToDecimal(string value)
		{
			short result = 0;

			string revertValue = new(value.Reverse().ToArray());

			for (int i = 0; i < value.Length; i++)
			{
				if (revertValue[i].Equals('1')) result += (short)Math.Pow(2, i);
			}

			return result;
		}
	}
}
