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
    public interface IBrandService
    {
        IEnumerable<ExpandoObject> GetAllBrandsList(RequestParameters parameters ,bool trackChanges);
		IEnumerable<ExpandoObject> GetPagedAndShapedBrands(RequestParameters parameters, bool trackChanges);
		Brand GetBrand(int id, bool trackChanges);
        BrandsDto CreateBrand(BrandsDto brand);
        void UpdateBrand(BrandsDto brand);// ÖNCE GET YAPILACAK Sonra Set
        void DeleteBrand(int id);// ÖNCE GET YAPILACAK Sonra Set
    }
}
