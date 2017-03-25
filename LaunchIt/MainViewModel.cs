using LaunchIt.Core;
using LaunchIt.Data;
using LaunchIt.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaunchIt
{
    class MainViewModel : ViewModelBase
    {
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
        }

        void UpdateList()
        {
            if (!String.IsNullOrWhiteSpace(SearchText))
                Files = dataHelper.GetFileDetailList()
                    .Where(f => f.Name.Contains(_searchText))
                    .OrderBy(f => f.UsageCount)
                    .Take(8)
                    .ToList();
            else
                Files = new List<FileDetail>();

            SelectedItem = Files.FirstOrDefault();
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
            }
        }
    }
}
