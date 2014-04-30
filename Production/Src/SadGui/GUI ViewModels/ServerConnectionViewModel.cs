using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargetServerCommunicator.Servers;
using TargetServerCommunicator.Data;
using System.Windows;
using System.Collections.ObjectModel;
using TargetBase;

namespace SadCLGUI.ViewModels
{
	class ServerConnectionViewModel : ViewModelBase
	{
		private MainWindowViewModel MainWindowVM;
		private TargetServerCommunicator.IGameServer m_gameserver;
		private IEnumerable<string> gameModes;
		private string serverPort;
		private string serverIP;
		private string teamName;



		public ObservableCollection<string> GameModes { get; set; }
		public bool isConnected { get; private set; }
		public DelegateCommand connectToServer {get; set;}
		public DelegateCommand StartGame { get; set; }
		public DelegateCommand StopGame { get; set; }
		public string SelectedGameMode {get; set;}

		public ServerConnectionViewModel(SadCLGUI.ViewModels.MainWindowViewModel MWVM) {
			connectToServer = new DelegateCommand((Action)connect);
			StartGame = new DelegateCommand((Action)startGame);
			ServerPort = "3000";
			ServerIP = "0.0.0.0";
			TeamName = "Team Dart";
			MainWindowVM = MWVM;
			SelectedGameMode = "d";
			GameModes = new ObservableCollection<string>();
		}

		public string ServerPort {
			get { return serverPort; }
			set { serverPort = value; }
		}
		public string ServerIP {
			get { return serverIP; }
			set { serverIP = value; }
		}
		public string TeamName {
			get { return teamName; }
			set { teamName = value; }
		}


		private void connect() {
			try {
				m_gameserver = GameServerFactory.Create(GameServerType.Mock, teamName, serverIP, Convert.ToInt32(serverPort));
				gameModes = m_gameserver.RetrieveGameList();
				m_gameserver.StopRunningGame();
				foreach (var item in gameModes) {
					GameModes.Add(item);
				}
				//GameModes = ObservableCollection<string>(gameModes);
				isConnected = true;
				MainWindowVM.setTargets(m_gameserver.RetrieveTargetList(SelectedGameMode).ToList());
			}
			catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		private void startGame() {
			if (SelectedGameMode!=null) {
				m_gameserver.StartGame(SelectedGameMode);
				MainWindowVM.killTargets(m_gameserver.RetrieveTargetList(SelectedGameMode).ToList());
			} else {
				MessageBox.Show("Error: You must select a game mode.");
			}
		}



	}
}
