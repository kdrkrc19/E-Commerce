using Entities.Models;
using Entities.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAllBrands(bool trackChanges);
        Brand GetBrand(int id, bool trackChanges);
        BrandsDto CreateBrand(BrandsDto brand);
        void UpdateBrand(BrandsDto brand);// ÖNCE GET YAPILACAK Sonra Set
        void DeleteBrand(int id);// ÖNCE GET YAPILACAK Sonra Set
    }
}
