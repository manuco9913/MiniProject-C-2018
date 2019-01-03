using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace BE
{
    public abstract class Person
    {
        public String ID { get; set; }
        public Name Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}