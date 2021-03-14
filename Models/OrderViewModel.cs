namespace HepsiburadaCase.Models {
    public class OrderViewModel {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double ProductSoldPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductCode { get; set; }

    }
}