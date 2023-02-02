using MSMQ.Messaging;
using MSMQ_MVVM_Application.Model;
using MSMQ_Subsciber_MVVM_Application.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;

namespace MSMQ_Subsciber_MVVM_Application.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class RetrievedWellDataViewModel:BindableBase
    {
        /// <summary>
        /// 
        /// </summary>
        public RetrievedWellDataViewModel()
        {
            RetrieveWellCommand = new DelegateCommand(RetrieveWellCommandAction);
            retrievedWellDataModels = new ObservableCollection<RetrievedWellDataModel>();
        }
        public DelegateCommand RetrieveWellCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void RetrieveWellCommandAction()
        {
            MessageQueue queue = new MessageQueue(@".\private$\MSMQ_MessagingApp");
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MSMQ_MVVM_Application.Model.WellDataModel) });
            Message msg = queue.Receive();
            WellDataModel welldata = (WellDataModel)msg.Body;
            RetrievedWellDataModel retrievedWellDataModel = new RetrievedWellDataModel
            {
                FieldName = welldata.FieldName,
                WellName = welldata.WellName,
                DrainagePoint = welldata.DrainagePoint
            };
            retrievedWellDataModels.Add(retrievedWellDataModel);
        }
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<RetrievedWellDataModel> retrievedWellDataModels;
        public ObservableCollection<RetrievedWellDataModel> RetrievedWellDataModels
        {
            get { return retrievedWellDataModels; }
            set { retrievedWellDataModels = value; }
        }
    }
}
