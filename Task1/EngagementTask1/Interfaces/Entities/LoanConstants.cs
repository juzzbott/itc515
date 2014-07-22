using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngagementTask1.Interfaces.Entities
{
    public abstract class LoanConstants
    {
        public const int LOAN_PERIOD = 14;

        public enum LoanState { PENDING, CURRENT, OVERDUE, COMPLETE }

    }
}
