using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiburadaCase.Data.Entities {

    public class BaseEntity {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual int Id { get; set; }

        [Column]
        public virtual bool IsDeleted { get; set; } = false;
        [Column]
        public virtual bool IsActive { get; set; } = true;
        [Column]
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}