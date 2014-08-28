using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Librarian.Interfaces.Entities;
using Librarian.Interfaces.Helpers;

namespace Librarian.Interfaces.Daos
{
    public interface IMemberDAO
    {
        IMember addMember(string firstName, string lastName, string ContactPhone, string emailAddress);

        IMember getMemberByID(int id);

        List<IMember> listMembers();

        List<IMember> findMembersByLastName(string lastName);

        List<IMember> findMembersByEmailAddress(string emailAddress);

        List<IMember> findMembersByNames(string firstName, string lastName);

    }
}
