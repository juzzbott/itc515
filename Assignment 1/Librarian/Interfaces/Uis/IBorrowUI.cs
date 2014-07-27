using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Interfaces.Controls;
using Librarian.Interfaces.Entities;

namespace Librarian.Interfaces.Uis
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
