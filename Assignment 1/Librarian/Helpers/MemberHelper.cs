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
	public class MemberHelper : IMemberHelper
	{

		/// <summary>
		/// Creates a new IMember object.
		/// </summary>
		/// <param name="firstName">The first name of the member.</param>
		/// <param name="lastName">The last name of the member.</param>
		/// <param name="contactPhone">A contact phone number for the member.</param>
		/// <param name="emailAddress">The contact email address for the member.</param>
		/// <param name="id">The ID of the member.</param>
		/// <returns>A new IMember object.</returns>
		public IMember makeMember(string firstName, string lastName, string contactPhone, string emailAddress, int id)
		{
			// Create the new member object
			IMember newMember = new Member(firstName, lastName, contactPhone, emailAddress, id);

			// return the new member object
			return newMember;
		}
	}
}
