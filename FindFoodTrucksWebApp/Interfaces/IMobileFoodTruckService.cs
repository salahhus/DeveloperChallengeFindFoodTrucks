using FindFoodTrucksWebApp.Models;

namespace FindFoodTrucksWebApp.Interfaces
{
    public interface IMobileFoodTruckService
    {
        public  ResultModel<IEnumerable<MobileFoodFacilityPermitModel>> GetMobileFoodFacilityPermitModelList(InputModel inputModel);
    }
}
