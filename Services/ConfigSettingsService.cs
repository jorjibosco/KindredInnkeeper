using System.IO;
using System.Text.Json;
using Stunlock.Core;

namespace KindreInnkeeper.Services;
internal class ConfigSettingsService
{
	private static readonly string CONFIG_PATH = Path.Combine(BepInEx.Paths.ConfigPath, KindredInnkeeper.MyPluginInfo.PLUGIN_NAME);
	private static readonly string SETTINGS_PATH = Path.Combine(CONFIG_PATH, "settings.json");

	struct Config
	{
		public Config()
		{
			Info =
				[
				"<color=#00FFBB>Welcome to the Inn!</color>",
				"<color=#00FFBB>1-</color> Use <color=#00FFBB>.inn help</color> to view Inn commands.",
				"<color=#00FFBB>2-</color> Inn is designed for progression up to <color=#00FFBB>level 50</color>.",
				"<color=#00FFBB>3-</color> Do not team up with Inn clan members besides your duo.",
				"<color=#00FFBB>4-</color> Do not leave items unattended or steal from shared stations.",
				"<color=#00FFBB>5-</color> Claiming a plot kicks you from the Inn. Your storage will follow.",
				"<color=#00FFBB>6-</color> Leaving the clan will forfeit any items."
				];
		}

			public string[] Info { get; set; }
		
	}

	Config config;

	public string[] InnInfo
	{
		get
		{
			return config.Info;
		}
		set
		{
			config.Info = value;
			SaveConfig();
		}
	}

	public ConfigSettingsService()
	{
		LoadConfig();
	}

	public void LoadConfig()
	{
		if (!File.Exists(SETTINGS_PATH))
		{
			config = new Config();
			SaveConfig();
			return;
		}

		var json = File.ReadAllText(SETTINGS_PATH);
		config = JsonSerializer.Deserialize<Config>(json);
	}

	void SaveConfig()
	{
		if(!Directory.Exists(CONFIG_PATH))
			Directory.CreateDirectory(CONFIG_PATH);
		var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });
		File.WriteAllText(SETTINGS_PATH, json);
	}

}
