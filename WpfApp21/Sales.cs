//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp21
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sales
    {
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int PartnerID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
    
        public virtual Partners Partners { get; set; }
        public virtual Products Products { get; set; }
    }
}
