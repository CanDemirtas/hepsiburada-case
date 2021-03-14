using System.Collections.Generic;

namespace HepsiburadaCase.Data.Entity {
    public class Product : BaseEntity {

        public string Code { get; set; }
        public double Price { get; set; }
        public long Stock { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}