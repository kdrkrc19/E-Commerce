using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryEmailUs : IRepositoryBase<EmailUs>
    {
        EmailUs GetEmail(int id, bool trackChanges);
    }
}
