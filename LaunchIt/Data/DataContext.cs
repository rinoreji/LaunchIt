using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    class DataContext
    {
        const string DB_FILE_NAME = "LaunchIt.xml";
        const string SETTINGS_FILE_NAME = "Settings.xml";
        const string USAGE_FILE_NAME = "UsageStatitics.xml";

        private Data _data;// = new Data();
        private Settings _settings;// = new Settings();
        private UsageStatistics _usageStat;

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

        public UsageStatistics UsageStatitics
        {
            get
            {
                if (_usageStat == null)
                    _usageStat = LoadUsageData();
                return _usageStat;
            }
            set { _usageStat = value; }
        }



        private Settings GetFactorySettings()
        {
            var settings = new Settings();
            settings.SourcePaths = new List<SourcePath>() { 
                new SourcePath() { Path = @"%SystemRoot%\system32", Types = "*.bat;*.lnk;*.exe", RecursiveSearch = false },
                new SourcePath() { Path = @"%APPDATA%\Microsoft\Internet Explorer\Quick Launch", Types = "*.lnk", RecursiveSearch = true },
                new SourcePath() { Path = @"%PROGRAMDATA%\Microsoft\Windows\Start Menu", Types = "*.lnk", RecursiveSearch = true },
                new SourcePath() { Path = @"D:\Playground\scripting\bat", Types = "*.bat", RecursiveSearch = true },
            };
            settings.HotKey = new HotKey { ModifierKey = 1, Key = 32 };
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

            var data = Load<Data>(DB_FILE_NAME);
            var usageStat = LoadUsageData();

            usageStat.UsageDetails.ForEach(u =>
            {
                var item = data.Files.FirstOrDefault(f => string.Equals(f.FilePath, u.FullPath, System.StringComparison.OrdinalIgnoreCase));
                if (item != null)
                    item.UsedCount = u.UsageCount;
            });

            return data;
        }

        private UsageStatistics LoadUsageData()
        {
            if (!File.Exists(USAGE_FILE_NAME))
            {
                Save(new UsageStatistics { UsageDetails = new List<UsageDetail>() }, USAGE_FILE_NAME);
            }

            return Load<UsageStatistics>(USAGE_FILE_NAME);
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

        internal bool SaveUsageData()
        {
            var dataInDisc = LoadUsageData();
            if (!dataInDisc.Equals(_usageStat))
                return Save(_usageStat, USAGE_FILE_NAME);

            return true;
        }

        internal bool SaveData()
        {
            var dataInDisc = LoadData();
            if (!dataInDisc.Equals(_data))
                return Save(_data, DB_FILE_NAME);
            return true;
        }

        internal bool SaveSettings()
        {
            var settings = LoadSettings();
            if (!settings.Equals(_settings))
                return Save(_settings, SETTINGS_FILE_NAME);
            return true;
        }
    }
}
