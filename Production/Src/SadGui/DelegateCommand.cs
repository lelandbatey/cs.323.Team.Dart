﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGui
{
	public class DelegateCommand : ICommand
	{

		public event EventHandler CanExecuteChanged;
		private bool m_canExecute;
		private Action m_action;

		public DelegateCommand(Action actionToTake) {
			m_canExecute = true;
			m_action     = actionToTake;
		}

		public bool CanExecute(object parameter) {
			return m_canExecute;
		}

		public void Execute(object parameter) {
			m_action();
		}
	}
}
