/*
 2.
Клас "Теплиця" (Hothouse) має властивість "Температура" а також знає мінімальну та максимально припустимі температури.
Коли температура стає більшою за максимальну клас Теплиця виводить повідомлення та ініціює подію "надто гаряче" (TooHot)
Коли температура стає меншою за мінімальну клас Теплиця виводить повідомлення та ініціює подію "надто холодно" (TooCold)
Коли температура повертається у межі норми клас Теплиця виводить повідомлення та ініціює подію "Все добре" (Well)
Події теплиці несуть у собі посилання на об'єкт теплиці (delegate void HotHouseDeleg(HotHouse house))
delegate void HotHouseDeleg(HotHouse house);
class HotHouse
{
	public event HotHouseDeleg TooHot;
	public event HotHouseDeleg TooCold;
.......
}
class Heater
{
	public void Warm(HotHouse h)
	{.......
	h.Temperature +=....
	}
}

class  Cooler
{
	public void Cool(HotHouse h)
	{.......}
}
Клас "Нагрівач" (Heater) має  метод гріти (Warm) котрий збільшує температуру у теплиці на 5 градусів.   Warm(HotHouse house)
Клас "Охолоджувач" (Cooler) має  метод охолоджувати (Cool) котрий зменшує температуру у теплиці на 5 градусів.
Ці методи виводять повідомлення у консоль про те хто і що робить.



Main() створює екземпляр теплиці та по екземпляру нагрівача та охолоджувача і підписує їх на події теплиці, так щоби вони вмикалися і 
вимикалися у відповідь на події теплиці.

Сама Main() імітує вплив погоди на теплицю, збільшуючи чи зменшуючи температуру в ній на випадкове значення від -2 до +2 градусів.
Це все відбувається у циклі, щоразу очікуючи натискання пропуску (чи довільного символу, Console.ReadKey()).
У консолі маємо бачити як змінюється температура у теплиці, як вмикаються та вимикаються пристрої.

 */

using System;


namespace _02_hothouse
{
    delegate void HotHouseDeleg(HotHouse house);

    class HotHouse
    {
        private int temperature;
        private const int MinTmpr = 17;
        private const int MaxTmpr = 40;

        public HotHouse(int tmpr = 0)
        {
            Temperature = tmpr;
        }

        public int Temperature
        {
            get => temperature;
            set
            {
                temperature = value;

                if (temperature > MinTmpr && temperature < MaxTmpr)
                {
                    ShowMessage($"Well. Temperature is {temperature}C");
                    Well?.Invoke(this);
                }
                else
                if (temperature <= MinTmpr)
                {
                    ShowMessage($"Too cold. Temperature is {temperature}C", 4);
                    TooCold?.Invoke(this);
                }
                else
                if (temperature >= MaxTmpr)
                {
                    ShowMessage($"Too hot. Temperature is {temperature}C", 3);
                    TooHot?.Invoke(this);
                }

               
            }
        }

        public event HotHouseDeleg TooHot;
        public event HotHouseDeleg TooCold;
        public event HotHouseDeleg Well;

        // колір тексту - червоний, синій і сірий
        private void SetColor(int color = 0)
        {
            switch (color)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }

        // виводити меседж в певному кольорі
        private void ShowMessage(string message, int color = 0)
        {
            SetColor(color);
            Console.WriteLine(message);
            SetColor(0);
        }

    }

    // Клас "Нагрівач" (Heater) має  метод гріти(Warm) котрий збільшує температуру у теплиці на 5 градусів.Warm(HotHouse house)
    class Heater
    {
        public void Warm(HotHouse h)
        {
            Console.WriteLine("Heater warm temperature to {0}C", h.Temperature + 5);
            h.Temperature += 5;
        }
    
    }

    // Клас "Охолоджувач" (Cooler) має  метод охолоджувати (Cool) котрий зменшує температуру у теплиці на 5 градусів.
    class Cooler
    {
        public void Cool(HotHouse h)
        {
            Console.WriteLine("Cooler cool temperature to {0}C", h.Temperature - 5);
            h.Temperature -= 5;
        }
    }

 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\tFor next step press Space\n\tFor Exit press any key\n");

            HotHouse hhouse = new HotHouse(22);
            Heater ht = new Heater();
            Cooler cl = new Cooler();

            hhouse.TooHot += cl.Cool;
            hhouse.TooCold += ht.Warm;

            Random rand = new Random();
            
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {

                    if (rand.Next(0, 2) == 0)
                        hhouse.Temperature += 2;
                    else
                        hhouse.Temperature -= 2;
                }
                else
                    break;
            }
        }
    }
}
