
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIClientV2.Viewmodels;

namespace UIClientV2.Controls
{

    public partial class MainControlView : UserControl
    {

		private readonly MainControlViewModel _viewModel = null;

        public MainControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as MainControlViewModel;
			_viewModel.Initialize(this);
        }

    }
}
