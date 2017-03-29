using LaunchIt.Core;
using LaunchIt.Data;
using LaunchIt.Helper;
using LaunchIt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LaunchIt
{
    class MainViewModel : ViewModelBase
    {
        SettingsViewModel _settingsVM;
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged("SearchText"); UpdateList(); }
        }

        private List<FileDetail> _files = new List<FileDetail>();

        public List<FileDetail> Files
        {
            get { return _files; }
            set { _files = value; OnPropertyChanged("Files"); }
        }

        DataHelper dataHelper;
        public MainViewModel()
        {
            dataHelper = new DataHelper();
            _settingsVM = new SettingsViewModel(dataHelper);
        }

        void UpdateList()
        {
            if (!String.IsNullOrWhiteSpace(SearchText))
                Files = dataHelper.FileDetailList
                    .Where(f => f.Name.ToLower().Contains(_searchText.ToLower()))
                    .OrderByDescending(f => f.UsedCount)
                    .Take(8)
                    .ToList();
            else
                Files = new List<FileDetail>();

            SelectedItem = Files.FirstOrDefault();
            SelectedIndex = 0;
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged("SelectedIndex"); }
        }

        private FileDetail _selectedItem;
        public FileDetail SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        private ICommand _aboutCommmand;
        public ICommand AboutCommand
        {
            get
            {
                if (_aboutCommmand == null)
                {
                    _aboutCommmand = new DelegateCommand(p =>
                    {
                        var _settingsWindow = new SettingsWindow();
                        _settingsWindow.Owner = p as MainWindow;
                        _settingsWindow.DataContext = _settingsVM;
                        _settingsWindow.ShowDialog();
                    });
                }
                return _aboutCommmand;
            }
        }

        internal void MovePrevious()
        {
            SelectedIndex = GetNewIndex(SelectedIndex - 1);
        }

        internal void MoveNext()
        {
            SelectedIndex = GetNewIndex(SelectedIndex + 1);
        }

        int GetNewIndex(int idx)
        {
            var count = Files.Count;

            if (count > 0)
            {
                return (count + idx) % count;
            }

            return -1;
        }

        internal void LaunchSelected()
        {
            var itemToLaunch = SelectedItem ?? Files.FirstOrDefault();
            if (itemToLaunch != null)
            {
                Launcher.Launch(itemToLaunch);

                var usageDetail = dataHelper.UsageStatistics.FirstOrDefault(u => string.Equals(u.FullPath, itemToLaunch.FilePath, StringComparison.OrdinalIgnoreCase));
                if (usageDetail == null)
                {
                    dataHelper.UsageStatistics.Add(new UsageDetail(itemToLaunch.FilePath, 1));
                }
                else
                {
                    usageDetail.UsageCount += 1;
                }

                dataHelper.SaveUsageStatitics();
            }
        }
    }
}
