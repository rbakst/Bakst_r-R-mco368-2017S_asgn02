using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Computer
    {
        readonly string id;
        public string ID { get { return id; } }

        Boolean? hasAntenna;
        public Boolean? HasAntenna
        {
            get { return hasAntenna; }
            set { hasAntenna = value; }
        }

        double? storageCapacity;
        public double? StorageCapacity
        {
            get { return storageCapacity; }
            set
            {
                if (value >= 0 || value == null)
                {
                    storageCapacity = value;
                }
                else
                {
                    throw new InvalidOperationException();

                }
            }

           
        }

        int?[] numLicenses;
        public int?[] NumLicenses
        {
            //how return deep copy?
            get{ return numLicenses; }
        }
        int ram;

        public int RAM
        {
            get
            {
                int amountRemoved;
                if(this.hasAntenna ?? false)
                {
                    amountRemoved = 100;
                }
                else
                {
                    amountRemoved = 50;
                }

                int counter=0;
                foreach(int? i in numLicenses)
                {
                    if (i != null)
                    {
                        counter++;
                    }

                }
                amountRemoved += counter;

                return this.ram - amountRemoved;
            }
            set
            {
                if (value >= 1000)
                {
                    this.ram = value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public Computer (string id, Boolean? hasAntenna, double? storageCapacity, int ram, int?[] numLicenses)
        {
            this.id = id;
            HasAntenna = hasAntenna;
            StorageCapacity = storageCapacity;
            this.numLicenses = numLicenses;
            RAM = ram;
        }
        override
        public string ToString()
        {
            StringBuilder info = new StringBuilder();

            info.Append("\nComputer ID: ");
            info.Append(id);
            info.Append("\nAntenna: ");
            info.Append(hasAntenna.ToString() ?? "not applicable");
            info.Append("\nStorage Capacity: ");
            info.Append(storageCapacity.ToString() ?? "Doesn't support hard drive");
            info.Append("\nNumber of Licenses:");
            for(int i = 0; i < numLicenses.Length; i++)
            {
                info.Append("\n\tSoftware " + (i+1) + ": ");
                info.Append(numLicenses[i].ToString() ?? "Not Installed");
            }
            info.Append("\nRAM: ");
            info.Append(RAM);

            return info.ToString();

        }
    }
}
