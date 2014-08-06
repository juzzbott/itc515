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
	public class BookDAO : IBookDAO
	{

		#region BookDAO Fields

		/// <summary>
		/// The collection containing the IBook objects
		/// </summary>
		private IList<IBook> _items;

		/// <summary>
		/// The helper used for object creation.
		/// </summary>
		private IBookHelper _helper;
 
		#endregion

		#region Contstructor

		public BookDAO(IBookHelper helper)
		{

			// Ensure the helper is not null
			if (helper == null)
			{
				throw new ArgumentNullException("helper", "The 'helper' parameter cannot be null.");
			}

			// Instantiate the collection of books
			this._items = new List<IBook>();

			// Set the helper field
			this._helper = helper;

		}

		#endregion

		#region IBookDAO interface members

		/// <summary>
		/// Adds a new book to the collection and returns the IBook object created.
		/// </summary>
		/// <param name="author">The author of the book.</param>
		/// <param name="title">The title of the book.</param>
		/// <param name="callNo">The call number for the book.</param>
		/// <returns>The created IBook object.</returns>
		public IBook addBook(string author, string title, string callNo)
		{
			// Get the max book id
			int maxId = getMaxId();

			// Create the IBook object
			IBook member = _helper.makeBook(author, title, callNo, maxId + 1);

			// add to the list
			_items.Add(member);

			// return the new member object
			return member;
		}

		/// <summary>
		/// Gets the IBook object that matches the specified id parameter. If no book with that id is found, null is returned.
		/// </summary>
		/// <param name="id">The Id of the book to search for.</param>
		/// <returns>The IBook object if the Id is matched, otherwise null.</returns>
		public IBook getBookByID(int id)
		{
			return this._items.FirstOrDefault(i => i.getID() == id);
		}

		/// <summary>
		/// Returns all the IBook objects currently in the collection.
		/// </summary>
		/// <returns>The list of books in the collection.</returns>
		public List<IBook> listBooks()
		{
			return (List<IBook>)this._items;
		}

		/// <summary>
		/// Returns a collection of books that match the author parameter.
		/// </summary>
		/// <param name="author">The author of books to search for.</param>
		/// <returns>The list of IBook objects with a matching author.</returns>
		public List<IBook> findBooksByAuthor(string author)
		{
			return this._items.Where(i => i.getAuthor().Equals(author, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		/// <summary>
		/// Returns a collection of books that match the title parameter.
		/// </summary>
		/// <param name="title">The title of the books to search for.</param>
		/// <returns>The list of IBook object with a matching title.</returns>
		public List<IBook> findBooksByTitle(string title)
		{
			return this._items.Where(i => i.getTitle().Equals(title, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		/// <summary>
		/// Returns a collection of books that match both the author and title parameters.
		/// </summary>
		/// <param name="author">The author of the books to search for.</param>
		/// <param name="title">The title of the books to search for.</param>
		/// <returns>The list of IBook object which match both the author and title.</returns>
		public List<IBook> findBooksByAuthorTitle(string author, string title)
		{
			return this._items.Where(i => i.getAuthor().Equals(author, StringComparison.CurrentCultureIgnoreCase) && i.getTitle().Equals(title, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Gets the maximum Id of the IBook objects currently in the collection. 
		/// </summary>
		/// <returns>The maximum Id of the objects in the items collection.</returns>
		private int getMaxId()
		{

			// Set the initial value
			int maxId = 0;

			// Iterate through the items in the collection
			foreach (IBook book in this._items)
			{
				// Get the book id
				int bookId = book.getID();

				// Set the bookId as the next maxId if it's greater than the current Id.
				maxId = (bookId > maxId ? bookId : maxId);
			}

			// return the maxId value
			return maxId;

		}

		#endregion
	}
}
