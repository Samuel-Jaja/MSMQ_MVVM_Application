using MSMQ.Messaging;
using MSMQ_MVVM_Application.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MSMQ_MVVM_Application.ViewModel
{
    /// <summary>
    /// WellDataViewModel handles te logic between the model and view. The class uses BindableBase class from Prism and MSMQ Messaging Nuget Package
    /// </summary>
    public class WellDataViewModel:BindableBase
    {
        public WellDataViewModel()
        {
            SendWellDataCommand = new DelegateCommand(SendWellDataCommandAction);
        }

        public DelegateCommand SendWellDataCommand { get; set; }
        /// <summary>
        /// This method sends data( well data) to the queue created  
        /// </summary>
        public void SendWellDataCommandAction()
        {
            EnableQueue();
            MessageQueue queue = new MessageQueue(@".\private$\MSMQ_MessagingApp");
            WellDataModel wellData = new WellDataModel
            {
                WellName = WellName,
                FieldName = FieldName,
                DrainagePoint = DrainagePoint
            };
            Message msg  = new Message(wellData);
            queue.Send(msg, "CypherCrescentResource");
            FieldName = string.Empty;
            WellName= string.Empty;
            DrainagePoint=string.Empty;
        }

        /// <summary>
        /// This metthod creates and names a queue if one does nor exist
        /// </summary>
        public void EnableQueue()
        {
            string queueName = @".\private$\MSMQ_MessagingApp";
            MessageQueue? messageQueue = null;
            if (!MessageQueue.Exists(queueName))
            {
                messageQueue = MessageQueue.Create(queueName);
            }
        }

        /// <summary>
        /// Backing fields and corresponding properties 
        /// </summary>
        private string fieldName;
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; RaisePropertyChanged();}
        }
        private string wellName;
        public string WellName
        {
            get { return wellName; }
            set { wellName = value; RaisePropertyChanged();}
        }
        private string drainagePoint;
        public string DrainagePoint
        {
            get { return drainagePoint; }
            set { drainagePoint = value; RaisePropertyChanged(); }
        }
    }
}
