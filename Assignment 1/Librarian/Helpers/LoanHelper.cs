using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Entities;
using Librarian.Interfaces.Entities;
using Librarian.Interfaces.Helpers;

namespace Librarian.Helpers
{
	public class LoanHelper : ILoanHelper
	{

		/// <summary>
		/// Builds a new ILoan object.
		/// </summary>
		/// <param name="book">The IBook to set for the loan.</param>
		/// <param name="borrower">The IMember to set for the loan.</param>
		/// <param name="borrowDate">The date the book was borrowed. Note: The time is ignored and midnight that day is used to allow for the full day.</param>
		/// <param name="dueDate">The date the book is due. Note: The time is ignored and midnight that day is used to allow for the full day.</param>
		/// <param name="id">The ID that the loan object is going to use.</param>
		/// <returns>A new ILoan object.</returns>
		public ILoan makeLoan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate, int id)
		{
			// Create the loan
			ILoan newLoan = new Loan(book, borrower, borrowDate, dueDate, id);

			// return the loan
			return newLoan;
		}
	}
}
