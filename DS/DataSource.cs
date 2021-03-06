﻿using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DataSource
    {
        private static List<Tester> testersList = new List<Tester>();
        private static List<Trainee> traineesList = new List<Trainee>();
        private static List<DrivingTest> drivingtestsList = new List<DrivingTest>();

        public static void init()
        {
            TestersList.Add(new Tester
            {
                ID = "123456789",
                Name = new Name { FirstName = "jojo", LastName = "chalass" },
                Address = new Address
                {
                    City = "Jerusalem",
                    Number = 21,
                    StreetName = "havvad haleumi",
                    //                  ZipCode = 91160
                },
                DayOfBirth = DateTime.Now.AddYears(-45),
                Gender = Gender.MALE,
                Experience = 10,
                Expertise = CarType.Truck_Heavy,
                MaxDistance = 2,
                Schedule = new Schedule
                {
                    Data = new bool[5][]
                     {
                      new bool[6] { false, false, true, false, false, false},
                      new bool[6] { false, false, false, false, false, false},
                      new bool[6] { false, false, false, false, false, false},
                      new bool[6] { false, false, true, false, false, false},
                      new bool[6] { false, false, false, false, false, false}
                      }
                }
            });
            TestersList.Add(new Tester
            { 
            ID = "123456788",
                Name = new Name { FirstName = "Aviad", LastName = "Feig" },
                Address = new Address
                {
                    City = "Jerusalem",
                    Number = 21,
                    StreetName = "uziel",
                    //                  ZipCode = 91160
                },
                DayOfBirth = DateTime.Now.AddYears(-54),
                Gender = Gender.MALE,
                Experience = 10,
                Expertise = CarType.Truck_Heavy,
                MaxDistance = 2,
                Schedule = new Schedule
                {
                    Data = new bool[5][]
                     {
                      new bool[6] { false, false, true, false, false, false},
                      new bool[6] { false, false, true, false, false, false},
                      new bool[6] { false, false, true, false, false, false},
                      new bool[6] { false, false, true, false, false, false},
                      new bool[6] { false, false, false, false, false, false}
                      }
                }
            });

            TraineesList.Add(new Trainee
            {
                ID = "987654321",
                Name = new Name { FirstName = "eran", LastName = "zehuze" },
                Address = new Address
                {
                    City = "TLv",
                    Number = 21,
                    StreetName = "Jerusalem Bld",
                    //                  ZipCode = 91160
                },
                DayOfBirth = DateTime.Now.AddYears(-21),
                Gender = Gender.MALE,
                CarTrained = CarType.Truck_Heavy,
                DrivingSchool = SchoolName.MAOF,
                GearType = GearType.Manual,
                LessonsNb = 134
            }
            );
            TraineesList.Add(new Trainee
            {
                ID = "987654322",
                Name = new Name { FirstName = "Emanuel", LastName = "Macron" },
                Address = new Address
                {
                    City = "Haifa",
                    Number = 100,
                    StreetName = "Hell Av.",
                    //                  ZipCode = 91160
                },
                DayOfBirth = DateTime.Now.AddYears(-24),
                Gender = Gender.MALE,
                CarTrained = CarType.Private,
                DrivingSchool = SchoolName.OR_YAROK,
                GearType = GearType.Manual,
                LessonsNb = 12
            }
            );
        }
        public static List<DrivingTest> DrivingtestsList
        {
            get { return drivingtestsList; }
            set { drivingtestsList = value; }
        }

        public static List<Tester> TestersList
        {
            get { return testersList; }
            set { testersList = value; }
        }

        public static List<Trainee> TraineesList
        {
            get { return traineesList; }
            set { traineesList = value; }
        }
    }
}
