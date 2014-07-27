using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Interfaces.Entities;
using Librarian.Interfaces.Helpers;

namespace Librarian.Interfaces.Daos
{
    interface ILoanDAO
    {
        void createNewPendingList(IMember borrower);

        ILoan createPendingLoan(IMember borrower, IBook book, DateTime borrowDate, DateTime dueDate);

        List<ILoan> getPendingList(IMember borrower);

        void commitPendingLoans(IMember borrower);

        void clearPendingLoans(IMember borrower);

        ILoan getLoanByID(int id);

        ILoan getLoanByBook(IBook book);

        List<ILoan> listLoans();

        List<ILoan> findLoansByBorrower(IMember borrower);

        List<ILoan> findLoansByBookTitle(string title);

        void updateOverDueStatus(DateTime currentDate);

        List<ILoan> findOverDueLoans();

    }
}
