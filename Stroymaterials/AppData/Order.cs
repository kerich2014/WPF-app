//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stroymaterials.AppData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int id_order { get; set; }
        public int order_pointup { get; set; }
        public int order_user { get; set; }
        public int order_check { get; set; }
    
        public virtual Check Check { get; set; }
        public virtual Pointup Pointup { get; set; }
        public virtual User User { get; set; }
    }
}