using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Utilities.Exceptions
{
    public class NewsManageException : Exception
    {
        public NewsManageException()
        {
        }

        public NewsManageException(string message)
            : base(message)
        {
        }

        public NewsManageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
