using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using SadCL.MissileLauncher;

namespace SadGui
{
	class MainWindowViewModel
	{
		private IMissileLauncher m_launcher;

		// Define the increment for all move commands here
		private const double moveAmnt = 100;

		public MainWindowViewModel(IMissileLauncher launcher) {
			m_launcher = launcher;

			exitCommand = new DelegateCommand(exit);
			FireCommand = new DelegateCommand(Fire);
			moveUpCommand = new DelegateCommand(moveUp);
			moveDownCommand = new DelegateCommand(moveDown);
			moveLeftCommand = new DelegateCommand(moveLeft);
			moveRightCommand = new DelegateCommand(moveRight);

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
			m_launcher.moveBy(-moveAmnt, 0);
		}
		public void moveRight() {
			m_launcher.moveBy(moveAmnt, 0);
		}

		public ICommand exitCommand { get; set; }

		public ICommand moveLeftCommand { get; set; }
		public ICommand moveRightCommand { get; set; }
		public ICommand moveDownCommand { get; set; }
		public ICommand moveUpCommand { get; set; }
		public ICommand FireCommand { get; set; }
	}
}
