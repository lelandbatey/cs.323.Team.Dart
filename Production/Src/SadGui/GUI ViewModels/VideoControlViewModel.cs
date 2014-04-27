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
        static private VideoControlViewModel cam_model = null;

        private Image<Bgr, Byte> ImageFrame;
        private Emgu.CV.UI.ImageBox CamImageBox;
        private Capture capture = null;
        private DispatcherTimer timer;
        private bool captureInProgress;

        static public VideoControlViewModel Singleton {
            get {
                if (cam_model == null) {
                    cam_model = new VideoControlViewModel();
                }

                return cam_model;
            }
        }

        public void CameraButton(Emgu.CV.UI.ImageBox passed_CamImageBox) {
            if (CamImageBox == null) {
                CamImageBox = passed_CamImageBox;
                try {
                    capture = new Capture();
                    timer = new DispatcherTimer();
                }
                catch (TypeInitializationException excpt) {
                    MessageBox.Show(excpt.Message);
                }
                catch (NullReferenceException excpt) {
                    MessageBox.Show(excpt.Message);
                }
            }
            if (capture != null) {
                if (captureInProgress) {
                    //btnStart.Content = "Start!";
                    timer.Stop();
                } else {
                    //btnStart.Content = "Stop?";
                    timer.Tick += new EventHandler(ProcessFrame);
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                    timer.Start();
                }
                captureInProgress = !captureInProgress;
            }
        }


        private void ProcessFrame(object sender, EventArgs arg) {
            ImageFrame = capture.QueryFrame();
            CamImageBox.Image = ImageFrame;
        }


        private void ReleaseData() {
            if (capture != null) {
                capture.Dispose();
            }
        }
    }
}
