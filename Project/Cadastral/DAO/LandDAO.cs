using Cadastral.DataModel;
using Cadastral.Models;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LandDAO()
        {

        }

        public async Task<List<LandViewModel>> GetLands() =>
            await (from land in _edmx.Lands
                   where land.CadastrId == 2
                   select new LandViewModel
                   {
                       Cadastr = new CadastrViewModel
                       {
                           CadastrId = land.CadastrId,
                           CadastrName = land.Cadastr.Name
                       },
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

        public async Task<LandViewModel> GetLandById(int landId) =>
            await (from land in _edmx.Lands
                   where land.LandId == landId
                   select new LandViewModel
                   {
                       Cadastr = new CadastrViewModel
                       {
                           CadastrId = land.CadastrId,
                           CadastrName = land.Cadastr.Name
                       },
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
            .FirstOrDefaultAsync();

        public async Task EditLand(LandViewModel model)
        {
            logger.Debug("Редактирование земельного участка");
            var entity = await (from land in _edmx.Lands
                                where land.LandId == model.LandId
                                select land).FirstOrDefaultAsync();
            if (entity == null)
            {
                logger.Error("Модель для редактирования пустая");
                throw new Exception("Модель для редактирования пустая");
            }
            entity.LandTypeId = model.LandType.LandTypeId > 0 ? model.LandType.LandTypeId : entity.LandTypeId;
            entity.OwnerId = model.Owner.OwnerId > 0 ? model.Owner.OwnerId : entity.OwnerId;
            entity.Address = model.Address;
            entity.Area = model.Area;
            entity.Cost = model.Cost;
            entity.CadastrId = model.Cadastr.CadastrId > 0 ? model.Cadastr.CadastrId : entity.CadastrId;   
            if(entity.CadastrId == 2)
            {
                var license = new LicenseRequest
                {
                    LandId = entity.LandId,
                    LicenseReqState = "Not confirmed"
                };
                _edmx.LicenseRequests.Add(license);
            }
            await _edmx.SaveChangesAsync();
        }

        public async Task CreateLand(LandViewModel model)
        {
            logger.Debug("Создание нового земельного участка");
            Land land = new Land
            {
                Address = model.Address,
                Area = model.Area,
                Cost = model.Cost,
                LandTypeId = model.LandType.LandTypeId,
                OwnerId = model.Owner.OwnerId,
                CadastrId = model.Cadastr.CadastrId
            };
            _edmx.Lands.Add(land);
            await _edmx.SaveChangesAsync();            
            logger.Debug("создание заявки со статусом Not Accepted");
            var license = new LicenseRequest
            {
                LandId = land.LandId,
                LicenseReqState = "Not Accepted"
            };
            _edmx.LicenseRequests.Add(license);
            await _edmx.SaveChangesAsync();
        }

        public async Task RemoveLand(LandViewModel model)
        {
            logger.Debug("Удаление земельного участка");
            var licensee = await _edmx.LicenseRequests.FirstOrDefaultAsync(x => x.LandId == model.LandId);
            var entity = await (from land in _edmx.Lands
                                where land.LandId == model.LandId
                                select land).FirstOrDefaultAsync();
            if (entity == null)
            {
                logger.Error("Модель для удаления пустая!");
                throw new Exception("Модель для удаления пустая!");
            }   
            if(licensee == null)
            {
                logger.Error("Модель для удаления пустая!");
                throw new Exception("Модель для удаления пустая!");
            }
            _edmx.LicenseRequests.Remove(licensee);
            _edmx.Lands.Remove(entity);
            await _edmx.SaveChangesAsync();
        }

        //Get охраняемые

    }
}