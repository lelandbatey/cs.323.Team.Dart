using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Target;
using TargetBase;

namespace SadCLGUI.ViewModels
{
    public class TargetViewModel : ViewModels.ViewModelBase
    {
		//private Target.Target m_target;
		private TargetBase.Target m_target;

        public TargetViewModel(TargetBase.Target target) {
            m_target = target;
        }

        public string Name {
            get {
                return m_target.name;
            }
        }
        public string Friend {
            get {
                if (m_target.status > 0) {
                    return "Friend";
                } else {
                    return "Foe";
                }
            }
        }
        public string Status {
            get {
                if (m_target.hit > 0) {
                    return "Dead";
                } else {
                    return "Alive";
                }
            }
            set {
                
                m_target.hit++;
                OnPropertyChanged("Status");
                
            }
        }


		public bool IsFriend {
			get {
				if (m_target.status > 0) {
					return true;
				} else {
					return false;
				}
			}
		}

		public double X {
			get { return m_target.x; }
		}
		public double Y {
			get { return m_target.y; }
		}
		public double Z {
			get { return m_target.z; }
		}
    }
}
