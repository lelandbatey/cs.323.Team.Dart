using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using SadCL.MissileLauncher;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using SadCL;

namespace SadGui
{
	class MainWindowViewModel : ViewModelBase
	{
		private IMissileLauncher m_launcher;
		private List<Target.Target> targList;
		public ObservableCollection<Target.Target> Targets {get; set;}
		private string m_name;
		private double m_xPos;

		private Target.TargetManager tMan;
		private SadCL.MissileLauncher.MissileLauncherController mMan;

		private int msgNumber; // Tracks number of time message has been changed.

		// Define the increment for all move commands here
		private const double moveAmnt = 100;

		MsgTest message;
		

		public MainWindowViewModel(IMissileLauncher launcher, List<Target.Target> tempTargs) {
			m_launcher = launcher;

			tMan = Target.TargetManager.Instance;
			mMan = new SadCL.MissileLauncher.MissileLauncherController();
			mMan.reset();

			exitCommand = new DelegateCommand(exit);
			FireCommand = new DelegateCommand(Fire);
			moveUpCommand = new DelegateCommand(moveUp);
			moveDownCommand = new DelegateCommand(moveDown);
			moveLeftCommand = new DelegateCommand(moveLeft);
			moveRightCommand = new DelegateCommand(moveRight);
			loadTargetsCommand = new DelegateCommand(loadTargets);
			clearTargetsCommand = new DelegateCommand(clearTargets);
			aimAtTargetCommand = new DelegateCommand(aimAtTarget);
			shootAtTargetCommand = new DelegateCommand(killTarget);


			changeMessage = new DelegateCommand(testMsg);
			message = new MsgTest();
			msgNumber = 0;

			FilePath = new filePathText();
			TargetIndex = new targIndex();

			ChangeNameOfTarget = new DelegateCommand(changeName);

			m_name = "Hello World!";

			Targets = new ObservableCollection<Target.Target>(tMan.getAll());
			this.Message.TestMessage = "All jacked up and ready to go!";
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



		public MsgTest Message {
			get { return message; }
			set { message = value; }
		}

		public filePathText FilePath { get; set; }
		public targIndex TargetIndex { get; set; }

		public void loadTargets() {
			try {
				Debug.Write(this.FilePath.PathText);
				tMan.load(this.FilePath.PathText);
				Debug.Write("Correctly loaded the file.");

				// Removes all the targets, then inserts the newly loaded ones back in!
				Targets.Clear();
				foreach (var item in tMan.getAll()) {
					Targets.Add(item);
				}
				this.FilePath.PathText = "File sucessfully loaded!";
			}
			catch (Exception e) {
				this.FilePath.PathText = "There was an error with the file.";
				Debug.Write("There was an error with loading the file."+e.Message);
				//throw;
			}
		}

		public void clearTargets() {
			tMan.deleteAll();
			Targets.Clear();
		}


		public void aimAtTarget() {
			this.Message.TestMessage = "Successful aiming!";
			doTheAim(true);
		}

		public void doTheAim(bool allowFriend) {
			double X = 0.0;
			double Y = 0.0;
			double Z = 0.0;
			try {
				Tuple<double, double, double> targCoords = tMan.aimAt(Convert.ToInt32(TargetIndex.TargIndex), allowFriend);

				if (targCoords != null) {
					X = targCoords.Item1;
					Y = targCoords.Item2;
					Z = targCoords.Item3;
					double r = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
					double Theta = (Math.PI / 2) - Math.Acos(Z / r);
					double Phi = Math.Atan2(Y, X);

					// Uses non-relative tick conversion
					Phi = CoordConvert.sphToTick(Phi);
					Theta = CoordConvert.vertSphToTick(Theta);
					mMan.move(Phi, Theta);
				}

			}
			catch (Exception e) {
				throw e;
			}
		}

		public void killTarget() {
			try {
				doTheAim(false);
				mMan.fire();
			}
			catch (Exception) {
				this.Message.TestMessage = "I'm sorry Dave. I can't do that.";
			}
			
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
		public ICommand loadTargetsCommand { get; set; }
		public ICommand clearTargetsCommand { get; set; }
		public ICommand aimAtTargetCommand { get; set; }
		public ICommand shootAtTargetCommand { get; set; }


		public ICommand moveLeftCommand { get; set; }
		public ICommand moveRightCommand { get; set; }
		public ICommand moveDownCommand { get; set; }
		public ICommand moveUpCommand { get; set; }
		public ICommand FireCommand { get; set; }

	}
	class MsgTest : ChangedProperty
	{
		private string testMessage;

		public string TestMessage {
			get { return testMessage; }
			set {
				testMessage = value;
				this.OnPropertyChanged("TestMessage");

			}
		}
	}

	class filePathText : ChangedProperty
	{
		private string _pathText;

		public string PathText {
			get { return _pathText;}
			set { 
				_pathText = value;
				this.OnPropertyChanged("PathText");
			}
		}
	}

	class targIndex : ChangedProperty
	{
		private string _targIndex;

		public string TargIndex {
			get { return _targIndex; }
			set {
				_targIndex = value;
				this.OnPropertyChanged("TargIndex");
			}
		}
	}
}
