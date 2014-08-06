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
	public class MemberDAO : IMemberDAO
	{

		/// <summary>
		/// Contains the collection of IMember items.
		/// </summary>
		private IList<IMember> _items;

		/// <summary>
		/// The helper classed used to create IMember objects.
		/// </summary>
		private IMemberHelper _helper;

		/// <summary>
		/// Creates a new instance of the Member Data Access Object.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">Thrown if the 'helper' parameter is null.</exception>
		public MemberDAO(IMemberHelper helper)
		{

			// Ensure the helper is not null
			if (helper == null)
			{
				throw new ArgumentNullException("helper", "The 'helper' parameter cannot be null.");
			}

			// Instantiate the member list
			this._items = new List<IMember>();

			// Set the helper object
			this._helper = helper;
		}

		#region IMemberDAO interface methods

		public IMember addMember(string firstName, string lastName, string ContactPhone, string emailAddress)
		{

			// Get the max member id
			int maxId = getMaxId();

			// Create the IMember object
			IMember member = new Member(firstName, lastName, ContactPhone, emailAddress, maxId + 1);

			// add to the list
			_items.Add(member);

			// return the new member object
			return member;
		}

		/// <summary>
		/// Get the Member object based on the supplied id. If no member with that Id is found, null is returned.
		/// </summary>
		/// <param name="id">The id of the member to search for.</param>
		/// <returns>The member object, or null if not found.</returns>
		public IMember getMemberByID(int id)
		{
			// Return the member based on the Id.
			return this._items.FirstOrDefault(i => i.getID() == id);
		}

		/// <summary>
		/// Gets the list of members currently in the collection.
		/// </summary>
		/// <returns>The list of members.</returns>
		public List<IMember> listMembers()
		{
			// Return the collection of members
			return (List<IMember>)this._items;
		}

		/// <summary>
		/// Gets a collection of members that match the last name supplied.
		/// </summary>
		/// <param name="lastName">The last name to search for.</param>
		/// <returns>The list of members with matching last names to the 'lastName' parameter.</returns>
		public List<IMember> findMembersByLastName(string lastName)
		{
			return this._items.Where(i => i.getLastName().Equals(lastName, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		/// <summary>
		/// Gets a collection members that match the email address supplied.
		/// </summary>
		/// <param name="emailAddress">The email address to search for.</param>
		/// <returns>The list of members with matching email addresses to the 'emailAddress' parameter.</returns>
		public List<IMember> findMembersByEmailAddress(string emailAddress)
		{
			return this._items.Where(i => i.getEmailAddress().Equals(emailAddress, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		/// <summary>
		/// Gets a collection of members that match the first name and last name supplied.
		/// </summary>
		/// <param name="firstName">The first name to search for.</param>
		/// <param name="lastName">The last name to search for.</param>
		/// <returns>A list of members whose first and last names match the parameters supplied.</returns>
		public List<IMember> findMembersByNames(string firstName, string lastName)
		{
			return this._items.Where(i => i.getFirstName().Equals(firstName, StringComparison.CurrentCultureIgnoreCase) && i.getLastName().Equals(lastName, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Gets the maximum Id of the IMember objects currently in the collection. 
		/// </summary>
		/// <returns>The maximum Id of the objects in the items collection.</returns>
		private int getMaxId()
		{

			// Set the initial value
			int maxId = 0;

			// Iterate through the items in the collection
			foreach (IMember member in this._items)
			{
				// Get the member id
				int memberId = member.getID();

				// Set the memeberId as the next maxId if it's greater than the current Id.
				maxId = (memberId > maxId ? memberId : maxId);
			}

			// return the maxId value
			return maxId;

		}

		#endregion

	}
}
