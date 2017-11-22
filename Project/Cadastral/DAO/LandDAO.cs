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
    public class LandDAO
    {
        private CadastraDBEntities _edmx = new CadastraDBEntities();

        public LandDAO()
        {

        }

        public async Task<List<LandViewModel>> GetLands() =>
            await (from land in _edmx.Lands
                   where land.LandId > 0
                   select new LandViewModel
                   {
                       LandId = land.LandId,
                       Address = land.Address,
                       Area = land.Area,
                       Cost = land.Cost,
                       LandType = new LandTypeViewModel
                       {
                           LandTypeId = land.LandTypeId,
                           LandTypeName = land.LandType.Name.ToString()
                       },
                       Owner = new OwnerViewModel
                       {
                           OwnerId = land.OwnerId,
                           Name = land.Owner.Name,
                           Surname = land.Owner.Surname,
                           BirthDate = land.Owner.DateBirth
                       }
                   })
            .ToListAsync();

        public async Task<List<LandViewModel>> GetLandById(int landId) =>
            await (from land in _edmx.Lands
                   where land.LandId == landId
                   select new LandViewModel
                   {
                       LandId = land.LandId,
                       Address = land.Address,
                       Area = land.Area,
                       Cost = land.Cost,
                       LandType = new LandTypeViewModel
                       {
                           LandTypeId = land.LandTypeId,
                           LandTypeName = land.LandType.Name.ToString()
                       },
                       Owner = new OwnerViewModel
                       {
                           OwnerId = land.OwnerId,
                           Name = land.Owner.Name,
                           Surname = land.Owner.Surname,
                           BirthDate = land.Owner.DateBirth
                       }
                   })
            .ToListAsync();

        public async Task EditLand(LandViewModel model)
        {
            var entity = await (from land in _edmx.Lands
                                where land.LandId == model.LandId
                                select land).FirstOrDefaultAsync();
            if (entity == null)
                throw new Exception("Модель для редактирования пустая");

            entity.LandTypeId = model.LandType.LandTypeId;
            entity.OwnerId = model.Owner.OwnerId;
            entity.Address = model.Address;
            entity.Area = model.Area;
            entity.Cost = model.Cost;
            await _edmx.SaveChangesAsync();
        }

        public async Task CreateLand(LandViewModel model)
        {
            Land land = new Land
            {
                Address = model.Address,
                Area = model.Area,
                Cost = model.Cost,
                LandTypeId = model.LandType.LandTypeId,
                OwnerId = model.Owner.OwnerId
            };
            _edmx.Lands.Add(land);
            await _edmx.SaveChangesAsync();
        }

        public async Task RemoveLand(LandViewModel model)
        {
            var entity = await (from land in _edmx.Lands
                                where land.LandId == model.LandId
                                select land).FirstOrDefaultAsync();
            if (entity == null)
                throw new Exception("Модель для удаления пустая!");
            _edmx.Lands.Remove(entity);
            await _edmx.SaveChangesAsync();
        }

        //Get охраняемые

    }
}