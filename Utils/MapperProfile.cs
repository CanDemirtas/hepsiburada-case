using AutoMapper;
using HepsiburadaCase.Data.Entity;
using HepsiburadaCase.Models;

namespace HepsiburadaCase.Utils {
    public class MapperProfile : Profile {
        public MapperProfile () {
            CreateMap<Campaign, CampaignViewModel> ();
            CreateMap<CampaignViewModel, Campaign> ();

            CreateMap<Product, ProductViewModel> ();
            CreateMap<ProductViewModel, Product> ();

            CreateMap<Order, OrderViewModel> ();
            CreateMap<OrderViewModel, Order> ();

        }
    }
}