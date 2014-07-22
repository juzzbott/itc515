using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngagementTask1.Interfaces.Entities;

namespace EngagementTask1.Interfaces.Helpers
{
    interface ILoanHelper
    {
        ILoan makeLoan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate, int id);

    }
}
