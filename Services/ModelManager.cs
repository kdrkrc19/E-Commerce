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
    public class ModelManager : IModelService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDataShaper<ModelsDto> _dataShaper;

        public ModelManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<ModelsDto> dataShaper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _dataShaper = dataShaper;
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
			_repositoryManager.Save();
		}

        public IEnumerable<ExpandoObject> GetAllModelList(RequestParameters parameters,bool trackChanges)
        {
            List<ModelsDto> modelsDto = new List<ModelsDto>();
            var model = _repositoryManager.Model.GenericRead(trackChanges);
        
            foreach (var item in model)
            {
                ModelsDto modelDto = new ModelsDto();

                modelDto.ModelName = item.ModelName;
                modelDto.ModelId = item.ModelId;

                modelsDto.Add(modelDto);
            }

            var shapeData = _dataShaper.ShapeDataList(modelsDto, parameters.Fields);
            return shapeData;
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

		public IEnumerable<ExpandoObject> GetPagedAndShapedModels(RequestParameters parameters, bool trackChanges)
		{
			var models = _repositoryManager.Model.GetPagedModels(parameters, trackChanges);

			// Product'ı ProductsDto'ya dönüştür
			var modelsDto = models.Select(m => new ModelsDto
			{
				ModelId = m.ModelId,
                ModelName = m.ModelName,
			});

			// Dönüştürülmüş ProductsDto koleksiyonunu ExpandoObject olarak şekillendir
			var shapeData = _dataShaper.ShapeDataList(modelsDto, parameters.Fields);
			return shapeData;
		}
	}
}
