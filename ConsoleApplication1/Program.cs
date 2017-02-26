using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static int Main(string[] args)
        {
            Computer myPrototype = new Computer("c1", true, 2000, 2000, new int?[5]);
          

            Computer[] computers = new Computer[10];
            int choice;
            int placeHolder = 0;
            Computer userPrototype = null;
            do
            {
                choice = menu();
                switch (choice)
                {
                    case 1:
                        //Computer comp;
                        char ans;
                        for (int i = placeHolder; i < computers.Length; i++)
                        {
                            try
                            {
                                computers[i] = createComputer();
                                placeHolder = i + 1;
                                Console.WriteLine("Another Computer? (y/n): ");
                                ans = Console.ReadKey().KeyChar;
                                if (ans == 'n' || ans == 'N')
                                {
                                    break;
                                }
                            }catch(InvalidOperationException ex)
                            {
                                Console.Write("RAM or Storage Capacity was too low.");
                            }

                        }
                        break;
                    case 2:
                        try
                        {
                            userPrototype = createComputer();
                        }catch(InvalidOperationException ex)
                        {
                            Console.Write("RAM or storage Capacity was too low.");
                        }
                        break;
                    case 3:
                        getSummaryOfComputer(computers, myPrototype);
                        break;
                    case 4:
                        getSummaryOfAllComputers(computers, myPrototype);
                        break;
                    case 5:
                        Console.WriteLine("Starting Index: ");
                        int stIndex;
                        Int32.TryParse( Console.ReadLine() , out stIndex );
                        Console.WriteLine("Ending Index: ");
                        int endIndex;
                        Int32.TryParse(Console.ReadLine(), out endIndex);
                        getSummaryOfAllComputersRange(computers, stIndex, endIndex, userPrototype, myPrototype);
                        break;
                    case 6:
                        break;
                 
                }
            } while (choice != 6);
            return 1;
        }



        public static int menu()
        {
            StringBuilder info = new StringBuilder();

            info.Append("\n1. Add computers");
            info.Append("\n2. Specify prototype");
            info.Append("\n3. Get summary of a computer");
            info.Append("\n4. Get summary of all computers");
            info.Append("\n5. Get summary of computers from x to y");
            info.Append("\n6. Exit application");
            int choice;
           do
            {
                Console.WriteLine(info.ToString());
                Int32.TryParse(Console.ReadLine(), out choice);
            }
            while (choice < 1 || choice > 6);

            return choice;
        }

        public static Computer createComputer()
        {
            Console.WriteLine("ID: ");
            string id = Console.ReadLine();

            Console.WriteLine("Antenna (y/n) (n/a if not applicable): ");
            char antenna = Console.ReadKey().KeyChar;
            Boolean? hasAntenna = null;

            if (antenna == 'y' || antenna == 'Y')
            {
                hasAntenna = true;
            }
            else if (antenna == 'n' || antenna == 'N')
            {
                hasAntenna = false;
            }
            Console.ReadLine();
            Console.WriteLine("Hard Drive Capacity: (-1 if not applicable)");
            int input;
            Int32.TryParse(Console.ReadLine(), out input);
            int? hdCapacity;
            if (input == -1)
            {
                hdCapacity = null;
            }
            else
            {
                hdCapacity = input;
            }

            Console.WriteLine("RAM: ");
            int ram;
            Int32.TryParse( Console.ReadLine(), out ram);

            Console.WriteLine("Equipped For Extra Software? (y/n)");
            char ans = Console.ReadKey().KeyChar;
            Console.ReadLine();
            int?[] softwareLicenses = null;
          
            if (ans == 'y' || ans == 'Y')
            {
                softwareLicenses = new int?[5];
        
                for (int i = 0; i < softwareLicenses.Length; i++)
                {
                    Console.WriteLine("Number Licenses for Software " + (i + 1) + "(-1 if software not installed): ");
                    Int32.TryParse( Console.ReadLine(), out input);
                    if (input == -1)
                    {
                        softwareLicenses[i] = null;
                    }
                    else
                    {
                        softwareLicenses[i] = input;
                    }
                }
            }

            Computer comp = new Computer(id, hasAntenna, hdCapacity, ram, softwareLicenses);
            return comp;


        }
        private static void getSummaryOfComputer(Computer[] computers, Computer myPrototype)
        {
            Console.WriteLine("Which computer index number would you like to view?");
            int choice;
            Int32.TryParse( Console.ReadLine(), out choice);

            Console.WriteLine(computers[choice]??myPrototype);
        }

        private static void getSummaryOfAllComputersRange(Computer[] computers, int startingIndex, int endingIndex, Computer userProtoype, Computer myPrototype)
        {
            int totalRAM = 0;
            int numWithAntenna = 0;
            int numCompsAntennaApplicable = 0;
            double totalStorage = 0;
            int numCompsWithHardDriveCapacity = 0;
            int totalSoftwareLicenses = 0;
            int machinesCapableOfSoftware = 0;

            //create array and loop through to add and calculate
            int[] totalNumLicensesEachSoftware = new int[myPrototype.NumLicenses.Length];
            int[] machinesInstalledEachSoftware = new int[myPrototype.NumLicenses.Length];
            /*int softwareInstalled = 0;
            int numLicensesSoftware0 = 0;
            int numInstalledSoftware1 =0;
            int numLicensesSoftware1 = 0;
            int numInstalledSoftware2 = 0;
            int numLicensesSoftware2 = 0;
            int numInstalledSoftware3 = 0;
            int numLicensesSoftware3 = 0;
            int numInstalledSoftware4 = 0;
            int numLicensesSoftware4 = 0;*/


            for (int i = startingIndex; i <= endingIndex; i++)
            {
                Computer c;
                c = computers[i] ?? userProtoype ?? myPrototype;

                //add RAM
                totalRAM += c.RAM;


                if (c.HasAntenna.HasValue)
                {
                    numCompsAntennaApplicable++;
                    //better way to do this? Already confirmed its not null
                    if (c.HasAntenna ?? false)
                    {
                        numWithAntenna++;
                    }
                }
                /*
                //add antenna
                if (c.HasAntenna ?? false) 
                {
                    numWithAntenna++;
                    numCompsAntennaApplicable++;
                }
                //add all computers able to have antenna
                else if (!c.HasAntenna ?? false)
                {
                    numCompsAntennaApplicable++;
                }*/

                if (c.StorageCapacity.HasValue)
                {
                    numCompsWithHardDriveCapacity++;
                    totalStorage += c.StorageCapacity ?? 0;
                }

                if (c.NumLicenses != null)
                {
                    machinesCapableOfSoftware++;

                    for (int j = 0; j < c.NumLicenses.Length; j++)
                    {
                        if (c.NumLicenses[j].HasValue)
                        {
                            totalNumLicensesEachSoftware[j]++;
                            machinesInstalledEachSoftware[j] += c.NumLicenses[j] ?? 0;
                            totalSoftwareLicenses += c.NumLicenses[j] ?? 0;

                        }

                    }
                    /* if (c.NumLicenses[1].HasValue)
                     {
                         numInstalledSoftware1++;
                         numLicensesSoftware1 += c.NumLicenses[1] ?? 0;
                     }
                     if (c.NumLicenses[2].HasValue)
                     {
                         numInstalledSoftware2++;
                         numLicensesSoftware2 += c.NumLicenses[2] ?? 0;
                     }
                     if (c.NumLicenses[3].HasValue)
                     {
                         numInstalledSoftware3++;
                         numLicensesSoftware3 += c.NumLicenses[3] ?? 0;
                     }
                     if (c.NumLicenses[4].HasValue)
                     {
                         numInstalledSoftware4++;
                         numLicensesSoftware4 += c.NumLicenses[4] ?? 0;
                     }*/

                }

                double avgRAM = totalRAM / (endingIndex-startingIndex+1);

                double? percentWithAntenna;
                if (numCompsAntennaApplicable > 0)
                {
                    percentWithAntenna = numWithAntenna * 100 / numCompsAntennaApplicable;
                }
                else
                {
                    percentWithAntenna = null;
                }
                double? avgHardDriveCapacity;
                if (numCompsWithHardDriveCapacity > 0)
                {
                    avgHardDriveCapacity = totalStorage / numCompsWithHardDriveCapacity;
                }
                else
                {
                    avgHardDriveCapacity = null;
                }
                double? avgTotalSoftwareLicenses;
                if (machinesCapableOfSoftware > 0)
                {
                    avgTotalSoftwareLicenses = totalSoftwareLicenses / machinesCapableOfSoftware;
                }
                else
                {
                    avgTotalSoftwareLicenses = null;
                }

                StringBuilder info = new StringBuilder();

                info.Append("\nAverage RAM: ");
                info.Append(avgRAM);
                info.Append("\nPercent Machines With Antenna: ");
                info.Append(percentWithAntenna.ToString() ?? "Not Applicable");
                info.Append("\nAverage Hard Drive Capacity: ");
                info.Append(avgHardDriveCapacity.ToString() ?? "Not applicable");
                info.Append("\nAverage Total Software Licenses: ");
                info.Append(avgTotalSoftwareLicenses.ToString() ?? "Not applicable");

                for (i = 0; i < totalNumLicensesEachSoftware.Length; i++)
                {
                    info.Append("\n\tAverage Licenses for software " + (i + 1) + ": ");
                    if (machinesInstalledEachSoftware[i] > 0)
                    {
                        info.Append(totalNumLicensesEachSoftware[i] / machinesInstalledEachSoftware[i]);
                    }
                    else
                    {
                        info.Append("Not Applicable");
                    }
                }

                Console.WriteLine(info.ToString());
            }
        }

        private static void getSummaryOfAllComputers(Computer[] computers, Computer myPrototype)
        {
            int totalRAM = 0;
            int numWithAntenna = 0;
            int numCompsAntennaApplicable = 0;
            double totalStorage = 0;
            int numCompsWithHardDriveCapacity = 0;
            int totalSoftwareLicenses = 0;
            int machinesCapableOfSoftware = 0;

            //create array and loop through to add and calculate
            int[] totalNumLicensesEachSoftware = new int[myPrototype.NumLicenses.Length];
            int[] machinesInstalledEachSoftware = new int[myPrototype.NumLicenses.Length];
            /*int softwareInstalled = 0;
            int numLicensesSoftware0 = 0;
            int numInstalledSoftware1 =0;
            int numLicensesSoftware1 = 0;
            int numInstalledSoftware2 = 0;
            int numLicensesSoftware2 = 0;
            int numInstalledSoftware3 = 0;
            int numLicensesSoftware3 = 0;
            int numInstalledSoftware4 = 0;
            int numLicensesSoftware4 = 0;*/


            for (int i = 0; i <= computers.Length && computers[i]!=null; i++)
            {
                Computer c = computers[i];

                //add RAM
                totalRAM += c.RAM;


                if (c.HasAntenna.HasValue)
                {
                    numCompsAntennaApplicable++;
                    //better way to do this? Already confirmed its not null
                    if (c.HasAntenna ?? false)
                    {
                        numWithAntenna++;
                    }
                }
               
                if (c.StorageCapacity.HasValue)
                {
                    numCompsWithHardDriveCapacity++;
                    totalStorage += c.StorageCapacity ?? 0;
                }

                if (c.NumLicenses != null)
                {
                    machinesCapableOfSoftware++;

                    for (int j = 0; j < c.NumLicenses.Length; j++)
                    {
                        if (c.NumLicenses[j].HasValue)
                        {
                            totalNumLicensesEachSoftware[j] += c.NumLicenses[j] ?? 0;
                            machinesInstalledEachSoftware[j] ++;
                            totalSoftwareLicenses += c.NumLicenses[j] ?? 0;

                        }

                    }
               
                }

                double avgRAM = totalRAM / (i+1);

                double? percentWithAntenna;
                if (numCompsAntennaApplicable > 0)
                {
                    percentWithAntenna = numWithAntenna * 100 / numCompsAntennaApplicable;
                }
                else
                {
                    percentWithAntenna = null;
                }
                double? avgHardDriveCapacity;
                if (numCompsWithHardDriveCapacity > 0)
                {
                    avgHardDriveCapacity = totalStorage / numCompsWithHardDriveCapacity;
                }else
                {
                    avgHardDriveCapacity = null;
                }
                double? avgTotalSoftwareLicenses;
                    if (machinesCapableOfSoftware > 0) {
                    avgTotalSoftwareLicenses = totalSoftwareLicenses / machinesCapableOfSoftware;
                }else
                {
                    avgTotalSoftwareLicenses = null;
                }

                StringBuilder info = new StringBuilder();

                info.Append("\nAverage RAM: ");
                info.Append(avgRAM);
                info.Append("\nPercent Machines With Antenna: ");
                info.Append(percentWithAntenna.ToString() ?? "Not Applicable");
                info.Append("\nAverage Hard Drive Capacity: ");
                info.Append(avgHardDriveCapacity.ToString() ?? "Not applicable");
                info.Append("\nAverage Total Software Licenses: ");
                info.Append(avgTotalSoftwareLicenses.ToString() ?? "Not applicable");
               
                for (i = 0; i < totalNumLicensesEachSoftware.Length; i++)
                {
                    info.Append("\n\tAverage Licenses for software " + (i + 1) + ": ");
                    if (machinesInstalledEachSoftware[i] > 0)
                    {
                        info.Append(totalNumLicensesEachSoftware[i] / machinesInstalledEachSoftware[i]);
                    }
                    else
                    {
                        info.Append("Not Applicable");
                    }
                }


               
                Console.WriteLine(info.ToString());
            }

        }
    }
}
