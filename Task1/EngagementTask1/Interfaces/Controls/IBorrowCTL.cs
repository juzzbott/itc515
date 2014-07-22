using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngagementTask1.Interfaces.Controls
{
    interface IBorrowCTL
    {
        void cancel();

        void cardScanned(int memberID);

        void bookScanned(int bookID);

        void scanNext();

        void scansCompleted();

        void rejectPendingList();

        void confirmPendingList();

        void borrowUCended();
	
    }
}
