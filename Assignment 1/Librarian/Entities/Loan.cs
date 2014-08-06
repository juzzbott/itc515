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

		/// <summary>
		/// Commits the pending loan by setting the loan state to current. 
		/// </summary>
		/// <exception cref="System.ApplicationException">ApplicationException is thrown if the current loan is not in the PENDING state.</exception>
		public void commit()
		{
			// If the loan state is not pending, throw exception
			if (_loanState != LoanConstants.LoanState.PENDING)
			{
				throw new ApplicationException("The loan can only be commited if it is in the PENDING state.");
			}

			// Set the current loan state to current
			_loanState = LoanConstants.LoanState.CURRENT;

		}

		/// <summary>
		/// Completes the loan by setting the loan state to complete.
		/// </summary>
		/// <exception cref="System.ApplicationException">ApplicationException is thrown if the current loan is not in the CURRENT or OVERDUE states.</exception>
		public void complete()
		{

			// If the loan state is not current or overdue, throw exception
			if (_loanState != LoanConstants.LoanState.CURRENT || _loanState != LoanConstants.LoanState.OVERDUE)
			{
				throw new ApplicationException("The loan can only be completed if it is in the CURRENT or OVERDUE state.");
			}

			// Set the current loan state to current
			_loanState = LoanConstants.LoanState.COMPLETE;
		}

		/// <summary>
		/// Returns true if the loan is in the OVERDUE state.
		/// </summary>
		/// <returns>True if the loan is in the OVERDUE state, otherwise false.</returns>
		public bool isOverDue()
		{
			return (_loanState == LoanConstants.LoanState.OVERDUE);
		}

		/// <summary>
		/// Checks to see if the loan is overdue by ensuring the currentDate is less than or equal to the loan due date.
		/// </summary>
		/// <param name="currentDate">The current date to check if the loan is overdue.</param>
		/// <returns>True if the loan is in the OVERDUE state, otherwise false.</returns>
		/// <exception cref="System.ApplicationException">ApplicationException is thrown if the current loan is not in the CURRENT or OVERDUE states.</exception>
		public bool checkOverDue(DateTime currentDate)
		{

			// If the loan state is not current or overdue, throw exception
			if (_loanState != LoanConstants.LoanState.CURRENT || _loanState != LoanConstants.LoanState.OVERDUE)
			{
				throw new ApplicationException("The loan can only be completed if it is in the CURRENT or OVERDUE state.");
			}

			// Checks if the current date (midnight of that date so that the full day is valid) is greater than than loan due date.
			// If the currentDate > dueDate, set the loan state to OVERDUE and return true, otherwise return false.
			if (currentDate.Date > _dueDate)
			{
				_loanState = LoanConstants.LoanState.OVERDUE;
				return false;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Gets the IMember borrow object.
		/// </summary>
		/// <returns>The borrower object.</returns>
		public IMember getBorrower()
		{
			return _borrower;
		}

		/// <summary>
		/// Gets the book on loan.
		/// </summary>
		/// <returns>The book object.</returns>
		public IBook getBook()
		{
			return _book;
		}

		/// <summary>
		/// Gets the Id of the loan object.
		/// </summary>
		/// <returns>The Id of the loan.</returns>
		public int getID()
		{
			// return the Id of the loan
			return _id;
		}

		/// <summary>
		/// Gets the current state of the loan.
		/// <seealso cref="Librarian.Interfaces.Entities.LoanConstants.LoanState"/>
		/// </summary>
		/// <returns>The current state of the loan.</returns>
		public LoanConstants.LoanState getState()
		{
			return _loanState;
		}

		#endregion
	}
}
