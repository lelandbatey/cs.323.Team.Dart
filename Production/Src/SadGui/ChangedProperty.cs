using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGui
{
	class ChangedProperty : INotifyPropertyChanged
	{
		//private string testMessage;

		//public string TestMessage { 
		//	get{ return testMessage; }
		//	set
		//		{ 
		//			testMessage = value; 
		//			this.OnPropertyChanged("TestMessage");
					
		//		} 
		//	}

		protected void OnPropertyChanged(string propertyName) {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
