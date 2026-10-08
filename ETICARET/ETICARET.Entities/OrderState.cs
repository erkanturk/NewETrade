using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Entities;

public enum OrderState
{
    PaymentPending=0,
    Preparing=1,
    Shipped=2,
    Completed=3,
    Cancelled=4
}