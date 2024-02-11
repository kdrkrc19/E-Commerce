using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryEmailUs : RepositoryBase<EmailUs>, IRepositoryEmailUs
    {
        private readonly RepositoryContext _context;
        public RepositoryEmailUs(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public EmailUs GetEmail(int id, bool trackChanges) => GenericReadExpression(x => x.MailId.Equals(id), trackChanges).SingleOrDefault();
    }
}
