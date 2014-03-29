using SadCLGUI.GUI_ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SadCL.MissileLauncher;

namespace SadCLGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e) {
            MainWindow window = new MainWindow();

			// Here we use a relative path for cross-platform compatibilty, as well as the '@' symbol so we don't have to escape our slashes
            string FilePath = @"..\..\..\SadCL\SadCL_Main\SadCL_UnitTests\TestData\targets.ini";

            List<Target.Target> RawList = Target.TargetFactory.BuildTargetList(FilePath);


            MainWindowViewModel viewModel = new MainWindowViewModel(RawList);

            window.DataContext = viewModel;
            window.ShowDialog();
        }
    }
}
