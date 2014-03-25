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

		private List<Target.Target> targList;
		public ObservableCollection<Target.Target> Targets {get; set;}
		private string m_name;
		private double m_xPos;

		private Target.TargetManager tMan;
		private SadCL.MissileLauncher.MissileLauncherController mMan;

		private int msgNumber; // Tracks number of time message has been changed.

		// Define the increment for all move commands here
		private const double moveAmnt = (Math.PI/8.0);

		MsgTest message;
		

		public MainWindowViewModel(List<Target.Target> tempTargs) {

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
			reloadCommand = new DelegateCommand(reload);


			changeMessage = new DelegateCommand(testMsg);
			message = new MsgTest();
			msgNumber = 0;




			FilePath = new filePathText();
			TargetIndex = new targIndex();
			LauncherStatus = new laucherStatus();


			tMan = Target.TargetManager.Instance;
			mMan = new SadCL.MissileLauncher.MissileLauncherController();
			mMan.reset();
			updateLauncherStatus();

			ChangeNameOfTarget = new DelegateCommand(changeName);

			m_name = "Hello World!";

			Targets = new ObservableCollection<Target.Target>(tMan.getAll());
			this.Message.TestMessage = "All jacked up and ready to go!";
		}


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
		public laucherStatus LauncherStatus{ get; set; }

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
			updateLauncherStatus();
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
			updateLauncherStatus();
			
		}



		public void testMsg() {
			this.Message.TestMessage = this.Message.TestMessage + msgNumber;
			msgNumber++;
		}

		public void exit() {
			App.Current.Shutdown();
		}

		public void Fire() {
			mMan.fire();
			updateLauncherStatus();
		}

		public void moveUp() {
			mMan.moveBy(0, CoordConvert.vertSphToTick(moveAmnt));
			updateLauncherStatus();
		}

		public void moveDown() {
			mMan.moveBy(0, CoordConvert.vertSphToTick(-moveAmnt));
			updateLauncherStatus();
		}

		public void moveLeft() {
			mMan.moveBy(CoordConvert.sphToTickRel(moveAmnt), 0);
			updateLauncherStatus();
		}
		public void moveRight() {
			mMan.moveBy(CoordConvert.sphToTickRel(-moveAmnt), 0);
			updateLauncherStatus();
		}

		public void reload() {
			mMan.reload();
			updateLauncherStatus();
		}

		public void changeName() {
			Name = "new name";
		}

		private void updateLauncherStatus() {
			double theta = mMan.getTheta();
			double phi = mMan.getPhi();
			int ammo = mMan.getAmmo();
			

			string toRet = String.Format("Phi   : {0}\nTheta : {1}\nAmmo  : {2}", phi, theta, ammo);
			LauncherStatus.LaunchStatus = toRet;
		}

		public ICommand ChangeNameOfTarget { get; set; }
		public ICommand changeMessage { get; set; }
		public ICommand exitCommand { get; set; }
		public ICommand loadTargetsCommand { get; set; }
		public ICommand clearTargetsCommand { get; set; }
		public ICommand aimAtTargetCommand { get; set; }
		public ICommand shootAtTargetCommand { get; set; }
		public ICommand reloadCommand { get; set; }


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
	class laucherStatus : ChangedProperty
	{
		private string _launcherStatus;

		public string LaunchStatus {
			get { return _launcherStatus; }
			set {
				_launcherStatus = value;
				this.OnPropertyChanged("LaunchStatus");
			}
		}
	}
}
