using CsvHelper.Configuration;
using CsvHelper;
using FindFoodTrucksWebApp.Interfaces;
using FindFoodTrucksWebApp.Models;
using System.Globalization;

namespace FindFoodTrucksWebApp.Services
{
    public class MobileFoodTruckService : IMobileFoodTruckService
    {
        readonly ILogger<MobileFoodTruckService> _logger;
        readonly string  _filePath = Directory.GetCurrentDirectory();
        readonly IConfiguration _configuration;
        List<MobileFoodFacilityPermitModel> _mobileFoodFacilityPermits = new List<MobileFoodFacilityPermitModel>();
        public MobileFoodTruckService(ILogger<MobileFoodTruckService> logger, IConfiguration configuration)
        {
            this._logger = logger;
            _logger = logger;
            _configuration = configuration;
            this.ReadAllDataFromCSVFile();
        }

        private void ReadAllDataFromCSVFile() 
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(_filePath + this._configuration.GetSection("DataCSVFilePath").Value))
            using (var csv = new CsvReader(reader, configuration))
            {
                var records = csv.GetRecords<MobileFoodFacilityPermitModel>();
                this._mobileFoodFacilityPermits.AddRange(records);
            }
        }

        /// <summary>
        /// Serving for retriving all data from database
        /// </summary>
        /// <returns></returns>
        public ResultModel<IEnumerable<MobileFoodFacilityPermitModel>> GetMobileFoodFacilityPermitModelList(InputModel inputModel)
        {
            try
            {
                var filteredData = FilteringMobileFoodFacilityPermitModelByInputModel(inputModel, this._mobileFoodFacilityPermits);
                var resultData = new ResultModel<IEnumerable<MobileFoodFacilityPermitModel>>()
                {
                    Data = filteredData,
                    Errors = new List<Error>(),
                    IsSuccess = true
                };

                return resultData;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"ERROR : {ex}");
                throw;
            }
        }

        /// <summary>
        /// Filtering the data by input parameters,
        /// In this method I'm trying to clean the data. It seems that database contains any unneccessary data in columns...
        /// </summary>
        /// <param name="inputModel"></param>
        /// <param name="mobileFoodFacilityPermitModels"></param>
        /// <returns></returns>
        private IEnumerable<MobileFoodFacilityPermitModel> FilteringMobileFoodFacilityPermitModelByInputModel(InputModel inputModel, List<MobileFoodFacilityPermitModel> mobileFoodFacilityPermitModels) 
        {
            var filteredData = mobileFoodFacilityPermitModels.Where(x => x.FoodItems.Contains(inputModel.PreferredFood));

            double longitude = 0;
            double latitude = 0;
            List<CleanDataModel> cleanDataList = new List<CleanDataModel>();
            foreach (var item in filteredData) 
            {
                if (double.TryParse(item.Longitude, out longitude) && double.TryParse(item.Latitude, out latitude))
                {
                    CleanDataModel cleanDataModel = new CleanDataModel()
                    {
                        Latitude = latitude,
                        Longitude = longitude,
                        Locationid = item.Locationid
                    };   

                    cleanDataList.Add(cleanDataModel);
                } 
            }

            if (!inputModel.Longitude.StartsWith("-"))
            {
                inputModel.Longitude = "-" + inputModel.Longitude;
            }

            var data = cleanDataList.Where(x => x.Latitude <= double.Parse(inputModel.Latitude) && x.Longitude >= double.Parse(inputModel.Longitude)).Where(x=>x.Latitude != 0 && x.Longitude != 0).OrderBy(x=>x.Latitude);

            var mobileFoodFacilityPermits = new List<MobileFoodFacilityPermitModel>();
            foreach (var item in data)
            {
                var getCleanedData = filteredData.FirstOrDefault(x => x.Locationid == item.Locationid);
                if (getCleanedData != null) 
                {
                    MobileFoodFacilityPermitModel mobileFoodFacilityPermitModel = new MobileFoodFacilityPermitModel()
                    {
                         Longitude = getCleanedData.Longitude,
                         Latitude = getCleanedData.Latitude,
                         Address = getCleanedData.Address,
                         ApplicantName = getCleanedData.ApplicantName,
                         FacilityType = getCleanedData.FacilityType,
                         LocationDescription = getCleanedData.LocationDescription,
                         FoodItems = getCleanedData.FoodItems,
                         DaysHours = getCleanedData.DaysHours,
                    };

                    if (mobileFoodFacilityPermitModel.Longitude != "0" && mobileFoodFacilityPermitModel.Latitude != "0")
                    {
                        mobileFoodFacilityPermits.Add(mobileFoodFacilityPermitModel);
                    }
                }
            }

            if (inputModel.AmountOfResults != 0)
            {
                filteredData = mobileFoodFacilityPermits.Take(inputModel.AmountOfResults);
            }

            return filteredData.Where(x=>x.Latitude != "0" && x.Longitude != "0").OrderBy(x=>x.Latitude);
        }
    }
}
