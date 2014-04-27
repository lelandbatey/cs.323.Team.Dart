using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SadCLGUI.ViewModels
{
	class VideoControlViewModel : ViewModelBase
	{
		static private VideoControlViewModel m_camera = null;
		private Capture m_capture = null;
		private System.Windows.Controls.Image m_image = null;
		private bool m_running = false;
		private bool m_disabled = false;

		public VideoControlViewModel() {
			Action StartAction = Startup;
			
		}

		public bool IsRunning() {
			return m_running;
		}

		static public VideoControlViewModel Singleton {
			get {
				if (m_camera == null) {
					m_camera = new VideoControlViewModel();
				}
				return m_camera;
			}
		}

		public DelegateCommand StartCommand { get; set; }

		public void Startup() {
			
		}

		public void StopVideo() {
			m_running = false;
			m_capture = null;
			m_camera = null;
		}

		private void SetDisabled() {
			m_disabled = true;
			m_running = false;
		}

		public void Start(System.Windows.Controls.Image image) {
			if (image != null) {
				m_image = image;
			}

			if (m_running == true) {
				return;
			}
			if (m_capture == null) {
				try {
					m_capture = new Capture();
					m_disabled = false;
				}
				catch {

					SetDisabled();
					return;
				}
			}
			if (m_capture == null) {
				SetDisabled();
				return;
			}

			m_running = true;

			Image<Bgr, Byte> frame = m_capture.QueryFrame();
			//Image<Gray, Byte> gFrame = frame.Convert<Gray, Byte>();
			m_image.Source = ConvertImageToBitmap(frame);
			//var tester = ConvertImageToBitmap(frame).GetType();
			BackgroundWorker ImageWorker = new BackgroundWorker();
			var uDispatcher = Dispatcher.CurrentDispatcher;
			ImageWorker.DoWork += new DoWorkEventHandler(
			delegate(object o, DoWorkEventArgs e) {
				BackgroundWorker background = o as BackgroundWorker;

				while (m_running) {
					frame = m_capture.QueryFrame();
					if (frame != null) {
						
						uDispatcher.Invoke((Action<Image<Bgr, Byte>>)(obj => m_image.Source = (ConvertImageToBitmap(obj))), frame as Image<Bgr, Byte>);
						
						System.GC.Collect();
					}
					Thread.Sleep(1000 / 60);
				}

				// Potentially very gross
				//if (!m_running) {
				//	Bitmap tempImage = (Bitmap)Image.FromFile(@"./smpte_color_bars.png");
				//	frame = new Image<Bgr,Byte>(tempImage);
				//	uDispatcher.Invoke((Action<Image<Bgr, Byte>>)(obj => m_image.Source = (ConvertImageToBitmap(obj))), frame as Image<Bgr, Byte>);

				//	System.GC.Collect();
				//	Thread.Sleep(1500);
				//}

			});

			ImageWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
			delegate(object o, RunWorkerCompletedEventArgs args) {
				Bitmap tempImage = (Bitmap)Image.FromFile(@"./smpte_color_bars.png");
				m_image.Source = BitmapToBitMapImage(tempImage);
				//m_image.Source = ConvertImageToBitmap(null as Image<Bgr, Byte>);
			});

			ImageWorker.RunWorkerAsync();
		}

		private static BitmapSource ConvertImageToBitmap(IImage image) {
			if (image != null) {
				using (Bitmap source = image.Bitmap) {
					var hbitmap = source.GetHbitmap();

					var bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero,
						Int32Rect.Empty,
						BitmapSizeOptions.FromEmptyOptions());

					DeleteObject(hbitmap);

					bitmap.Freeze();
					return bitmap;
				}
			}
			return null;
		}

		private BitmapSource BitmapToBitMapImage(Bitmap bitm) {
			IntPtr hBitmap = bitm.GetHbitmap();
			

			var toReturn = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap,
				IntPtr.Zero,
				Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());

			DeleteObject(hBitmap);
			return toReturn;
		}
		

		[DllImport("gdi32")]
		private static extern int DeleteObject(IntPtr ptr);

		public void Stop() {
			m_running = false;
		}
	}
}
