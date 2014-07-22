using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngagementTask1.Interfaces.Entities;

namespace EngagementTask1.Interfaces.Helpers
{
    interface IMemberHelper
    {
        IMember makeMember(string firstName, string lastName, string contactPhone, string emailAddress, int id);

    }
}
