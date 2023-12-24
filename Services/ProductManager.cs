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
    public class ProductManager : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDataShaper<ProductsDto> _dataShaper;

        public ProductManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<ProductsDto> dataShaper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        public ProductsDto CreateProduct(ProductsDto product)
        {
            var data = _mapper.Map<Product>(product);
            data.ProductId = 0;
            var brand = _repositoryManager.Brand.GetBrand(data.BrandId, false);
            var model = _repositoryManager.Model.GetModel(product.ModelId, false);
            data.ProductDescription = brand.BrandName + " " + model.ModelName + " " + data.ProductName;
            _repositoryManager.Product.GenericCreate(data);
            _repositoryManager.Save();
            return product;
        }

        public void DeleteProduct(int id)
        {
            var product = _repositoryManager.Product.GetProduct(id, false);
            _repositoryManager.Product.GenericDelete((Product)product);
        }

        public IEnumerable<ExpandoObject> GetAllProductsList(RequestParameters parameters, bool trackChanges)
        {
            List<ProductsDto> productsDto = new List<ProductsDto>();

            var product = _repositoryManager.Product.GenericRead(trackChanges);
            //var product = _repositoryManager.Product.GetPagedProducts(parameters, false);

            foreach (var item in product)
            {
                ProductsDto productDto2 = new ProductsDto();

                productDto2.ProductName = item.ProductName;
                productDto2.ProductId = item.ProductId;
                productDto2.ProductPrice = item.ProductPrice;
                productDto2.ProductStock = item.ProductStock;
                productDto2.ProductDescription = item.ProductDescription;
                productDto2.BrandId = item.BrandId;
                productDto2.ModelId = item.ModelId;

                productsDto.Add(productDto2);
            }
            

            var shapeData = _dataShaper.ShapeDataList(productsDto, parameters.Fields);
            return shapeData;
        }

        public Product GetProduct(int id, bool trackChanges)
        {
            var product = _repositoryManager.Product.GetProduct(id, false);
            return product;
        }

        public void UpdateProduct(ProductsDto product)
        {
            int id = product.ProductId;
            var findStudent = _repositoryManager.Product.GetProduct(id, false);
            if (findStudent != null)
            {
                var data = _mapper.Map<Product>(product);
                //findStudent.Name = student.Name;
                //findStudent.Surname = student.Surname;
                _repositoryManager.Product.GenericUpdate(data);
                _repositoryManager.Save();
            }
        }

        public IEnumerable<Product> GetAllProductsPagination(RequestParameters parameters, bool trackChanges)
        {
            
            var query = _repositoryManager.Product.GetPagedProducts(parameters, false);
            
            //if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            //{
                
            //    query = ApplySorting((IQueryable<Product>)query, parameters.OrderBy);
            //}

            return query.ToList();
        }

        private IQueryable<Product> ApplySorting(IQueryable<Product> query, string orderBy)
        {
            switch (orderBy)
            {
                case "ProductName":
                    return query.OrderBy(p => p.ProductName);
                case "ProductPrice":
                    return query.OrderBy(p => p.ProductPrice);
                default:
                    return query; 
            }
        }
    }
}
