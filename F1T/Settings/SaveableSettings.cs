using F1T.Core;
using System;
using System.IO;
using System.Xml.Serialization;

namespace F1T.Settings
{
    public abstract class SaveableSettings : ObservableObject
    {
        protected static string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\F1T\\config\\";
        protected abstract string Filename { get; }


        private bool _toggled;
        /// <summary>
        /// True if Module switch is toggled, False otherwise
        /// </summary>
        public bool Toggled
        {
            get { return _toggled; }
            set { SetField(ref _toggled, value, "Toggled"); }
        }

        private bool _autoToggled;
        public bool AutoToggled
        {
            get { return _autoToggled; }
            set { SetField(ref _autoToggled, value, "AutoToggled"); }
        }

        /// <summary>
        /// Serialize (save) this as type <see cref="{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Save<T>()
        {
            Directory.CreateDirectory(SettingsPath);

            using (StreamWriter sw = new StreamWriter(SettingsPath + Filename))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(T));
                xmls.Serialize(sw, this);
            }
        }

        /// <summary>
        /// Deserialize (read) this as type <see cref="{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><see cref="{T}"/></returns>
        public T Read<T>()
        {
            // Try and read it
            // If the file does not exist,
            // OR has an error in it
            // Regen the file, and then Read() it
            try
            {
                using (StreamReader sw = new StreamReader(SettingsPath + Filename))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(T));
                    return (T)xmls.Deserialize(sw);
                }
            }
            catch (Exception)
            {
                Save<T>();
                return Read<T>();
            }

        }
    }
}
