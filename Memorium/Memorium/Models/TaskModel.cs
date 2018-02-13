using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorium.Models
{
    class TaskModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Task ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Task Title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Task Content
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// Checker if task is finished
        /// </summary>
        public bool isFinished { get; set; }

        /// <summary>
        /// Status of execution task.
        /// Takes "Active" and "Finished" values
        /// </summary>
        private string status;

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
