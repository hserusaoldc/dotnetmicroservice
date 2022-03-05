using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetmicroservice
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public EnumTypes? Type { get; set; }
        public string Description { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? ManufacturedOn { get; set; }
        public long? Mileage { get; set; }

        public enum EnumTypes
        {
            Land, Ait, Water
        }
    }
}
