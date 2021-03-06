﻿using Cadastral.DataModel;
using Cadastral.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastral.DAO
{
    public class LicenseDAO
    {
        private CadastraDBEntities _edm = new CadastraDBEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LicenseDAO()
        {

        }

        public IEnumerable<LicenseRequestModel> GetAllRequests()
        {
            //получение всех лицензий
            var requests = (from license in _edm.LicenseRequests
                            join land in _edm.Lands on license.LandId equals land.LandId
                            join owner in _edm.Owners on land.OwnerId equals owner.OwnerId
                            select new LicenseRequestModel
                            {
                                LicenseId = license.LicenseId,
                                LiicenseRequestState = license.LicenseReqState,
                                Cadastr = new CadastrViewModel
                                {
                                    CadastrId = land.CadastrId,
                                    CadastrName = land.Cadastr.Name
                                },
                                Land = new LandViewModel
                                {
                                    LandId = land.LandId,
                                    Address = land.Address,
                                    Area = land.Area,
                                    Cost = land.Cost,
                                    LandType = new LandTypeViewModel
                                    {
                                        LandTypeId = land.LandTypeId,
                                        LandTypeName = land.LandType.Name
                                    }
                                },
                                Owner = new OwnerViewModel
                                {
                                    OwnerId = owner.OwnerId,
                                    Name = owner.Name,
                                    Surname = owner.Surname,
                                    BirthDate = owner.DateBirth
                                }
                            })
                           .ToList();
            return requests;
        }

        public LicenseRequestModel GetLicenseById(int id)
        {
            //получить лицензию по его айди
            var request = (from license in _edm.LicenseRequests
                           join land in _edm.Lands on license.LandId equals land.LandId
                           join owner in _edm.Owners on land.OwnerId equals owner.OwnerId
                           where license.LicenseId == id
                           select new LicenseRequestModel
                           {
                               LicenseId = license.LicenseId,
                               LiicenseRequestState = license.LicenseReqState,
                               Cadastr = new CadastrViewModel
                               {
                                   CadastrId = land.CadastrId,
                                   CadastrName = land.Cadastr.Name
                               },
                               Land = new LandViewModel
                               {
                                   LandId = land.LandId,
                                   Address = land.Address,
                                   Area = land.Area,
                                   Cost = land.Cost,
                                   LandType = new LandTypeViewModel
                                   {
                                       LandTypeId = land.LandTypeId,
                                       LandTypeName = land.LandType.Name
                                   }
                               },
                               Owner = new OwnerViewModel
                               {
                                   OwnerId = owner.OwnerId,
                                   Name = owner.Name,
                                   Surname = owner.Surname,
                                   BirthDate = owner.DateBirth
                               }
                           })
                          .FirstOrDefault();
            return request;
        }

        public void EditLicenseData(LicenseRequestModel model)
        {
            logger.Debug("Редактирование лицензии");
            var license = _edm.LicenseRequests.FirstOrDefault(x => x.LicenseId == model.LicenseId);
            if (license == null)
            {
                logger.Error("Нету данных для редактирования");
                throw new Exception("Что-то пошло не так");
            }   
            license.Land.LandId = model.Land.LandId;
            license.Land.LandTypeId = model.Land.LandType.LandTypeId;
            license.Land.Owner.OwnerId = model.Owner.OwnerId;
            license.Land.Area = model.Land.Area;
            license.Land.Cost = model.Land.Cost;
            license.Land.Address = model.Land.Address;
            _edm.SaveChanges();
        }

        public void Accept(int id)
        {
            logger.Debug("Установка статуса лицензии Accepted");
            var licEntity = _edm.LicenseRequests.FirstOrDefault(x => x.LicenseId == id);
            licEntity.LicenseReqState = "Accepted";
            _edm.SaveChanges();
        }

        public void Reject(int id)
        {
            logger.Debug("Установка статуса лицензии Accepted");
            var licEntity = _edm.LicenseRequests.FirstOrDefault(x => x.LicenseId == id);
            licEntity.LicenseReqState = "Rejected";
            _edm.SaveChanges();
        }

        public void NotAccepted(int id)
        {
            logger.Debug("Установка статуса лицензии Not Accepted");
            var licEntity = _edm.LicenseRequests.FirstOrDefault(x => x.LicenseId == id);
            licEntity.LicenseReqState = "Not Accepted";
            _edm.SaveChanges();
        }

        public void SentToRevision(int id)
        {
            logger.Debug("Установка статуса лицензии Sened for revision");
            var licEntity = _edm.LicenseRequests.FirstOrDefault(x => x.LicenseId == id);
            licEntity.LicenseReqState = "Sened for revision";
            _edm.SaveChanges();
        }

    }
}