using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_DAL_2.Models
{
    [Flags]
    public enum UserRole
    {
        Librarian = 1,
        Reader = 2,
    }
}
