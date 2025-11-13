using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public enum Status
    {
        Pendent = 1,
        InProgress = 2,
        Finished = 3,
        Canceled = 4
    }
}
