using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using SadCL.MissileLauncher;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SadGui
{
	class MainWindowViewModel : ViewModelBase
	{
		private IMissileLauncher m_launcher;
		private List<Target.Target> targList;
		public ObservableCollection<Target.Target> Targets {get; set;}
		private string m_name;
		private double m_xPos;

		private int msgNumber; // Tracks number of time message has been changed.

		// Define the increment for all move commands here
		private const double moveAmnt = 100;

		ChangedProperty message;

		public MainWindowViewModel(IMissileLauncher launcher, List<Target.Target> tempTargs) {
			m_launcher = launcher;

			exitCommand = new DelegateCommand(exit);
			FireCommand = new DelegateCommand(Fire);
			moveUpCommand = new DelegateCommand(moveUp);
			moveDownCommand = new DelegateCommand(moveDown);
			moveLeftCommand = new DelegateCommand(moveLeft);
			moveRightCommand = new DelegateCommand(moveRight);


			changeMessage = new DelegateCommand(testMsg);
			message = new ChangedProperty();
			msgNumber = 0;

			ChangeNameOfTarget = new DelegateCommand(changeName);

			m_name = "Hello World!";

			Targets = new ObservableCollection<Target.Target>(tempTargs);
		}


		// Alright, this right here lets us 
		//public ObservableCollection<TargetViewModel> Targets { get; private set; }



		public string Name { 
			get{ return m_name; }
			set { 
				m_name = value;
				OnPropertyChanged("Name");
			}
		}

		public double X {
			get {
				return m_xPos;
			}
			set {
				m_xPos = value;
				OnPropertyChanged("X");
			}
		}



		public ChangedProperty Message {
			get { return message; }
			set { message = value; }
		}

		public void testMsg() {
			this.Message.TestMessage = this.Message.TestMessage + msgNumber;
			msgNumber++;
		}

		public void exit() {
			App.Current.Shutdown();
		}

		public void Fire() {
			m_launcher.fire();
		}

		public void moveUp() {
			m_launcher.moveBy(0, moveAmnt);
		}

		public void moveDown() {
			m_launcher.moveBy(0, -moveAmnt);
		}

		public void moveLeft() {
			m_launcher.moveBy(moveAmnt, 0);
		}
		public void moveRight() {
			m_launcher.moveBy(-moveAmnt, 0);
		}

		public void changeName() {
			Name = "new name";
		}

		public ICommand ChangeNameOfTarget { get; set; }
		public ICommand changeMessage { get; set; }
		public ICommand exitCommand { get; set; }

		public ICommand moveLeftCommand { get; set; }
		public ICommand moveRightCommand { get; set; }
		public ICommand moveDownCommand { get; set; }
		public ICommand moveUpCommand { get; set; }
		public ICommand FireCommand { get; set; }

	}
}
