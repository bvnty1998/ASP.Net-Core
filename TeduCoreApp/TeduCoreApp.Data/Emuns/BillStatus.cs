using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TeduCoreApp.Data.Emuns
{
    public enum BillStatus
    {
        [Description("New")]
        New,
        [Description("InProgress")]
        InProgress,
        [Description("Returned")]
        Returned,
        [Description("Cancelled")]
        Cancelled,
        [Description("Complated")]
        Completed
    }
}
