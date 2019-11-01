using System;
using System.Collections.Generic;
using NHibernate.Mapping;

namespace MSQL.Model.Mapping
{
    public class InventoryItemMap
    {
        public const string TableName = "inventoryitemdb";
        public static List<InventoryItemMap> list = new List<InventoryItemMap>();
        public virtual  int Id { get; set; }
        public virtual  int InventoryId { get; set; }
        public virtual int Level { get; set; }
        public virtual int Count { get; set; }
        public virtual int IsDressed { get; set; }
        public virtual int RoleId { get; set; }
        
    }
}