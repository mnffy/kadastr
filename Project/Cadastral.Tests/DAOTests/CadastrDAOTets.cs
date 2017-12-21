using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadastral.DAO;
using System.Threading.Tasks;
using Cadastral.Models;

namespace Cadastral.Tests.DAOTests
{

    [TestClass]
    public class CadastrDAOTets
    {

        [TestMethod]
        public async Task Get_CadastrBYId()
        {
            var cadastrDAO = new CadastrDAO();
            var cadastrEntity = await cadastrDAO.GetCadastrById(2);
            var cadastrModel = new CadastrViewModel()
            {
                CadastrId = 2,
                CadastrName = "Land"
            };
            Assert.AreEqual(cadastrEntity, cadastrModel);
        }

        public async Task Edit_Cadastr()
        {
            var cadDAO = new CadastrDAO();
            await cadDAO.EditCadastr(null);
        }

    }
}
