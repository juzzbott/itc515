using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Interfaces.Entities;

namespace Librarian.Interfaces.Helpers
{
    interface ILoanHelper
    {
        ILoan makeLoan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate, int id);

    }
}
