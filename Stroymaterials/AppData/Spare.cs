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
    
    public partial class Spare
    {
        public int id_spare { get; set; }
        public string spare_name { get; set; }
        public int spare_count { get; set; }
        public int spare_category { get; set; }
        public int spare_price { get; set; }
        public int spare_provider { get; set; }
        public int spare_maker { get; set; }
        public string spare_description { get; set; }
        public int spare_available { get; set; }
        public string spare_photo { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Maker Maker { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
