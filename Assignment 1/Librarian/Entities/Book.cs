using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Interfaces.Entities;

namespace Librarian.Entities
{
	public class Book : IBook
	{

		#region Book fields

		/// <summary>
		/// The Id of the current Book object.
		/// </summary>
		private int _id;

		/// <summary>
		/// The author of the book.
		/// </summary>
		private string _author;

		/// <summary>
		/// The title of the book.
		/// </summary>
		private string _title;
		
		/// <summary>
		/// The call numnber for the book.
		/// </summary>
		private string _callNumber;

		/// <summary>
		/// The loan object related to the book if available.
		/// </summary>
		private ILoan _loan;

		/// <summary>
		/// Represents the current state of the book.
		/// </summary>
		private BookConstants.BookState _bookState;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance ofo the Borow 
		/// </summary>
		/// <param name="author"></param>
		/// <param name="title"></param>
		/// <param name="callNumber"></param>
		/// <param name="bookId"></param>
		/// <exception cref="System.ApplicationExcetion">Thrown when the author parameter is null or empty.</exception>
		/// <exception cref="System.ApplicationExcetion">Thrown when the title parameter is null or empty.</exception>
		/// <exception cref="System.ApplicationExcetion">Thrown when the callNumber parameter is null or empty.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the bookId is not a positive integer.</exception>
		public Book(string author, string title, string callNumber, int bookId)
		{
			// If the author is null or empty, throw exeption
			if (String.IsNullOrEmpty(author))
			{
				throw new ArgumentException("The 'author' parameter cannot be null or empty.", "author");
			}

			// If the title is null or empty, throw exeption
			if (String.IsNullOrEmpty(title))
			{
				throw new ArgumentException("The 'title' parameter cannot be null or empty.", "title");
			}

			// If the callNumber is null or empty, throw exeption
			if (String.IsNullOrEmpty(callNumber))
			{
				throw new ArgumentException("The 'callNumber cannot be null or empty.", "callNumber");
			}

			// Ensure the loanId is a positive integer
			if (bookId <= 0)
			{
				throw new ArgumentOutOfRangeException("bookId", "The 'bookId' parameter must be a positive integer value.");
			}

			// Set the book fields
			_id = bookId;
			_author = author;
			_title = title;
			_callNumber = callNumber;
			_bookState = BookConstants.BookState.AVAILABLE;

		}

		#endregion

		#region IBook interface methods

		/// <summary>
		/// Associates the loan object with the book and sets the book state as ON_LOAN.
		/// </summary>
		/// <param name="loan">The ILoan object to associate with the book.</param>
		/// <exception cref="System.ApplicatonException">THrown if the book is not currently available.</exception>
		public void borrow(ILoan loan)
		{
			// If the current book is not available, throw exception
			if (_bookState != BookConstants.BookState.AVAILABLE)
			{
				throw new ApplicationException("The book is not currently available.");
			}

			// Set the loan falue
			_loan = loan;

			// Set the book state to ON_LOAN
			_bookState = BookConstants.BookState.ON_LOAN;

		}

		/// <summary>
		/// Gets the current loan object for the book. Null will be returned if the book is not currently ON_LOAN.
		/// </summary>
		/// <returns>The current ILoan object if the book is ON_LOAN otherwise null.</returns>
		public ILoan getLoan()
		{
			// Return the loan if the book is ON_LOAN, otherwise return null
			if (_bookState == BookConstants.BookState.ON_LOAN)
			{
				return _loan;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns the book and marks the book as AVAILABLE if the damaged flag is set to false. If the damaged flag is true, the book state is set to DAMAGED.
		/// Removes the loan object currently associated with the book.
		/// </summary>
		/// <param name="damaged">If true, the book will be placed in the DAMAGED state on return.</param>
		/// <exception cref="System.ApplicationException">Thrown if the book is not currently in the ON_LOAN state.</exception>
		public void returnBook(bool damaged)
		{
			// Ensure the book is currently in the ON_LOAN state
			if (_bookState != BookConstants.BookState.ON_LOAN)
			{
				throw new ApplicationException("The book cannot be returned as it is not currently in the ON_LOAN state.");
			}

			// remove the loan
			_loan = null;

			// Set the state to AVAILABLE if not damaged, otherwise set to DAMAGED state
			_bookState = (!damaged ? BookConstants.BookState.AVAILABLE : BookConstants.BookState.DAMAGED);

		}

		/// <summary>
		/// Sets the state of the book to LOST.
		/// </summary>
		/// <exception cref="System.ApplicationException">Thrown if the book is not currently in the ON_LOAN state.</exception>
		public void lose()
		{
			// Ensure the book is currently in the ON_LOAN state.
			if (_bookState != BookConstants.BookState.ON_LOAN)
			{
				throw new ApplicationException("The book cannot be marked as lost as it is not currently in the ON_LOAN state.");
			}

			// Set the book to the lost state
			_bookState = BookConstants.BookState.LOST;
		}

		/// <summary>
		/// Sets the book to the AVAILABLE state if currently in the DAMAGED state.
		/// </summary>
		/// <exception cref="System.ApplicationException">Thrown if the book is not currently in the DAMAGED state.</exception>
		public void repair()
		{
			// Ensure the book is in the DAMAGED state
			if (_bookState != BookConstants.BookState.DAMAGED)
			{
				throw new ApplicationException("The book cannot be marked as repaired and available as it is not currently in the DAMAGED state.");
			}

			// Set the book to the lost state
			_bookState = BookConstants.BookState.AVAILABLE;
		}

		/// <summary>
		/// Sets the book to the DISPOSED state.
		/// </summary>
		/// <exception cref="System.ApplicationException">Thrown if the book is not currently in the AVAILABLE, DAMAGED or LOST states.</exception>
		public void dispose()
		{
			// Ensure the book is in the AVAILABLE, DAMAGED or LOST states
			if (_bookState != BookConstants.BookState.AVAILABLE || _bookState != BookConstants.BookState.DAMAGED ||
				_bookState != BookConstants.BookState.LOST)
			{
				throw new ApplicationException("The book cannot be marked as disposed as it is not currently in the AVAILABLE, DAMAGED or LOST state.");
			}

			// Set the book to the lost state
			_bookState = BookConstants.BookState.DISPOSED;
		}

		public BookConstants.BookState getState()
		{
			throw new NotImplementedException();
		}

		public string getAuthor()
		{
			throw new NotImplementedException();
		}

		public string getTitle()
		{
			throw new NotImplementedException();
		}

		public string getCallNumber()
		{
			throw new NotImplementedException();
		}

		public int getID()
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}
