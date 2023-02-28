using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        const string Create = "1";
        const string Sell = "2";
        const string From = "3";
        const string Send = "4";
        const string Info = "5";
        const string Exit = "6";

        bool isWork = true;

        TrainPlan plan = new TrainPlan();

        Console.WriteLine("Добро пожаловать! Что желаете сделать?");

        while (isWork)
        {
            Console.WriteLine($"{Create} - Создать направление\n{Sell} - Продать билеты" +
                $"\n{From} - Количество вагонов\n{Send} - Отправка\n{Info} - План поездов\n{Exit} - Выход");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case Create:
                    plan.CreateDirection();
                    break;

                case Sell:
                    plan.SellTickets();
                    break;

                case From:
                    plan.FormTrain();
                    break;

                case Send:
                    plan.SendTrain();
                    break;

                case Info:
                    plan.PrintCurrentInfo();
                    break;

                case Exit:
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Ошибка! Нет такой команды :(");
                    break;
            }
        }
    }
}

class TrainPlan
{
    private int minimumPassengerCount = 50;
    private int maximumPassengerCount = 250;
    private int minimumTrainCarCapacity = 50;
    private int maximumTrainCarCapacity = 100;
    private int _numbersPassengers;

    private string _destination;

    private List<int> _train = new List<int>();
    private Random _random = new Random();

    public void CreateDirection()
    {
        Console.Write("Укажите пункт назначения: ");
        _destination = Console.ReadLine();
    }

    public void SellTickets()
    {
        _numbersPassengers = _random.Next(minimumPassengerCount, maximumPassengerCount);
        Console.WriteLine($"Количество пассажиров: {_numbersPassengers}");
    }

    public void CreatePlaces(out int trainCarCapacity)
    {
        trainCarCapacity = _random.Next(minimumTrainCarCapacity, maximumTrainCarCapacity);
    }

    public void FormTrain()
    {
        CreatePlaces(out int trainCarCapacity);

        int countTrainCars = (int)Math.Ceiling((double)_numbersPassengers / trainCarCapacity);

        for (int i = 0; i < countTrainCars; i++)
        {
            _train.Add(0);
        }

        Console.WriteLine($"Количество вагонов состава: {countTrainCars}\n Вместимость одного вагона: {trainCarCapacity}");
    }

    public void SendTrain()
    {
        if (_destination != null && _numbersPassengers != 0)
        {
            Console.WriteLine($"Поезд отправлен в направлении: {_destination}. Количество пассажиров: {_numbersPassengers}");
            _destination = null;
            _numbersPassengers = 0;
            _train.Clear();
        }
        else
        {
            Console.WriteLine("Текущий план поездов отсутствует.");
        }
    }

    public void PrintCurrentInfo()
    {
        if (_destination != null && _numbersPassengers != 0)
        {
            Console.WriteLine($"Текущее место назначения: {_destination}");
            Console.WriteLine($"Текущее количество пассажиров: {_numbersPassengers}");
            Console.WriteLine($"Текущее количество вагонов состава: {_train.Count}");
        }
        else
        {
            Console.WriteLine("Текущий план поездов отсутствует.");
        }
    }
}