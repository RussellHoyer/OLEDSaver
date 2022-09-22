using Newtonsoft.Json;
using OLEDSaver.Models;
using System;
using System.IO;

namespace OLEDSaver.Providers
{
    /// <summary>
    /// Bare bones implementation to load and save settings.
    /// </summary>
    public class ConfigSettingsProvider
    {
        #region Private fields

        private WindowConfigSettingsModel _windowConfigSettings;

        #endregion
        #region Constructors

        public ConfigSettingsProvider()
        {
            _windowConfigSettings = new WindowConfigSettingsModel();
        }

        #endregion
        #region Public Properties

        #endregion
        #region Methods

        public WindowConfigSettingsModel GetWindowConfigSettings()
        {
            return _windowConfigSettings;
        }

        public (bool Success, string Message) LoadConfigSettings(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException(String.Format("Could not find file '{0}'.", filePath));

            string fileData = File.ReadAllText(filePath);
            WindowConfigSettingsModel? windowConfigSettings = JsonConvert.DeserializeObject<WindowConfigSettingsModel>(fileData);
            
            if (windowConfigSettings == null)
                return (false, "Error! Could not deserialize settings data!");

            _windowConfigSettings = windowConfigSettings;
            return (true, "Loaded Window config settings.");
        }

        public (bool Success, string Message) SaveConfigSettings(string filePath, WindowConfigSettingsModel windowConfigSettings)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            ArgumentNullException.ThrowIfNull(windowConfigSettings, nameof(windowConfigSettings));

            try
            {
                string fileData = JsonConvert.SerializeObject(windowConfigSettings, Formatting.Indented);
                File.WriteAllText(filePath, fileData);
                return (true, "Config settings saved!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        #endregion
    }
}
