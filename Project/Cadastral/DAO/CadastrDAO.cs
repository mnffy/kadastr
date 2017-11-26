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
    public class CadastrDAO
    {
        private CadastraDBEntities _edmx = new CadastraDBEntities();

        public CadastrDAO()
        {

        }

        public async Task<List<CadastrViewModel>> GetCadastras() =>
            await (from cad in _edmx.Cadastrs
                   where cad.CadastrId > 0
                   select new CadastrViewModel
                   {
                       CadastrId = cad.CadastrId,
                       CadastrName = cad.Name
                   })
            .ToListAsync();

        public async Task<CadastrViewModel> GetCadastrById(int cadId) =>
        await (from cad in _edmx.Cadastrs
               where cad.CadastrId == cadId
               select new CadastrViewModel
               {
                   CadastrId = cad.CadastrId,
                   CadastrName = cad.Name
               })
            .FirstOrDefaultAsync();


        public async Task EditCadastr(CadastrViewModel model)
        {
            var entity = await _edmx.Cadastrs.FirstOrDefaultAsync(x => x.CadastrId == model.CadastrId);
            if (entity == null)
                throw new Exception("Не найдена модель для редактирования");
            entity.Name = model.CadastrName;
            await _edmx.SaveChangesAsync();
        }

        public async Task RemoveCadastr(CadastrViewModel model)
        {
            var entity = await _edmx.Cadastrs.FirstOrDefaultAsync(x => x.CadastrId == model.CadastrId);
            if (entity == null)
                throw new Exception("Не найдена модель для редактирования");
            _edmx.Cadastrs.Remove(entity);
            await _edmx.SaveChangesAsync();
        }

        public async Task CreateCadastr(CadastrViewModel model)
        {
            Cadastr entity = new Cadastr
            {
                Name = model.CadastrName
            };
            _edmx.Cadastrs.Add(entity);
            await _edmx.SaveChangesAsync();
        }
    }
}