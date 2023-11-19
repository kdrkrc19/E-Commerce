using AutoMapper;
using Entities.Models;
using Entities.ModelsDTO;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ModelManager : IModelService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public ModelManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public ModelsDto CreateModel(ModelsDto model)
        {
            var data = _mapper.Map<Model>(model);
            data.ModelId = 0;
            _repositoryManager.Model.GenericCreate(data);
            _repositoryManager.Save();
            return model;
        }

        public void DeleteModel(int id)
        {
            var model = _repositoryManager.Model.GetModel(id, false);
            _repositoryManager.Model.GenericDelete((Model)model);
        }

        public IEnumerable<Model> GetAllModels(bool trackChanges)
        {
            return _repositoryManager.Model.GenericRead(trackChanges);
        }

        public Model GetModel(int id, bool trackChanges)
        {
            var model = _repositoryManager.Model.GetModel(id, false);
            return model;
        }

        public void UpdateModel(ModelsDto model)
        {
            int id = model.ModelId;
            var findStudent = _repositoryManager.Model.GetModel(id, false);
            if (findStudent != null)
            {
                var data = _mapper.Map<Model>(model);
                //findStudent.Name = student.Name;
                //findStudent.Surname = student.Surname;
                _repositoryManager.Model.GenericUpdate(data);
                _repositoryManager.Save();
            }
        }
    }
}
