using SadCL.MissileLauncher;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SadGui
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Applciation_Startup(object sender, StartupEventArgs args) {
			MainWindow window = new MainWindow();
			IMissileLauncher launcher = new DreamCheekyLauncher("Photon Cannon");
			MainWindowViewModel viewModel = new MainWindowViewModel(launcher);

			window.DataContext = viewModel;
			window.ShowDialog();
		}
	}
}
