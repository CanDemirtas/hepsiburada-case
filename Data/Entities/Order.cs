namespace HepsiburadaCase.Data.Entities {
    public class Order : BaseEntity {

        public int ProductId { get; set; }
        public string ProductCode { get; set; }

        public double ItemPrice { get; set; }
        
        public int ProductQuantity { get; set; }

        public Product Product { get; set; }

    }
}