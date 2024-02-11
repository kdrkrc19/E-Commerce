using Entities;
using Entities.Models;
using Entities.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEmailService
    {
        EmailUsDto CreateMail(EmailUsDto mail);
        IEnumerable<ExpandoObject> GetAllMailsList(RequestParameters parameters, bool trackChanges);
        EmailUs GetMail(int id, bool trackChanges);
    }
}
