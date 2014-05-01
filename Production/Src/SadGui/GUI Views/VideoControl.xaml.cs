//using SadCLGUI.GUI_ViewModels;
using SadCLGUI.ViewModels;
using System;
using System.Collections.Generic;
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

namespace SadCLGUI.GUI_Views
{
	/// <summary>
	/// Interaction logic for VideoControl.xaml
	/// </summary>
	public partial class VideoControl : UserControl
	{
		public VideoControl() {
			InitializeComponent();
		}

		// It's not *nice* that we have this in the codebehind, but it's about the minimum we 
		// could figure out how to get away with. We have to send the image to the view model 
		// somehow, so this is how we figured we'd do it.
		
		private void StopButton_Click(object sender, RoutedEventArgs e) {
			VideoControlViewModel.Instance.StopVideo();
		}

		private void StartButton_Click(object sender, RoutedEventArgs e) {
			VideoControlViewModel.Instance.Start(image);
		}
	}
}
