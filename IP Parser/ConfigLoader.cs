using System.Text.Json;

namespace IP_Parser
{
	public class ConfigLoader
	{
        private string _configPath = string.Empty;

        public ConfigLoader(string path)
        {
            _configPath = path;
        }

        public Options ReadConfiguration()
        {
            string data = File.ReadAllText(_configPath);

            return JsonSerializer.Deserialize<Options>(data)!;

		}
    }
}
