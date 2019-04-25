using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    class CarLimitException : Exception
    {
        public Auto exceptionalCar;
        new public string Message = "Car number limit has been reached. ";
        public CarLimitException(Auto car) => exceptionalCar = car;
    }
    class OverspeedException : Exception
    {
        public Auto exceptionalCar;
        new public string Message = "Car exceeded speed";
        public OverspeedException(Auto car) => exceptionalCar = car;
    }
    class UnderspeedException: Exception
    {
        public Auto exceptionalCar;
        new public string Message = "Car speed has dropped below the required.";
        public UnderspeedException(Auto car) => exceptionalCar = car;
    }

    class Auto
    {
        private string _name;
        public int id { get; set; }
        private int _number;
        private string _productionDate;
        private int _carSpeed = 0;
        private int _speedChangeRate = 1;
        public int maxSpeed = 666;
        public int minSpeed = 10;
        public Auto() { }
        public Auto(string name, int number = 0, int id = 0, string productionDate = "unknown", string manufacturer = "unknown")
        {
            _name = name;
            this.id = id;
            _number = number;
            _productionDate = productionDate;
        }
        public void Beep() => Console.WriteLine("B  E  E  E  E  E  E  E  E  E  P!");
        public void IncreaseSpeed(int speedToReach)
        {
            while (_carSpeed < speedToReach)
                _carSpeed += _speedChangeRate;
               
        }
        public void DecreaseSpeed(int speedToReach)
        {
            while (_carSpeed > speedToReach)
                _carSpeed -= _speedChangeRate;
                    
        }



        public void Drive()
        {
            if (_carSpeed > maxSpeed)
                throw new OverspeedException(this) ;
            if (_carSpeed < minSpeed)
                throw new UnderspeedException(this);

        }
        public void Brake()
        {
            DecreaseSpeed(0);
        }
    }

    class CarManufacturer
    {
        private string _name;
        private int _carNumberLimit;
        public CarManufacturer(string name, int carNumberLimit = 0)
        { _name = name; _carNumberLimit = carNumberLimit; }

        private int _carCounter = 0;

        public Auto CreateCar(string carName)
        {
            var car = new Auto() { };

            if (_carCounter < _carNumberLimit)
            {
                _carCounter++;
                car = new Auto(carName, _carCounter, manufacturer: _name);
                return car;
            }
            else
                car = new Auto(null, id: _carNumberLimit) { };
            {

                throw new CarLimitException(car);

            }

        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            var tazy = new CarManufacturer("Tazy", 3);
            var taz = tazy.CreateCar("Taz");
            taz.Beep();
            var AnotherTaz = tazy.CreateCar("AnotherTaz");

            var AnothaTaz = tazy.CreateCar("AnothaTaz");
            try
            {

                
                AnothaTaz.Beep();

                
                taz.IncreaseSpeed(660);
                taz.Drive();
                taz.IncreaseSpeed(670);
                taz.Drive();
                
            }
            catch (CarLimitException ex)
            { Console.WriteLine(ex.Message + $"Max car number was: {ex.exceptionalCar.id}"); }
            catch (OverspeedException ex)
            { Console.WriteLine(ex.Message + $" Car ID is {ex.exceptionalCar.id}. Car speed will be decreased to maximum");
                ex.exceptionalCar.DecreaseSpeed(ex.exceptionalCar.maxSpeed);

            }

            try {
                taz.DecreaseSpeed(2);
                taz.Drive();
            }
            catch (UnderspeedException ex)
            { Console.WriteLine(ex.Message + $" Car ID is {ex.exceptionalCar.id}. Car speed will be increased to minimum");
                ex.exceptionalCar.IncreaseSpeed(ex.exceptionalCar.minSpeed);
            }
            Console.ReadLine();
        }

    }
}
