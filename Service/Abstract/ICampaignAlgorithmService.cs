using System.Collections.Generic;
using DaikinProject.Models;
using HepsiburadaCase.Models;

namespace HepsiburadaCase.Service.Abstract {
    public interface ICampaignAlgorithmService {

        ServiceResult<ProductViewModel> GetProduct (ProductViewModel model);
        ServiceResult<ProductViewModel> CreateProduct (ProductViewModel model);
        ServiceResult<OrderViewModel> CreateOrder (OrderViewModel model);
        ServiceResult<CampaignViewModel> CreateCampaign (CampaignViewModel model);
        ServiceResult<CampaignViewModel> GetCampaign (CampaignViewModel model);
        ServiceResult<int> IncreaseTime (int hour);
    }
}