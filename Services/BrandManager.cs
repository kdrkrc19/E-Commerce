using AutoMapper;
using Entities;
using Entities.Models;
using Entities.ModelsDTO;
using Repositories;
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
    public class BrandManager : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDataShaper<BrandsDto> _dataShaper;

        public BrandManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<BrandsDto> dataShaper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _dataShaper = dataShaper;
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
            _repositoryManager.Save();
        }

        public IEnumerable<ExpandoObject> GetAllBrandsList(RequestParameters parameters, bool trackChanges)
        {
            List<BrandsDto> brandsDto = new List<BrandsDto>();

            var brand = _repositoryManager.Brand.GenericRead(trackChanges);

            foreach(var item in brand) 
            {
                BrandsDto brandsDto2 = new BrandsDto();

                brandsDto2.BrandId = item.BrandId;
                brandsDto2.BrandName = item.BrandName;

                brandsDto.Add(brandsDto2);
            }

            var shapeData = _dataShaper.ShapeDataList(brandsDto, parameters.Fields);
            return shapeData;
        }

        public Brand GetBrand(int id, bool trackChanges)
        {
            var brand = _repositoryManager.Brand.GetBrand(id, trackChanges);
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

		public IEnumerable<ExpandoObject> GetPagedAndShapedBrands(RequestParameters parameters, bool trackChanges)
		{
			var brands = _repositoryManager.Brand.GetPagedBrands(parameters, trackChanges);

			// Product'ı ProductsDto'ya dönüştür
			var brandsDto = brands.Select(b => new BrandsDto
			{
				BrandId = b.BrandId,
				BrandName = b.BrandName
			});

			// Dönüştürülmüş ProductsDto koleksiyonunu ExpandoObject olarak şekillendir
			var shapeData = _dataShaper.ShapeDataList(brandsDto, parameters.Fields);
			return shapeData;
		}
	}
}
