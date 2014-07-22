using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngagementTask1.Interfaces.Controls;
using EngagementTask1.Interfaces.Entities;

namespace EngagementTask1.Interfaces.Uis
{
    interface IBorrowUI
    {
        void initialise(IBorrowCTL control);

        void setState(BorrowCTLConstants.State state);

        void displayBorrowerDetails(IMember borrower);

        void scanBook();

        void displayBook(IBook book);

        void displayPendingList(List<ILoan> pendingList);

        void displayCompletedList(List<ILoan> pendingList);

        void printLoanSlip();

    }
}
