using System;

namespace ConsoleAppDay9_3
{
    public delegate void TempDel(int t);

    public class Heater
    {
        private int _heaterTemp;
        public int HeaterTemp
        {
            set { _heaterTemp = value; }
            get { return _heaterTemp; }
        }

        public void TemperatureChange(int t)
        {
            if (t < _heaterTemp)
            {
                Console.WriteLine("Heater ON");
            }
            else
            {
                Console.WriteLine("Heater OFF");
            }
        }
    }

    public class Cooler
    {
        private int _coolerTemp;
        public int CoolerTemp
        {
            set { _coolerTemp = value; }
            get { return _coolerTemp; }
        }

        public void TemperatureChange(int t)
        {
            if (t > _coolerTemp)
            {
                Console.WriteLine("Cooler ON");
            }
            else
            {
                Console.WriteLine("Cooler OFF");
            }
        }
    }

    public class Thermostat
    {
        public event TempDel OnTemperatureChange;
        private int _temp;

        public int CurrentTemp
        {
            set
            {
                if (_temp != value)
                {
                    _temp = value;
                    OnTemperatureChange?.Invoke(_temp);
                }
            }
            get { return _temp; }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Thermostat t = new Thermostat();
            Cooler c = new Cooler();
            Heater h = new Heater();

            c.CoolerTemp = 80;
            h.HeaterTemp = 30;

            t.OnTemperatureChange += c.TemperatureChange;
            t.OnTemperatureChange += h.TemperatureChange;

            t.CurrentTemp = 40;
        }
    }
}
