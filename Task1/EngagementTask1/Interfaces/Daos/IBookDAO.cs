using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngagementTask1.Interfaces.Entities;
using EngagementTask1.Interfaces.Helpers;

namespace EngagementTask1.Interfaces.Daos
{
    interface IBookDAO
    {
        IBook addBook(string author, string title, string callNo);

        IBook getBookByID(int id);

        List<IBook> listBooks();

        List<IBook> findBooksByAuthor(string author);

        List<IBook> findBooksByTitle(string title);

        List<IBook> findBooksByAuthorTitle(string author, string title);

    }
}
