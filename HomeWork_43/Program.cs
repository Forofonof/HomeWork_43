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

        TrainPlan plan = new TrainPlan();

        Console.WriteLine("Добро пожаловать! Что желаете сделать?");

        while (true)
        {
            Console.WriteLine($"{Create} - Создать направление\n{Sell} - Продать билеты\n{From} - Количество вагонов\n{Send} - Отправка\n{Info} - План поездов");
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
                default:
                    Console.WriteLine("Ошибка! Нет такой команды :(");
                    break;
            }
        }
    }
}

class TrainPlan
{
    private string _destination;

    private int _numbersPassengers;

    private int _trainCarCapacity;

    private List<int> _train = new List<int>();

    private Random rand = new Random();

    public void CreateDirection()
    {
        Console.Write("Укажите пункт назначения: ");
        _destination = Console.ReadLine();
    }

    public void SellTickets()
    {
        _numbersPassengers = rand.Next(50, 250);
        Console.WriteLine($"Количество пассажиров: {_numbersPassengers}");
    }

    public void CreateTrain()
    {
        _trainCarCapacity = rand.Next(50, 100);
    }

    public void FormTrain()
    {
        CreateTrain();

        int numbersTrainCars = (int)Math.Ceiling((double)_numbersPassengers / _trainCarCapacity);

        for (int i = 0; i < numbersTrainCars; i++)
        {
            _train.Add(50);
        }
        Console.WriteLine($"Количество вагонов состава: {numbersTrainCars}\n Вместимость одного вагона: {_trainCarCapacity}");
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