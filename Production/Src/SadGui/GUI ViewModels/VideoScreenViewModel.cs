using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCLGUI.GUI_ViewModels
{
    class VideoScreenViewModel
    {   
        Capture m_camera;
        bool m_isProcessing;

        public VideoScreenViewModel()
        {
            m_isProcessing  = false;
            m_camera        = new Capture();
        }

    }
}
