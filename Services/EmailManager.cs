using AutoMapper;
using Entities;
using Entities.Models;
using Entities.ModelsDTO;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailManager : IEmailService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDataShaper<EmailUsDto> _dataShaper;

        public EmailManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<EmailUsDto> dataShaper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
        public EmailUsDto CreateMail(EmailUsDto mail)
        {
            var data = _mapper.Map<EmailUs>(mail);
            data.MailId = 0;
            _repositoryManager.EmailUs.GenericCreate(data);
            _repositoryManager.Save();
            return mail;
        }

        public IEnumerable<ExpandoObject> GetAllMailsList(RequestParameters parameters, bool trackChanges)
        {
            List<EmailUsDto> emailUsDto = new List<EmailUsDto>();

            var mail = _repositoryManager.EmailUs.GenericRead(trackChanges);

            foreach (var item in mail) 
            {
                EmailUsDto emailUsDto2 = new EmailUsDto();

                emailUsDto2.MailId = item.MailId;
                emailUsDto2.Name = item.Name;
                emailUsDto2.Subject = item.Subject;
                emailUsDto2.EMail = item.EMail;
                emailUsDto2.textarea = item.textarea;

                emailUsDto.Add(emailUsDto2);
            }
            var shapeData = _dataShaper.ShapeDataList(emailUsDto, parameters.Fields);
            return shapeData;
        }

        public EmailUs GetMail(int id, bool trackChanges)
        {
            var mail = _repositoryManager.EmailUs.GetEmail(id, trackChanges);
            return mail;
        }
    }
}
