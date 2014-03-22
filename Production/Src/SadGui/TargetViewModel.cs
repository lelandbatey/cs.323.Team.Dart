using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target;

namespace SadGui
{
	class TargetViewModel : ViewModelBase
	{
		private Target.Target m_target;

		public TargetViewModel(Target.Target target) {
			m_target = target;
		}

		public string Name {
			get {
				return m_target.Name;
			}
			private set{}
		}

	}
}
