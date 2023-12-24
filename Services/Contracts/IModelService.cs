using Entities.Models;
using Entities.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IModelService
    {
        IEnumerable<Model> GetAllModelList(bool trackChanges);
        Model GetModel(int id, bool trackChanges);
        ModelsDto CreateModel(ModelsDto model);
        void UpdateModel(ModelsDto model);// ÖNCE GET YAPILACAK Sonra Set
        void DeleteModel(int id);// ÖNCE GET YAPILACAK Sonra Set
    }
}
