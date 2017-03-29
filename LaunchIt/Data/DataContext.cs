using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    class DataContext
    {
        const string DB_FILE_NAME = "LaunchIt.xml";
        const string SETTINGS_FILE_NAME = "Settings.xml";

        private Data _data;// = new Data();
        private Settings _settings;// = new Settings();

        public Settings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = LoadSettings();
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
                if (_data == null)
                    _data = LoadData();
                return _data.Files;
            }
            set
            {
                _data.Files = value;
            }
        }


        private Settings GetFactorySettings()
        {
            var settings = new Settings();
            settings.SourcePaths = new List<SourcePath>() { 
                new SourcePath() { Path = @"%SystemRoot%/system32", Types = "*.bat;*.lnk;*.exe", RecursiveSearch = false },
                new SourcePath() { Path = @"%APPDATA%\Microsoft\Internet Explorer\Quick Launch", Types = "*.lnk", RecursiveSearch = true },
                new SourcePath() { Path = @"%PROGRAMDATA%\Microsoft\Windows\Start Menu", Types = "*.lnk", RecursiveSearch = true },
            };

            return settings;
        }



        private Settings LoadSettings()
        {
            if (!File.Exists(SETTINGS_FILE_NAME))
            {
                Save(GetFactorySettings(), SETTINGS_FILE_NAME);
            }

            return Load<Settings>(SETTINGS_FILE_NAME);
        }

        private Data LoadData()
        {
            if (!File.Exists(DB_FILE_NAME))
            {
                Save(new Data { Files = new List<FileDetail>() }, DB_FILE_NAME);
            }

            return Load<Data>(DB_FILE_NAME);
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

        internal bool SaveData()
        {
            var dataInDisc = LoadData();
            if (!_data.Equals(dataInDisc))
                return Save(_data, DB_FILE_NAME);
            return true;
        }

        internal bool SaveSettings()
        {
            var settings = LoadSettings();
            if (!_settings.Equals(settings))
                return Save(_settings, SETTINGS_FILE_NAME);
            return true;
        }
    }
}
