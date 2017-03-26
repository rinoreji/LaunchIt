using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    class DataContext
    {
        const string DB_FILE_NAME = "LaunchIt.xml";
        const string SETTINGS_FILE_NAME = "Settings.xml";

        private Data _data = new Data();
        private Settings _settings = new Settings();

        public Settings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        public List<FileDetail> FileDetailList
        {
            get
            {

                return _data.Files;
            }
            set
            {
                _data.Files = value;
            }
        }


        private Settings GetFactorySettings()
        {
            var sys32 = new SourcePath() { Path = @"%SystemRoot%/system32", Types = "*.bat;*.lnk;*.exe", RecursiveSearch = false };
            var appData = new SourcePath() { Path = @"%APPDATA%", Types = "*.lnk;*.exe", RecursiveSearch = true };

            var settings = new Settings();
            settings.SourcePaths = new List<SourcePath>() { appData, sys32 };

            return settings;
        }



        public Settings LoadSettings()
        {
            if (!File.Exists(SETTINGS_FILE_NAME))
            {
                Save(GetFactorySettings(), SETTINGS_FILE_NAME);
            }

            Settings = Load<Settings>(SETTINGS_FILE_NAME);

            return Settings;
        }

        public Data LoadData()
        {
            if (!File.Exists(DB_FILE_NAME))
            {
                Save(new Data { Files = new List<FileDetail>() }, DB_FILE_NAME);
            }

            _data = Load<Data>(DB_FILE_NAME);
            return _data;
        }

        private T Load<T>(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs))
            {
                var result = (T)serializer.Deserialize(reader);
                return result;
            }
        }

        public bool Save<T>(T data, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            //using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var writer = new StreamWriter(fileName, append: false))
            {
                serializer.Serialize(writer, data);
            }

            return true;
        }

        internal bool SaveFileList()
        {
            return Save(_data, DB_FILE_NAME);
        }

        internal bool SaveSettings()
        {
            return Save(_settings, SETTINGS_FILE_NAME);
        }
    }
}
