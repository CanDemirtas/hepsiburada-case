using System.Collections.Generic;

namespace HepsiburadaCase.Data.Entity {
    public class Campaign : BaseEntity {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int Duration { get; set; }
        public int CurrentDuration { get; set; }
        public double PriceManipulationLimit { get; set; }
        public int TargetSales { get; set; }
        public int TotalSales { get; set; }
        public int Turnover { get; set; }
        public double CurrentProductPrice { get; set; }
        public int AverageItemPrice { get; set; }
        public Product Product { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}