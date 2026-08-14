using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Data_Interface
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
