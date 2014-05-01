using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadCLGUI
{
    class LauncherCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;  //Required to implement the ICommand Interface.

        private Action m_action;  //The command that is associated with this "Command".
        private bool m_canExecute;  //This boolean won't be set to true unless if the default constructor is used.

        public LauncherCommand(Action actionToTake) { //Basic constructor for a new delegate command.
            m_canExecute = true;
            m_action = actionToTake;
        }
        public bool CanExecute(object parameter) {  //A required method to implement for the ICommand interface.
            return m_canExecute;  //Don't know where this is used.
        }

        public void Execute(object parameter) {  //A required method to implement for the ICommand interface.
            TaskQueue.Add_Task(m_action);  //When the binding is activated, the action contained in this parameter is used.
        }
    }
}
