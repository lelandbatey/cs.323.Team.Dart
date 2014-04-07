using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target;

namespace SadCLGUI.GUI_ViewModels
{
    public class TargetViewModel : ViewModels.ViewModelBase
    {
        private Target.Target m_target;

        public TargetViewModel(Target.Target target) {
            m_target = target;
        }

        public string Name {
            get {
                return m_target.Name;
            }
        }
        public string Friend {
            get {
                if (m_target.Friend) {
                    return "Friend";
                } else {
                    return "Foe";
                }
            }
        }
        public string Status {
            get {
                if (m_target.dead) {
                    return "Dead";
                } else {
                    return "Alive";
                }
            }
            set {
                if (!m_target.dead) {
                    m_target.dead = true;
                    OnPropertyChanged("Status");
                }
            }
        }


		public bool IsFriend {
			get {
				if (m_target.Friend) {
					return true;
				} else {
					return false;
				}
			}
		}

		public double X {
			get { return m_target.X; }
		}
		public double Y {
			get { return m_target.Y; }
		}
		public double Z {
			get { return m_target.Z; }
		}
    }
}
