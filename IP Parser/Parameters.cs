namespace IP_Parser
{
	class Parameters
	{
		public string FileLog { get; set; }
		public string FileOut { get; set; }
		public short AddressStart { get; set; } = 0;
		public short AddressMask { get; set; } = 255;
    }
}
