using AutoMapper;
using Entities.Models;
using Entities.ModelsDTO;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BrandManager : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public BrandManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public BrandsDto CreateBrand(BrandsDto brand)
        {
            var data = _mapper.Map<Brand>(brand);
            data.BrandId = 0;
            _repositoryManager.Brand.GenericCreate(data);
            _repositoryManager.Save();
            return brand;
        }

        public void DeleteBrand(int id)
        {
            var brand = _repositoryManager.Brand.GetBrand(id, false);
            _repositoryManager.Brand.GenericDelete((Brand)brand);
        }

        public IEnumerable<Brand> GetAllBrands(bool trackChanges)
        {
            return _repositoryManager.Brand.GenericRead(trackChanges);
        }

        public Brand GetBrand(int id, bool trackChanges)
        {
            var brand = _repositoryManager.Brand.GetBrand(id, false);
            return brand;
        }

        public void UpdateBrand(BrandsDto brand)
        {
            int id = brand.BrandId;
            var findStudent = _repositoryManager.Brand.GetBrand(id, false);
            if (findStudent != null)
            {
                var data = _mapper.Map<Brand>(brand);
                //findStudent.Name = student.Name;
                //findStudent.Surname = student.Surname;
                _repositoryManager.Brand.GenericUpdate(data);
                _repositoryManager.Save();
            }
        }
    }
}
