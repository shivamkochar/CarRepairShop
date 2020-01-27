using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShop
{
     public class VehicleList
    {
        private string make;
        private string model;
        private string color;
        private int numberOfDoors;
        private int selectedAccNumber;

        public string Make
        {
            get => make;
            set => make = value;
        }

        public string Model
        {
            get => model;
            set => model = value;
        }
        public string Color
        {
            get => color;
            set => color = value;
        }
        public int NumberOfDoors
        {
            get => numberOfDoors;
            set => numberOfDoors = value;
        }

        public int SelectedAccNumber
        {
            get => selectedAccNumber;
            set => selectedAccNumber = value;
        }
    }
}
