namespace HepsiburadaCase.Models {
    public class CampaignViewModel {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int Duration { get; set; }
        public double PriceManipulationLimit { get; set; }
        public double CurrentDuration { get; set; }
        public double CurrentProductPrice { get; set; }
        public int TargetSales { get; set; }
        public int TotalSales { get; set; }
        public int Turnover { get; set; }
        public int AverageItemPrice { get; set; }
        public string ProductCode { get; set; }
        public bool IsActive { get; set; }

        // public ProductViewModel Product { get; set; }

    }
}