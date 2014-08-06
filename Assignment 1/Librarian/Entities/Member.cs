using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		}

		#endregion

		#region IMember interface members

		public bool hasOverDueLoans()
		{
			throw new NotImplementedException();
		}

		public bool hasReachedLoanLimit()
		{
			throw new NotImplementedException();
		}

		public bool hasFinesPayable()
		{
			throw new NotImplementedException();
		}

		public bool hasReachedFineLimit()
		{
			throw new NotImplementedException();
		}

		public float getFineAmount()
		{
			throw new NotImplementedException();
		}

		public void addFine(float fine)
		{
			throw new NotImplementedException();
		}

		public void payFine(float payment)
		{
			throw new NotImplementedException();
		}

		public void addLoan(ILoan loan)
		{
			throw new NotImplementedException();
		}

		public List<ILoan> getLoans()
		{
			throw new NotImplementedException();
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
