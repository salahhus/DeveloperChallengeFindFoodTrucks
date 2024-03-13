using FindFoodTrucksWebApp.Controllers;
using FindFoodTrucksWebApp.Interfaces;
using FindFoodTrucksWebApp.Models;
using FindFoodTrucksWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;


namespace FindFoodTracksWebApi.Test
{
    public class UnitTest1
    {
        MobileFoodTruckController _controller;
        IMobileFoodTruckService _mobileFoodTruckService;

        public UnitTest1()
        {
            var mock = new Mock<ILogger<MobileFoodTruckService>>();
            ILogger<MobileFoodTruckService> logger = mock.Object;

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            this._mobileFoodTruckService = new MobileFoodTruckService(logger, configuration);
            this._controller = new MobileFoodTruckController(this._mobileFoodTruckService);
        }

        [Fact]
        public void GetAllFoodTracks_Success()
        {
            // Arrange
            InputModel inputModel = new InputModel()
            {
                AmountOfResults = 100,
                Latitude = "37.7600869319869",
                Longitude = "122.418806481101",
                PreferredFood = "Hot dog"
            };

            // Act
            var result = this._controller.GetFilteredMobileFoodTruck(inputModel);
            var resultType = result as ViewResult;
            var resultTypeList = resultType?.Model as IEnumerable<MobileFoodFacilityPermitModel>;

            // Assert
            Assert.NotNull(resultTypeList);
            Assert.IsAssignableFrom<IEnumerable<MobileFoodFacilityPermitModel>>(resultTypeList);
            Assert.Single(resultTypeList);
        }
    }
}