using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
namespace BE
{
    public class Address : IComparable<Address>
    {
        public String StreetName { get; set; }
        public int Number { get; set; }
        public String City { get; set; }

        public int CompareTo(Address other)
        {
            int r = City.CompareTo(other.City);
            if (r == 0)
            {
                r = StreetName.CompareTo(other.StreetName);
            }
            if (r == 0)
            {
                r = Number.CompareTo(other.Number);
            }
            return r;
        }

        //public int ZipCode { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
