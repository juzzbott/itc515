using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Entities;
using Librarian.Interfaces.Daos;
using Librarian.Interfaces.Entities;
using Librarian.Interfaces.Helpers;

namespace Librarian.Daos
{
	public class LoanDAO : ILoanDAO
	{

		#region LoanDAO fields

		/// <summary>
		/// The collection of pending ILoan objects stored in the DAO. 
		/// The key for the dictionary is the member id. This helps to segregate the list into members.
		/// </summary>
		private IDictionary<int, IList<ILoan>> _pendingItems;

		/// <summary>
		/// THe collection of commited ILoan objects for the member.
		/// The key for the dictionary is the member id. This helps to segregate the list into members.
		/// </summary>
		private IList<ILoan> _committedItems;

		/// <summary>
		/// The ILoanHelper object for helping to create ILoan objects.
		/// </summary>
		private ILoanHelper _helper;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of the LoanDAO object.
		/// </summary>
		/// <param name="helper">An ILoanHelper object.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'helper' parameter is null.</exception>
		public LoanDAO(ILoanHelper helper)
		{

			// Ensure the helper is not null
			if (helper == null)
			{
				throw new ArgumentNullException("helper", "The 'helper' parameter cannot be null.");
			}

			// Instantiate the loan lists
			this._pendingItems = new Dictionary<int, IList<ILoan>>();
			this._committedItems = new List<ILoan>();

			// Set the helper object
			this._helper = helper;

		}

		#endregion

		#region ILoan interface methods

		/// <summary>
		/// Creates a new pending list for the borrower in the DAO.
		/// </summary>
		/// <param name="borrower">The IMemer to create the pending list for.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'borrower' parameter is null.</exception>
		public void createNewPendingList(IMember borrower)
		{

			// Validate the borrower parameter
			if (borrower == null)
			{
				throw new ArgumentNullException("borrower", "The 'borrower' parameter cannot be null.");
			}

			// get the borrowerId
			int borrowerId = borrower.getID();

			// If the dictionary of items does not contain the member id as a key, then create a new list of ILoan objects for that borrower
			if (!_pendingItems.ContainsKey(borrowerId))
			{
				_pendingItems.Add(new KeyValuePair<int, IList<ILoan>>(borrowerId, new List<ILoan>()));
			}
			
		}

		/// <summary>
		/// Creates a new ILoan object for the member to borrow the book.
		/// </summary>
		/// <param name="borrower">The user borrowing the book.</param>
		/// <param name="book">The book being loaned.</param>
		/// <param name="borrowDate">The date the book has been borrowed.</param>
		/// <param name="dueDate">The date the book is due.</param>
		/// <returns>The new ILoan object that was added to the collection.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'borrower' parameter is null.</exception>
		/// <exception cref="System.ApplicationException">Thrown if no pending loan list exists for the borrower.</exception>
		public ILoan createPendingLoan(IMember borrower, IBook book, DateTime borrowDate, DateTime dueDate)
		{

			// Get the borrower ID and validate the borrower is a valid object and has a pending list in the pending loan map.
			int borrowerId = 0;
			validatePendingListExistsForBorrower(borrower, out borrowerId);

			// Get the max id for loan items for the borrower
			int maxId = getMaxId(borrowerId);

			// Create the new ILoan item
			ILoan newLoan = _helper.makeLoan(book, borrower, borrowDate, dueDate, (maxId + 1));

			// Add the loan to the pending list
			this._pendingItems[borrowerId].Add(newLoan);

			// return the new loan
			return newLoan;
		}

		/// <summary>
		/// Gets the list of pending loans for the borrower.
		/// </summary>
		/// <param name="borrower">The IMember object to get the list of loans for.</param>
		/// <returns>The list of pending loans for the borrower.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'borrower' parameter is null.</exception>
		/// <exception cref="System.ApplicationException">Thrown if no pending loan list exists for the borrower.</exception>
		public List<ILoan> getPendingList(IMember borrower)
		{

			// Get the borrower ID and validate the borrower is a valid object and has a pending list in the pending loan map.
			int borrowerId = 0;
			validatePendingListExistsForBorrower(borrower, out borrowerId);

			return (List<ILoan>)this._pendingItems[borrowerId];

		}

		/// <summary>
		/// Commits the loans in the pending loan list for the borrower.
		/// </summary>
		/// <param name="borrower">The borrower to commit the pending loan list for.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'borrower' parameter is null.</exception>
		/// <exception cref="System.ApplicationException">Thrown if no pending loan list exists for the borrower.</exception>
		public void commitPendingLoans(IMember borrower)
		{

			// Get the borrower ID and validate the borrower is a valid object and has a pending list in the pending loan map.
			int borrowerId = 0;
			validatePendingListExistsForBorrower(borrower, out borrowerId);

			// Iterate through the pending loan list for the borrower
			for(int i = 0; i < this._pendingItems[borrowerId].Count; i++) 
			{

				// Get the currently iterating pending loan
				ILoan loanToCommit = this._pendingItems[borrowerId][i];

				// Update te loan state to commited
				loanToCommit.commit();

				// add the loan to the commited list
				this._committedItems.Add(loanToCommit);
			}

			// Remove the pending list for the borrower
			this._pendingItems.Remove(borrowerId);

		}

