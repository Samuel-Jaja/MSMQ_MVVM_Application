﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMQ_Subsciber_MVVM_Application.Model
{
    public class RetrievedWellDataModel
    {
        public string? FieldName { get; set; }
        public string? WellName { get; set; }
        public string? DrainagePoint { get; set; }
    }
}
