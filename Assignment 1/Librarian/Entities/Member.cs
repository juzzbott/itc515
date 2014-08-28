using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Daos;
using Librarian.Helpers;
using Librarian.Interfaces.Daos;
using Librarian.Interfaces.Entities;

namespace Librarian.Entities
{
	public class Member : IMember
	{

		#region Member fields

		/// <summary>
		/// The Id of the member.
		/// </summary>
		private int _id;

		/// <summary>
		/// The members' first name.
		/// </summary>
		private string _firstName;

		/// <summary>
		/// The members' last name.
		/// </summary>
		private string _lastName;

		/// <summary>
		/// The contact phone for the member.
		/// </summary>
		private string _contactPhone;

		/// <summary>
		/// The members' email address.
		/// </summary>
		private string _emailAddress;

		/// <summary>
		/// The state of the current member.
		/// </summary>
		private MemberConstants.MemberState _memberState;

		/// <summary>
		/// Determines the fine amount for the member.
		/// </summary>
		private float _fineAmount;
		
		/// <summary>
		/// The list containing the loans
		/// </summary>
		private List<ILoan> _loanList;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of the Member object.
		/// </summary>
		public Member(string firstName, string lastName, string contactPhone, string emailAddress, int id)
		{
			// Validate the first name
			if (String.IsNullOrEmpty(firstName))
			{
				throw new ArgumentException("The 'firstName' parameter cannot be null or empty.", "firstName");
			}

			// Validate the last name
			if (String.IsNullOrEmpty(lastName))
			{
				throw new ArgumentException("The 'lastName' parameter cannot be null or empty.", "lastName");
			}

			// Validate the contact phone
			if (String.IsNullOrEmpty(contactPhone))
			{
				throw new ArgumentException("The 'contactPhone' parameter cannot be null or empty.", "contactPhone");
			}

			// Validate the email address
			if (String.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentException("The 'emailAddress' parameter cannot be null or empty.", "emailAddress");
			}

			// Ensure the id is a positive integer
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException("id", "The 'id' parameter must be a positive integer.");
			}

			// Set the fields
			this._id = id;
			this._firstName = firstName;
			this._lastName = lastName;
			this._contactPhone = contactPhone;
			this._emailAddress = emailAddress;

			// Set the default member state
			this._memberState = MemberConstants.MemberState.BORROWING_ALLOWED;

			// Set the fine amount to 0
			this._fineAmount = 0.0f;

			// Create the loan list.
			this._loanList = new List<ILoan>();


		}

		#endregion

		#region IMember interface members

		/// <summary>
		/// Determines if any of the loans for the member are overdue.
		/// </summary>
		/// <returns>True if the loans are overdue, otherwise false.</returns>
		public bool hasOverDueLoans()
		{
		
			// If any of the loans are overdue, return true, otherwise false
			return _loanList.Any(i => i.isOverDue());
		}

		/// <summary>
		/// Determines if the user has reached the limit of the amount of loans they can have.
		/// </summary>
		/// <returns>True if the loan limit is reached, otherwise false;</returns>
		public bool hasReachedLoanLimit()
		{
			return (_loanList.Count >= MemberConstants.LOAN_LIMIT);

		}

		/// <summary>
		/// Determines if the user has any fines payable.
		/// </summary>
		/// <returns>True if the user has any fines, otherwise false.</returns>
		public bool hasFinesPayable()
		{
			return (this._fineAmount > 0.0);
		}

		/// <summary>
		/// Determines if the user has reached the maximum limit of the fine amount.
		/// </summary>
		/// <returns>True if the fine limit has been reached, otherwise false.</returns>
		public bool hasReachedFineLimit()
		{
			return (this._fineAmount >= MemberConstants.FINE_LIMIT);
		}

		/// <summary>
		/// Gets the current fine amount for the user.
		/// </summary>
		/// <returns>The fine amount</returns>
		public float getFineAmount()
		{
			return this._fineAmount;
		}

