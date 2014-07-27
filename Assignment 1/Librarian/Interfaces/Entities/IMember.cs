using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Interfaces.Entities
{
    interface IMember
    {
        bool hasOverDueLoans();

        bool hasReachedLoanLimit();

        bool hasFinesPayable();

        bool hasReachedFineLimit();

        float getFineAmount();

        void addFine(float fine);

        void payFine(float payment);

        void addLoan(ILoan loan);

        List<ILoan> getLoans();

        void removeLoan(ILoan loan);

        String getFirstName();

        String getLastName();

        String getContactPhone();

        String getEmailAddress();

        int getID();

        MemberConstants.MemberState getState();

    }
}
