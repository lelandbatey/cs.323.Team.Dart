using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Code used from, "http://stackoverflow.com/questions/15119974/creating-backgroundworker-with-queue"

namespace SadCLGUI
{
    static class TaskQueue
    {
        //Because it's static, FromResult returns the outcome of the previous
        //Task that was completed.
        static private Task command_task = Task.FromResult(true);
        static private object key = new object();

        //Somehow, 't' represents the current Task, and we are running the
        //action that was passed to this method.
        static public void Add_Task(Action passed_command) {
            try {
                lock (key) {
                    //Execute the action that was passed to this method, and assign
                    //the result of this command back to the same value.

                    //Can't make much sense of it...
                    command_task = command_task.ContinueWith(t => passed_command());
                }
            }

            //Apparently the only exceptions this class throws are Aggregates
            catch (AggregateException excpt) {
                foreach (var ae in excpt.InnerExceptions) {
                    Console.WriteLine(ae.Message);
                }
            }
        }
    }
}
