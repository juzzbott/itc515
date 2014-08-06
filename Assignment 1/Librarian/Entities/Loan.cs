using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Interfaces.Entities;

namespace Librarian.Entities
{
	public class Loan : ILoan
	{

		#region Loan properties

		/// <summary>
		/// The Id for the current loan object.
		/// </summary>
		private int _id;

		/// <summary>
		/// The date the loan was started. Note: This DateTime will be set to midnight of the date so that the loan is valid for the entire day.
		/// </summary>
		private DateTime _borrowDate;

		/// <summary>
		/// The date the loaned book is due to be returned. Note: This DateTime will be set 1 second prior to midnight the day 
		/// after the loan is due so that the due date allows returns for the entire day.
		/// So, if the dueDate specified is 04/08/2014, the date time value will be 04/08/2014 23:59:59
		/// </summary>
		private DateTime _dueDate;

		/// <summary>
		/// The IBook object that was loaned.
		/// </summary>
		private IBook _book;

		/// <summary>
		/// The IMember who initiated the loan.
		/// </summary>
		private IMember _borrower;

		/// <summary>
		/// The current state of the loan.
		/// </summary>
		private LoanConstants.LoanState _loanState;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of the Loan object.
		/// </summary>
		/// <param name="book">The IBook object being loaned.</param>
		/// <param name="borrower">The IMember who is initiating the loan.</param>
		/// <param name="borrowDate">The date and time that the book is being borrowed.</param>
		/// <param name="dueDate">The date ad time that the book is due.</param>
		/// <param name="loanId">The Id of the loan object.</param>
		public Loan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate, int loanId)
		{

			// Validate the book parameter
			if (book == null)
			{
				throw new ArgumentNullException("book", "The 'book' parameter cannot be null.");
			}

			// Validate the member parameter
			if (borrower == null)
			{
				throw new ArgumentNullException("borrower", "The 'borrower' parameter cannot be null.");
			}

			// Ensure the borrowDate is valid
			if (borrowDate == DateTime.MinValue)
			{
				throw new ArgumentOutOfRangeException("borrowDate", "The borrowDate cannot be the default DateTime value.");
			}

			// Ensure the borrowDate is valid
			if (dueDate == DateTime.MinValue)
			{
				throw new ArgumentOutOfRangeException("dueDate", "The dueDate cannot be the default DateTime value.");
			}

			// Ensure the due date is not < borrowDate
			if (dueDate < borrowDate)
			{
				throw new ArgumentOutOfRangeException("dueDate", "The dueDate value cannot be less than the borrowDate.");
			}

			// Ensure the loanId is a positive integer
			if (loanId <= 0)
			{
				throw new ArgumentOutOfRangeException("loanId", "The 'loanId' parameter must be a positive integer value.");
			}

			// Set the fields of the Loan class
			this._book = book;
			this._borrower = borrower;
			this._id = loanId;
			
			// Use the Date property to get the midnight value for the current date
			this._borrowDate = borrowDate.Date;

			// Add 1 day to the Date property, and then subtract 1 second to get 23:59:59 on the due date
			this._dueDate = dueDate.Date.AddDays(1).AddSeconds(-1);

			// Set the initial state for the loan object
			this._loanState = LoanConstants.LoanState.PENDING;

		}

		#endregion

		#region ILoan interface members

		public void commit()
		{
			throw new NotImplementedException();
		}

		public void complete()
		{
			throw new NotImplementedException();
		}

		public bool isOverDue()
		{
			throw new NotImplementedException();
		}

		public bool checkOverDue(DateTime currentDate)
		{
			throw new NotImplementedException();
		}

		public IMember getBorrower()
		{
			throw new NotImplementedException();
		}

		public IBook getBook()
		{
			throw new NotImplementedException();
		}

		public int getID()
		{
			throw new NotImplementedException();
		}

		public LoanConstants.LoanState getState()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
