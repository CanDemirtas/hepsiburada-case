namespace HepsiburadaCase.Data.Entity {
    public class Order : BaseEntity {

        public int ProductId { get; set; }

        public double ProductSoldPrice { get; set; }

        public int ProductQuantity { get; set; }

        public Product Product { get; set; }

        public int CampaignId { get; set; }

        public Campaign Campaign { get; set; }

    }
}