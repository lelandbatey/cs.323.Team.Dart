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

			var targets = new List<Target.Target>();

			for (int i = 0; i < 4; i++) {
				var sumthin = new Target.Target();
				sumthin.Name = i.ToString();
				sumthin.Friend = false;
				sumthin.X = i+1;
				sumthin.Y = i;
				sumthin.Z = i+2;
				targets.Add(sumthin);
			}

			MainWindowViewModel viewModel = new MainWindowViewModel(launcher, targets);
			window.DataContext = viewModel;
			window.ShowDialog();
		}
	}
}
