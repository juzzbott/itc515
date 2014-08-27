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


		}

		#endregion

		#region IMember interface members

		/// <summary>
		/// Determines if any of the loans for the member are overdue.
		/// </summary>
		/// <returns>True if the loans are overdue, otherwise false.</returns>
		public bool hasOverDueLoans()
		{
			// Get the loans list
			List<ILoan> currentLoans = this.getLoans();

			// If any of the loans are overdue, return true, otherwise false
			return currentLoans.Any(i => i.checkOverDue(DateTime.Now));
		}

		/// <summary>
		/// Determines if the user has reached the limit of the amount of loans they can have.
		/// </summary>
		/// <returns>True if the loan limit is reached, otherwise false;</returns>
		public bool hasReachedLoanLimit()
		{
			// Get the loans list
			List<ILoan> currentLoans = this.getLoans();

			return (currentLoans.Count >= MemberConstants.LOAN_LIMIT);

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
		public void addFine(float fine)
		{
			// Add the fine amount
			this._fineAmount += fine;
		}

		public void payFine(float payment)
		{
			throw new NotImplementedException();
		}

		public void addLoan(ILoan loan)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a list of loans for the current member.
		/// </summary>
		/// <returns>The list of loans for the user.</returns>
		public List<ILoan> getLoans()
		{

			// Create the loan DAO.
			ILoanDAO loanDao = new LoanDAO(new LoanHelper());

			// return the loans for the user.
			return loanDao.findLoansByBorrower(this);
		}

		public void removeLoan(ILoan loan)
		{
			throw new NotImplementedException();
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
