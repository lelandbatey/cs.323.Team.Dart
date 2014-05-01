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
		private bool m_isrunning = false;
		private bool m_disabled = false; // For now, it's not used. However, will be used if we make a switch for disableing video (questionable whether it'll happen)

		static private VideoControlViewModel m_VidControlVMInstance = null;
		private Capture m_capture = null;
		private System.Windows.Controls.Image m_image = null;
		

		// Singleton stuff. This is what makes it a singleton
		static public VideoControlViewModel Instance {
			get {
				if (m_VidControlVMInstance == null) {
					m_VidControlVMInstance = new VideoControlViewModel();
				}
				return m_VidControlVMInstance;
			}
		}

		private VideoControlViewModel() { }

		public bool IsRunning() {
			return m_isrunning;
		}

		public DelegateCommand StartCommand { get; set; }

		public void Startup() {
			
		}

		public void StopVideo() {
			m_isrunning = false;
			m_capture = null;
			m_VidControlVMInstance = null;
		}

		private void SetDisabled() {
			m_disabled = true;
			m_isrunning = false;
		}

		public void Start(System.Windows.Controls.Image image) {
			
			// Checks and instantiation
			if (image != null) {
				m_image = image;
			}

			if (m_capture == null) {
				try {
					// Throws an error if we don't have a camera attached
					m_capture = new Capture();
					m_disabled = false;
				} catch (Exception e){
					// If there's no camera, disable it, don't start
					MessageBox.Show(e.Message);
					SetDisabled();
					return;
				}
			}

			if (m_isrunning == true) {
				return;
			}

			m_isrunning = true;

			// Get frames, 
			Image<Bgr, Byte> currentCameraFrame = m_capture.QueryFrame();
			m_image.Source = BitmapToBitMapImage(currentCameraFrame.ToBitmap());
			
			
			BackgroundWorker ImageCaptureWorker = new BackgroundWorker();
			var External_Dispatcher = Dispatcher.CurrentDispatcher;


			ImageCaptureWorker.DoWork += new DoWorkEventHandler( delegate(object o, DoWorkEventArgs e) {
				// Create our background thread
				BackgroundWorker background = o as BackgroundWorker;

				while (m_isrunning) {
					currentCameraFrame = m_capture.QueryFrame();
					
					if (currentCameraFrame != null) {
						// Why do we have to do this weirdness with the dispatcher?
						// It turns out that in C#, one thread cannot access the objects 
						// of another thread. If you attempt to do so, you'll get an 
						// error. To get around this, we have to use this somewhat odd
						// hack, whereby we pass a reference to the dispatcher of 
						// "Thread A" to "Thread B". B then uses anonymous functions given
						// to A's dispatcher to be able to mess with the objects 
						// that belong to A. Doing this, we can get around the threading 
						// restrictions, but in a safe way.

						External_Dispatcher.Invoke(
							// This way of doing an anonymous function is taken from this StackOverflow answer:
							// http://stackoverflow.com/a/9732853/1712696
							(Action)(() => {
								m_image.Source = (BitmapToBitMapImage(currentCameraFrame.ToBitmap()));
							}));
						System.GC.Collect();
					}
					Thread.Sleep(2000/120);
				}


			});

			// When the capturing thread is turned off, this makes it so we see the 
			// classic "colored bars" in the image window.
			ImageCaptureWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
			delegate(object o, RunWorkerCompletedEventArgs args) {
				Bitmap tempImage = (Bitmap)Image.FromFile(@"./smpte_color_bars.png");
				m_image.Source = BitmapToBitMapImage(tempImage);
				
			});

			ImageCaptureWorker.RunWorkerAsync();
		}

		// This conversion function is pretty much taken from this stackoverflow:
		// http://stackoverflow.com/a/6484754/1712696
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

		
	}
}
