using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
