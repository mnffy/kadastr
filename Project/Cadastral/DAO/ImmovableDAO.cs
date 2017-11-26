using Cadastral.DataModel;
using Cadastral.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cadastral.DAO
{
    public class ImmovableDAO
    {
        private CadastraDBEntities _edm = new CadastraDBEntities();

        public async Task<IEnumerable<ImmovableViewModel>> GetImmovables() =>
            await (from im in _edm.Immovables
                   where im.ImmovableId > 0
                   select new ImmovableViewModel
                   {
                       ImmovableId = im.ImmovableId,
                       Address = im.Address,
                       Cost = im.Cost,
                       ImmovableType = new ImmovableTypeViewModel
                       {
                           ImmovableTypeId = im.ImmovableTypeId,
                           ImmovableTypeName = _edm.ImmovableTypes.FirstOrDefault(x => x.ImmovableTypeId == im.ImmovableTypeId).ImmovableName
                       },
                       Onwer = new OwnerViewModel
                       {
                           OwnerId = im.OwnerId,
                           Name = _edm.Owners.FirstOrDefault(x => x.OwnerId == im.OwnerId).Name,
                           Surname = _edm.Owners.FirstOrDefault(x => x.OwnerId == im.OwnerId).Surname
                       },
                       Cadastr = new CadastrViewModel
                       {
                           CadastrId = im.CadastrId,
                           CadastrName = im.Cadastr.Name
                       }
                   })
            .ToListAsync();

        public async Task<ImmovableViewModel> GetImmovableById(int id) =>
           await (from im in _edm.Immovables
                  where im.ImmovableId == id
                  select new ImmovableViewModel
                  {
                      ImmovableId = im.ImmovableId,
                      Address = im.Address,
                      Cost = im.Cost,
                      ImmovableType = new ImmovableTypeViewModel
                      {
                          ImmovableTypeId = im.ImmovableTypeId,
                          ImmovableTypeName = _edm.ImmovableTypes.FirstOrDefault(x => x.ImmovableTypeId == im.ImmovableTypeId).ImmovableName
                      },
                      Onwer = new OwnerViewModel
                      {
                          OwnerId = im.OwnerId,
                          Name = _edm.Owners.FirstOrDefault(x => x.OwnerId == im.OwnerId).Name,
                          Surname = _edm.Owners.FirstOrDefault(x => x.OwnerId == im.OwnerId).Surname
                      },
                      Cadastr = new CadastrViewModel
                      {
                          CadastrId = im.CadastrId,
                          CadastrName = im.Cadastr.Name
                      }
                  })
           .FirstOrDefaultAsync();

        public async Task EditImmovable(ImmovableViewModel model)
        {
            var entity = await _edm.Immovables.FirstOrDefaultAsync(x => x.ImmovableId == model.ImmovableId);
            if (entity == null)
                throw new Exception("Не найдена сущность для редактирования");
            entity.Address = model.Address;
            entity.Cost = model.Cost;
            entity.ImmovableTypeId = model.ImmovableType.ImmovableTypeId;
            entity.OwnerId = model.Onwer.OwnerId;
            entity.CadastrId = model.Cadastr.CadastrId;
            await _edm.SaveChangesAsync();
        }
        public async Task RemoveImmovable(int id)
        {
            var entity = await _edm.Immovables.FirstOrDefaultAsync(x => x.ImmovableId == id);
            if (entity == null)
                throw new Exception("Не найдена сущность для удаления");
            _edm.Immovables.Remove(entity);
            await _edm.SaveChangesAsync();
        }

        public async Task CreateImmovable(ImmovableViewModel model)
        {
            if (model == null)
                throw new Exception("Модель для добавления нового недвижимого имущества пуста!");
            var immovable = new Immovable
            {
                Address = model.Address,
                Cost = model.Cost,
                ImmovableTypeId = model.ImmovableType.ImmovableTypeId,
                OwnerId = model.Onwer.OwnerId,
                CadastrId = model.Cadastr.CadastrId
            };
            _edm.Immovables.Add(immovable);
            await _edm.SaveChangesAsync();
        }
    }
}