using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngagementTask1.Interfaces.Controls
{
    public abstract class BorrowCTLConstants
    {
        public enum State { STARTED, BORROWING, DISALLOWED, COMPLETED, CONFIRMED, CANCELLED, ENDED };

    }
}
