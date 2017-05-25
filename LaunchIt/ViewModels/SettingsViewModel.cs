using LaunchIt.Data;
using System.Linq;
using System.Windows.Input;

namespace LaunchIt.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        private ICommand _reScanIndex;
        private Helper.DataHelper dataHelper;



        public SettingsViewModel(Helper.DataHelper dataHelper)
        {
            // TODO: Complete member initialization
            this.dataHelper = dataHelper;
        }

        private Settings _settings;

        public Settings SettingsData
        {
            get
            {
                if (_settings == null)
                {
                    _settings = dataHelper.GetSettings();
                }
                return _settings;
            }
            set { _settings = value; }
        }

        private SourcePath _selectedSource;

        public SourcePath SelectedSource
        {
            get
            {
                if (_selectedSource == null)
                {
                    SelectedSource = SettingsData.SourcePaths.FirstOrDefault();
                }

                return _selectedSource;
            }
            set { _selectedSource = value; OnPropertyChanged("SelectedSource"); }
        }


        public ICommand ReScanIndexCommand
        {
            get
            {
                if (_reScanIndex == null)
                    _reScanIndex = new DelegateCommand(p =>
                    {
                        this.dataHelper.IndexFiles();
                    }, p => true);

                return _reScanIndex;
            }
        }

    }
}
