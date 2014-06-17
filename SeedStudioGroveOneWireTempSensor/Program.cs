using System;
using System.Threading;
using ThreelnDotOrg.NETMF.Hardware;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;

namespace SeedStudio.Grove.OneWireTempSensor
{
    public class Program
    {
        private static OneWireBus.Device[] devs;

        public static void Main()
        {
            // Data line connected to D0 - but couldn be any digital pin
            OutputPort pin = new OutputPort(Pins.GPIO_PIN_D0, false);
            OneWire bus = new OneWire(pin);
            // Get list of devices (just in DS18B20 family)
            devs = OneWireBus.Scan(bus, OneWireBus.Family.DS18B20);
            // Create array to hold DS18B20 references
            DS18B20[] ds = new DS18B20[devs.Length];
            // Instantiate DS18B20 objects
            for (int i = 0; i < devs.Length; i++)
            {
                ds[i] = new DS18B20(bus, devs[i]);
            }
            while (true)
            {
                for (int i = 0; i < devs.Length; i++)
                {
                    float temp = ds[i].ConvertAndReadTemperature();
                    Debug.Print(i.ToString() + ": " + temp.ToString());
                }
                Thread.Sleep(1000);
                Debug.Print(" ");
            } 
        }
    }
} 