﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TradeBDEntities : DbContext
    {
        private static TradeBDEntities _context;


        public TradeBDEntities()
            : base("name=TradeBDEntities")
        {
        }
        public static TradeBDEntities GetContext()
        {
            if (_context == null)
                _context = new TradeBDEntities();
            return _context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Hist_Postavka> Hist_Postavka { get; set; }
        public virtual DbSet<HistChangePrice> HistChangePrice { get; set; }
        public virtual DbSet<History_Prod> History_Prod { get; set; }
        public virtual DbSet<Postavka> Postavka { get; set; }
        public virtual DbSet<Postavshik> Postavshik { get; set; }
        public virtual DbSet<Prodazh> Prodazh { get; set; }
        public virtual DbSet<Tovar> Tovar { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }
    }
}
