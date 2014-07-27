using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Interfaces.Entities
{
    interface IBook
    {
        void borrow(ILoan loan);

        ILoan getLoan();

        void returnBook(bool damaged);

        void lose();

        void repair();

        void dispose();

        BookConstants.BookState getState();

        string getAuthor();

        string getTitle();

        string getCallNumber();

        int getID();

    }
}
