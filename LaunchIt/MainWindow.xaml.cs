using LaunchIt.Core;
using LaunchIt.Helper;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaunchIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataHelper dataHelper = new DataHelper();
        MainViewModel vm = null;
        public MainWindow()
        {
            vm = new MainViewModel();
            this.DataContext = vm;

            InitializeComponent();
            Loaded += (s, e) =>
            {
                var showHk = new HotKey(ModifierKeys.Control, Keys.Space, this);
                showHk.HotKeyPressed += showHk_HotKeyPressed;

                var hideHk = new HotKey(ModifierKeys.None, Keys.Escape, this);
                hideHk.HotKeyPressed += hideHk_HotKeyPressed;

                SetSearchTextOnFocus();
            };

            this.MouseLeftButtonUp += (s, e) => vm.LaunchSelected();
        }

        void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    vm.MoveNext();
                    break;
                case Key.Up:
                    vm.MovePrevious();
                    break;
                case Key.Enter:
                    vm.LaunchSelected();
                    break;
                default:
                   // SetSearchTextOnFocus();
                    break;
            }
        }
         
        private void SetSearchTextOnFocus()
        {
            this.Activate();
            SearchText.CaretIndex = SearchText.Text.Length;
            SearchText.Focus();
        }

        void hideHk_HotKeyPressed(HotKey obj)
        {
            this.Hide();
        }

        void showHk_HotKeyPressed(HotKey obj)
        {
            this.Show();
            this.Topmost = true;
            SetSearchTextOnFocus();
            this.Topmost = false;
        }
    }
}
