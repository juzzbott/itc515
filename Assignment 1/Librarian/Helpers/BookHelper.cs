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
	public class BookHelper : IBookHelper
	{
		/// <summary>
		/// Builds a new IBook object.
		/// </summary>
		/// <param name="author">The author of the book.</param>
		/// <param name="title">The title of the book.</param>
		/// <param name="callNumber">The call number for the book.</param>
		/// <param name="id">The ID of the book.</param>
		/// <returns>The new IBook object.</returns>
		public IBook makeBook(string author, string title, string callNumber, int id)
		{
			// Create the book
			IBook newBook = new Book(author, title, callNumber, id);

			// return the book
			return newBook;
		}
	}
}