		/// <summary>
		/// Clears the pending loan list for the specified borrower.
		/// </summary>
		/// <param name="borrower">THe borrower to clear the pending list for.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'borrower' parameter is null.</exception>
		/// <exception cref="System.ApplicationException">Thrown if no pending loan list exists for the borrower.</exception>
		public void clearPendingLoans(IMember borrower)
		{

			// Get the borrower ID and validate the borrower is a valid object and has a pending list in the pending loan map.
			int borrowerId = 0;
			validatePendingListExistsForBorrower(borrower, out borrowerId);

			// Clear all pending loan items for the borrower.
			this._pendingItems[borrowerId].Clear();
		}

		/// <summary>
		/// Get the loan from the committed loan list based on the provided loan id.
		/// </summary>
		/// <param name="id">The id of the committed loan to return.</param>
		/// <returns>The loan object with the id, otherwise null.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown if the id parameter is not a positive integer.</exception>
		public ILoan getLoanByID(int id)
		{

			// Validate the ID parameter
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException("id", "The 'id' parameter must be a positive integer.");
			}

			// Get the loan object based on the ID, or return null.
			return this._committedItems.FirstOrDefault(i => i.getID() == id);
		}

		/// <summary>
		/// Gets the loan based on the book provided.
		/// </summary>
		/// <param name="book">The book to match agaist the loan object.</param>
		/// <returns>The loan object with the specified book, otherwise null.</returns>
		public ILoan getLoanByBook(IBook book)
		{

			// If the book is null, return null as we can't match on Id
			if (book == null)
			{
				return null;
			}

			// Get the loan based on the book id.
			return this._committedItems.FirstOrDefault(i => i.getBook().getID() == book.getID());
		}

		/// <summary>
		/// Gets the complete list of committed loans.
		/// </summary>
		/// <returns>The list of committed loans.</returns>
		public List<ILoan> listLoans()
		{
			return (List<ILoan>)this._committedItems;
		}

		/// <summary>
		/// Finds a list of loans based on the borrower.
		/// </summary>
		/// <param name="borrower">The borrower to search loans for.</param>
		/// <returns>The list of loan objects for the specified borrower.</returns>
		public List<ILoan> findLoansByBorrower(IMember borrower)
		{

			// If the borrower is null, return an empty list as we can't match a null borrower
			if (borrower == null)
			{
				return new List<ILoan>();
			}

			// return the list of loan objects by the borrower.
			return this._committedItems.Where(i => i.getBorrower().getID() == borrower.getID()).ToList();
		}

		/// <summary>
		/// Find the list of loan objects based on the title of the book.
		/// </summary>
		/// <param name="title">The title of the book.</param>
		/// <returns>The list of loan objects with books matching the book title.</returns>
		public List<ILoan> findLoansByBookTitle(string title)
		{

			// If the title is null or empty, return empty list as we have nothing to match.
			if (String.IsNullOrEmpty(title))
			{
				return new List<ILoan>();
			}

			// Return the list of books that match against the book title.
			return this._committedItems.Where(i => i.getBook().getTitle().Equals(title, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		/// <summary>
		/// Iteertes through the committed loans and update any events that are overdue.
		/// </summary>
		/// <param name="currentDate">The current date to check against the due dates.</param>
		public void updateOverDueStatus(DateTime currentDate)
		{
			// Iterate through the committed loans and check for overdue loans
			for (int i = 0; i < this._committedItems.Count; i++)
			{

				// Get the loan
				ILoan loan = this._committedItems[i];

				// Check and update the overdue status
				loan.checkOverDue(currentDate);

				// Add the loan back into the list
				this._committedItems[i] = loan;

			}
		}

		/// <summary>
		/// Gets a list of all of the loans that are overdue.
		/// </summary>
		/// <returns>The list of overdue loans.</returns>
		public List<ILoan> findOverDueLoans()
		{
			return this._committedItems.Where(i => i.isOverDue()).ToList();
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Gets the maximum Id of the ILOan objects currently in the collection. 
		/// </summary>
		/// <returns>The maximum Id of the objects in the items collection.</returns>
		private int getMaxId(int borrowerId)
		{

			// Set the initial value
			int maxId = 0;

			// If there is no pending list, just return 0 as the max id, as we can't iterate through.
			if (!this._pendingItems.ContainsKey(borrowerId))
			{
				return 0;
			}

			// Iterate through the items in the collection
			foreach (ILoan loan in this._pendingItems[borrowerId])
			{
				// Get the loan id
				int loanId = loan.getID();

				// Set the loanId as the next maxId if it's greater than the current Id.
				maxId = (loanId > maxId ? loanId : maxId);
			}

			// return the maxId value
			return maxId;

		}

		/// <summary>
		/// Validates the borrower is not null and the borrower pending list exists. The function will return the borrower id.
		/// </summary>
		/// <param name="borrower">The borrow to validate the pending list for.</param>
		/// <param name="borrowerId">The Id of the borrower.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'borrower' parameter is null.</exception>
		/// <exception cref="System.ApplicationException">Thrown if no pending loan list exists for the borrower.</exception>
		private void validatePendingListExistsForBorrower(IMember borrower, out int borrowerId)
		{
			// Validate the borrower parameter
			if (borrower == null)
			{
				throw new ArgumentNullException("borrower", "The 'borrower' parameter cannot be null.");
			}

			// Get the borrower id
			borrowerId = borrower.getID();

			// If there is no key for the borrower ID, no pending list exists, so through the exception
			if (!this._pendingItems.ContainsKey(borrowerId))
			{
				throw new ApplicationException("No pending list exists for borrower.");
			}

		}

		#endregion
	}
}