		/// <summary>
		/// Adds the fine amount to members existing fine amount.
		/// </summary>
		/// <param name="fine">The fine amount to add.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the fine amount is negative.</exception>
		public void addFine(float fine)
		{
			// If the fine is negative, throw exception
			if (fine < 0)
			{
				throw new ArgumentOutOfRangeException("fine", "The fine amount cannot be negative.");
			}

			// Add the fine amount
			this._fineAmount += fine;
		}

		/// <summary>
		/// Pays the 'payment' value of a fine. Payment does not need to be full amount. Payment amount cannot be negative.
		/// </summary>
		/// <param name="payment">The payment to make.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the payment amount is negative.</exception>
		public void payFine(float payment)
		{
			// If the payment is negative, throw exception
			if (payment < 0)
			{
				throw new ArgumentOutOfRangeException("payment", "The payment amount cannot be negative.");
			}

			// Deduct the payment amount
			this._fineAmount -= payment;
		}

		/// <summary>
		/// Adds the ILoan to the members current loans
		/// </summary>
		/// <param name="loan">The ILoan object to add to the collection.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the loan parameter is null.</exception>
		/// <exception cref="System.ApplicationException">Thrown if the current member is in the BORROWING_DISALLOWED state.</exception>
		public void addLoan(ILoan loan)
		{

			// If the loan is null, throw exception
			if (loan == null)
			{
				throw new ArgumentNullException("loan", "The 'loan' parameter cannot be null.");
			}

			// If the borrower is disabled, throw application exception
			if (this._memberState == MemberConstants.MemberState.BORROWING_DISALLOWED)
			{
				throw new ApplicationException("The current member is disallowed from borrowing.");
			}

			// Add the loan to the members loan list
			_loanList.Add(loan);
		}

		/// <summary>
		/// Gets a list of loans for the current member.
		/// </summary>
		/// <returns>The list of loans for the user.</returns>
		public List<ILoan> getLoans()
		{
			// return the loans for the user.
			return _loanList;
		}

		/// <summary>
		/// Removes the loan from the list of loans for the member.
		/// </summary>
		/// <param name="loan">The loan to remove.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the loan parameter is null.</exception>
		/// <exception cref="System.ApplicationException">Thrown if the loan does not exist in the list of loans for the member.</exception>
		public void removeLoan(ILoan loan)
		{

			// If the loan is null, throw exception
			if (loan == null)
			{
				throw new ArgumentNullException("loan", "The 'loan' parameter cannot be null.");
			}

			// If the borrower is disabled, throw application exception
			if (!_loanList.Contains(loan))
			{
				throw new ApplicationException("The loan does not exist in the loan list.");
			}

			// Remove the loan
			_loanList.Remove(loan);

		}

		/// <summary>
		/// Gets the first name field.
		/// </summary>
		/// <returns>The members' first name.</returns>
		public string getFirstName()
		{
			return this._firstName;
		}

		/// <summary>
		/// Gets the last name.
		/// </summary>
		/// <returns>The members' last name.</returns>
		public string getLastName()
		{
			return this._lastName;
		}

		/// <summary>
		/// Gets the contact phone number.
		/// </summary>
		/// <returns>The members' contact phone.</returns>
		public string getContactPhone()
		{
			return this._contactPhone;
		}

		/// <summary>
		/// Gets the email address.
		/// </summary>
		/// <returns>The users' email address.</returns>
		public string getEmailAddress()
		{
			return this._emailAddress;
		}

		/// <summary>
		/// Gets the Id of the member.
		/// </summary>
		/// <returns>The Id of the member.</returns>
		public int getID()
		{
			return this._id;
		}

		/// <summary>
		/// Gets the current member state.
		/// </summary>
		/// <returns>The state of the member.</returns>
		public MemberConstants.MemberState getState()
		{
			return this._memberState;
		}

		#endregion

	}
}
