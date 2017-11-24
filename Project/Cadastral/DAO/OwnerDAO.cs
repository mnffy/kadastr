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
    public class OwnerDAO
    {
        private CadastraDBEntities _edm = new CadastraDBEntities();

        public OwnerDAO()
        {

        }

        public async Task<List<OwnerViewModel>> GetOwners() =>
            await (from owner in _edm.Owners
                   where owner.OwnerId > 0
                   select new OwnerViewModel
                   {
                       OwnerId = owner.OwnerId,
                       Name = owner.Name,
                       Surname = owner.Surname,
                       BirthDate = owner.DateBirth
                   })
            .ToListAsync();

        public async Task<OwnerViewModel> GetOwnerById(int ownerId) =>
             await (from owner in _edm.Owners
                    where owner.OwnerId > ownerId
                    select new OwnerViewModel
                    {
                        OwnerId = owner.OwnerId,
                        Name = owner.Name,
                        Surname = owner.Surname,
                        BirthDate = owner.DateBirth
                    })
            .FirstOrDefaultAsync();

        public async Task EditOwner(OwnerViewModel model)
        {
            var entity = await _edm.Owners.FirstOrDefaultAsync(x => x.OwnerId == model.OwnerId);
            if (entity == null)
                throw new Exception("Модель для редактирования пустая!");
            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.DateBirth = model.BirthDate;
            _edm.Owners.Add(entity);
            await _edm.SaveChangesAsync();
        }

        public async Task RemoveOwner(OwnerViewModel model)
        {
            var entity = await _edm.Owners.FirstOrDefaultAsync(x => x.OwnerId == model.OwnerId);
            if (entity == null)
                throw new Exception("Модель для редактирования пустая!");
            _edm.Owners.Remove(entity);
            await _edm.SaveChangesAsync();
        }

        public async Task CreateOwner(OwnerViewModel model)
        {
            Owner owner = new Owner
            {
                Name = model.Name,
                Surname = model.Surname,
                DateBirth = model.BirthDate
            };
            _edm.Owners.Add(owner);
            await _edm.SaveChangesAsync();
        }

    }
}