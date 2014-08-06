using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Interfaces.Entities
{
    public interface ILoan
    {
        void commit();

        void complete();

        bool isOverDue();

        bool checkOverDue(DateTime currentDate);

        IMember getBorrower();

        IBook getBook();

        int getID();

        LoanConstants.LoanState getState();
    }
}
