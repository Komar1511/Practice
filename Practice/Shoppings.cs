//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practice
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shoppings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shoppings()
        {
            this.Pavilions = new HashSet<Pavilions>();
        }
    
        public int idShopping { get; set; }
        public string nameShopping { get; set; }
        public string statusShopping { get; set; }
        public Nullable<int> countPavilions { get; set; }
        public string city { get; set; }
        public Nullable<decimal> priceShopping { get; set; }
        public Nullable<double> coefficientShopping { get; set; }
        public Nullable<int> countFloor { get; set; }
        public byte[] image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pavilions> Pavilions { get; set; }
    }
}
