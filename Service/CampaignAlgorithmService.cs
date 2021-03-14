using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DaikinProject.Models;
using HepsiburadaCase.Data;
using HepsiburadaCase.Data.Abstract;
using HepsiburadaCase.Data.Entity;
using HepsiburadaCase.Models;
using HepsiburadaCase.Service.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HepsiburadaCase.Service {
    public class CampaignAlgorithmService : ICampaignAlgorithmService {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public CampaignAlgorithmService (
            ApplicationDbContext context,
            ICampaignRepository campaignRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IOrderRepository orderRepository,
            ILogger<CampaignAlgorithmService> logger,
            IConfiguration configuration) {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _campaignRepository = campaignRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ServiceResult<ProductViewModel> CreateProduct (ProductViewModel model) {
            var result = new ServiceResult<ProductViewModel> ();

            try {
                var product = _productRepository.GetSingle (p => p.Code == model.Code);

                if (product != null) {
                    result.ResultType = ServiceResultType.Fail;
                    result.Message = "There is product with the same product code.";
                    return result;
                }

                var productEntity = _mapper.Map<Product> (model);
                _productRepository.Add (productEntity);
                _productRepository.Commit ();

                result.ResultType = ServiceResultType.Success;
                result.Data = _mapper.Map<ProductViewModel> (productEntity);;
                return result;

            } catch (Exception e) {
                _logger.LogError (e, "Error@CreateProduct:");
                result.Message = e.ToString ();
                result.ResultType = ServiceResultType.Fail;
                return result;
            }

        }

        public ServiceResult<CampaignViewModel> GetCampaign (CampaignViewModel model) {
            var serviceResult = new ServiceResult<CampaignViewModel> ();

            try {
                var campaign = _campaignRepository.AllIncludingAsQueryable (c => c.Product).FirstOrDefault (c => c.Name == model.Name && !c.IsDeleted);

                serviceResult.Data = _mapper.Map<CampaignViewModel> (campaign);
                serviceResult.Data.ProductCode = campaign.Product.Code;
                serviceResult.ResultType = ServiceResultType.Success;
                // serviceResult.Data.Product = _mapper.Map<ProductViewModel> (campaign.Product);

            } catch (Exception e) {

                _logger.LogError (e, "Error@GetCampaign:");
                serviceResult.Message = e.ToString ();
                serviceResult.ResultType = ServiceResultType.Fail;
                return serviceResult;;
            }
            return serviceResult;

        }

        public ServiceResult<int> IncreaseTime (int hour) {
            var serviceResult = new ServiceResult<int> ();

            var campaignList = _campaignRepository
                .AllIncludingAsQueryable (o => o.Orders, o => o.Product).ToList ();

            try {
                campaignList.ForEach (c => {
                    var duration = c.CurrentDuration + hour;
                    //Campaign not ended.
                    if (duration < c.Duration) {
                        c.CurrentDuration = duration;

                        var expectedSalesCountofHour = c.TargetSales / c.Duration;

                        var soldCountofHour = c.TotalSales / c.CurrentDuration;

                        var difference = expectedSalesCountofHour - soldCountofHour;

                        if (difference < 0) {

                            var remainingDuration = c.Duration - c.CurrentDuration;

                            var differenceTargetCountofHour = Math.Abs (difference) / remainingDuration;

                            var priceIncreasePercentage = differenceTargetCountofHour / expectedSalesCountofHour;

                            if (priceIncreasePercentage >= c.PriceManipulationLimit / 100)
                                priceIncreasePercentage = (int) c.PriceManipulationLimit / 100;

                            c.CurrentProductPrice = c.CurrentProductPrice + c.CurrentProductPrice * priceIncreasePercentage;
                        } else if (difference != 0) {

                            var idealTotalSales = soldCountofHour * c.CurrentProductPrice;
                            var newPriceOfProduct = idealTotalSales / expectedSalesCountofHour;
                            var priceDecreasePercentage = (c.CurrentProductPrice - newPriceOfProduct) / c.CurrentProductPrice;

                            if (priceDecreasePercentage >= c.PriceManipulationLimit / 100 || soldCountofHour == 0)
                                priceDecreasePercentage = (double) c.PriceManipulationLimit / 100;

                            c.CurrentProductPrice = c.CurrentProductPrice - c.CurrentProductPrice * priceDecreasePercentage;
                        }

                    } else {
                        //Campaign ended.
                        c.CurrentDuration = c.Duration;
                        c.IsActive = false;

                    }
                    var campaignOrdersTotal = c.Orders.Sum (co => co.ProductSoldPrice);

                    c.TotalSales = c.Orders.Sum (co => co.ProductQuantity);
                    c.Turnover = (int) c.Orders.Sum (co => (co.ProductQuantity * co.ProductSoldPrice));

                    if (c.TotalSales != 0)
                        c.AverageItemPrice = c.Turnover / c.TotalSales;

                    _campaignRepository.Update (c);
                    _campaignRepository.Commit ();

                });
                serviceResult.Data = campaignList.FirstOrDefault ().CurrentDuration;
                serviceResult.ResultType = ServiceResultType.Success;

            } catch (System.Exception e) {
                _logger.LogError ("Error@IncreaseTime:", e);
                serviceResult.Message = e.ToString ();
                serviceResult.ResultType = ServiceResultType.Fail;
            }
            return serviceResult;
        }

        public ServiceResult<ProductViewModel> GetProduct (ProductViewModel model) {
            var serviceResult = new ServiceResult<ProductViewModel> ();

            try {
                var product = _productRepository.GetSingle (p => p.Code == model.Code);

                if (product == null) {
                    serviceResult.ResultType = ServiceResultType.Fail;
                    serviceResult.Message = "There is no product with this product code.";
                    return serviceResult;
                }

                var productViewModel = _mapper.Map<ProductViewModel> (product);

                serviceResult.Data = productViewModel;
                serviceResult.ResultType = ServiceResultType.Success;
            } catch (System.Exception e) {
                _logger.LogError (e, "Error@GetProduct:");
                serviceResult.Message = e.ToString ();
                serviceResult.ResultType = ServiceResultType.Fail;
            }

            return serviceResult;
        }

        public ServiceResult<OrderViewModel> CreateOrder (OrderViewModel model) {
            var result = new ServiceResult<OrderViewModel> ();

            try {
                var campaign = _campaignRepository
                    .AllIncludingAsQueryable (o => o.Orders, o => o.Product).FirstOrDefault (c => c.Product.Code == model.ProductCode);

                if (campaign.Product == null) {
                    result.ResultType = ServiceResultType.Fail;
                    result.Message = "There is no product with this product code.";
                    return result;
                }

                model.ProductId = campaign.Product.Id;
                model.ProductSoldPrice = campaign.Product.Price;

                var orderEntity = _mapper.Map<Order> (model);
                orderEntity.ProductId = campaign.ProductId;
                orderEntity.CampaignId = campaign.Id;

                _orderRepository.Add (orderEntity);
                _orderRepository.Commit ();

                var newProductStockCount = campaign.Product.Stock - model.ProductQuantity;
                if (newProductStockCount < 0) {
                    result.ResultType = ServiceResultType.Fail;
                    result.Message = "There are no enough products in stock";
                    return result;
                }

                campaign.Product.Stock = newProductStockCount;
                _productRepository.UpdateWithCommit (campaign.Product);

                result.ResultType = ServiceResultType.Success;
                result.Data = _mapper.Map<OrderViewModel> (orderEntity);;
                return result;

            } catch (Exception e) {
                _logger.LogError (e, "Error@CreateOrder:");
                result.Message = e.ToString ();
                result.ResultType = ServiceResultType.Fail;
                return result;
            }

        }
        public ServiceResult<CampaignViewModel> CreateCampaign (CampaignViewModel model) {
            var result = new ServiceResult<CampaignViewModel> ();

            try {
                var campaign = _campaignRepository.GetSingle (p => p.Name == model.Name);

                //Check campaign, is campaign exist?
                if (campaign != null) {
                    result.ResultType = ServiceResultType.Fail;
                    result.Message = "There is campaign with given campaign name.";
                    return result;
                }

                //Check product, is product exist?
                var product = _productRepository.GetSingle (p => p.Code == model.ProductCode);

                if (product == null) {
                    result.ResultType = ServiceResultType.Fail;
                    result.Message = "There is no product with this product code.";
                    return result;
                }

                //Check product stock
                if ((product.Stock - model.TargetSales) < 0) {
                    result.ResultType = ServiceResultType.Fail;
                    result.Message = "There are no enough products in stock for creating to campaign.";
                    return result;
                }

                var campaignEntity = _mapper.Map<Campaign> (model);
                campaignEntity.ProductId = product.Id;
                campaignEntity.CurrentProductPrice = product.Price;
                _campaignRepository.Add (campaignEntity);
                _campaignRepository.Commit ();

                result.ResultType = ServiceResultType.Success;
                result.Data = _mapper.Map<CampaignViewModel> (campaignEntity);
                //TODO:
                result.Data.ProductCode = product.Code;
                return result;

            } catch (Exception e) {
                _logger.LogError ("Error@CreateCampaign: " + e.ToString ());
                result.Message = e.ToString ();
                result.ResultType = ServiceResultType.Fail;
                return result;
            }

        }
    }
}