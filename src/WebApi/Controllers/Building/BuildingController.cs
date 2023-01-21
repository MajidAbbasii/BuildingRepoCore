﻿using Application.Building;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using ServiceModel.Building;
using ServiceModel.Cost;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using WebApi.Controllers.BaseController;
using static WebApi.Controllers.Authentication.AuthenticationController;

namespace WebApi.Controllers.Building
{
    public class BuildingController : BaseController<BuildingRequest, BuildingResponse>
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }


        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ActionFilterModelStateValidation))]
        [Route("api/v1/[controller]/[action]")]
        public override async Task<BuildingResponse> GetById([FromBody] BuildingRequest request)
        {


            var Onebuilding = await _buildingRepository.GetById(request.theBuildingContract.Id);

            return new BuildingResponse()
            {
                theBuildingContractList = new List<BuildingContract>()
                {
                    new BuildingContract()
                    {

                        Address = Onebuilding.Address,
                        Plaque = Onebuilding.Plaque,
                        Title = Onebuilding.Title,
                        CityName = Onebuilding.CityName,
                        FloorCount = Onebuilding.FloorCount
                    }
                }
            };
        }

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ActionFilterModelStateValidation))]
        [Route("api/v1/[controller]/[action]")]
        public override async Task<BuildingResponse> GetAll()
        {

            //HMACSHA256 hmac = new HMACSHA256();
            //string key = Convert.ToBase64String(hmac.Key);
            var theBuildingList = await _buildingRepository.GetAll();

            return new BuildingResponse()
            {
                theBuildingContractList = theBuildingList.Select(x => new BuildingContract()
                {
                    Address = x.Address,
                    Plaque = x.Plaque,
                    Title = x.Title,
                    CityName = x.CityName,
                    FloorCount = x.FloorCount,
                }).ToList()
            };
        }

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ActionFilterModelStateValidation))]
        [Route("api/v1/[controller]/[action]")]
        public async Task<BuildingResponse> GetbyCityName([FromBody] BuildingRequest request)
        {
            var Thebuilding = await _buildingRepository.GetbyCityName(request.theBuildingContract.CityName);

            return new BuildingResponse()
            {
                theBuildingContractList = Thebuilding.Select(Onebuilding => new BuildingContract()
                {
                    Address = Onebuilding.Address,
                    Plaque = Onebuilding.Plaque,
                    Title = Onebuilding.Title,
                    CityName = Onebuilding.CityName,
                    FloorCount = Onebuilding.FloorCount
                }).ToList()
            };
        }

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ActionFilterModelStateValidation))]
        [Route("api/v1/[controller]/[action]")]
        public async Task<BuildingResponse> GetBuildingEager()
        {
            var theBuildingList = await _buildingRepository.GetBuildingEager();

            return new BuildingResponse()
            {
                theBuildingContractList = theBuildingList.Select(x => new BuildingContract()
                {
                    Address = x.Address,
                    Plaque = x.Plaque,
                    Title = x.Title,
                    CityName = x.CityName,
                    FloorCount = x.FloorCount,
                    TheCostList = x.TheCostList.Select(cost => new CostContract()
                    {
                        Amount = cost.Amount,
                        EventDate=cost.EventDate,
                        FromDate=cost.FromDate,
                        ToDate=cost.ToDate,
                        CashAmount=cost.CashAmount,

                    }).ToList()
                }).ToList()
            };
        }

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ActionFilterModelStateValidation))]
        [Route("api/v1/[controller]/[action]")]
        public async Task<BuildingResponse> GetBuildingExplicit()
        {
            var theBuildingList = _buildingRepository.GetBuildingExplicit();

            return new BuildingResponse()
            {
                theBuildingContractList = theBuildingList.Select(x => new BuildingContract()
                {
                    Address = x.Address,
                    Plaque = x.Plaque,
                    Title = x.Title,
                    CityName = x.CityName,
                    FloorCount = x.FloorCount,
                    TheCostList = x.TheCostList.Select(cost => new CostContract()
                    {
                        Amount = cost.Amount,
                        EventDate = cost.EventDate,
                        FromDate = cost.FromDate,
                        ToDate = cost.ToDate,
                        CashAmount = cost.CashAmount,
                        TheCostType=new CostTypeContract
                        {
                            Code=cost.TheCostType.Code,
                            Title = cost.TheCostType.Title
                        }

                    }).ToList()
                }).ToList()
            };
        }
    }
}