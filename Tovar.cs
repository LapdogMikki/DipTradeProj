//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DipTradeProj
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tovar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tovar()
        {
            this.Hist_Postavka = new HashSet<Hist_Postavka>();
            this.HistChangePrice = new HashSet<HistChangePrice>();
            this.History_Prod = new HashSet<History_Prod>();
            this.Postavka = new HashSet<Postavka>();
            this.Prodazh = new HashSet<Prodazh>();
        }
    
        public int id_tovar { get; set; }
        public string name { get; set; }
        public string ed_izm { get; set; }
        public double price { get; set; }
        public double kolvo_sklad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hist_Postavka> Hist_Postavka { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistChangePrice> HistChangePrice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History_Prod> History_Prod { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Postavka> Postavka { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prodazh> Prodazh { get; set; }
    }
}
