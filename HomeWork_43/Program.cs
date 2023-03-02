using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Station station = new Station();
        station.Work();
    }
}

class Station
{
    private int _numbersPassengers;
    private string _endpoint;
    private Random _random = new Random();
    private List<int> _train = new List<int>();

    public void Work()
    {
        const string Create = "1";
        const string Sell = "2";
        const string Send = "3";
        const string Info = "4";
        const string Exit = "5";

        Console.WriteLine("Добро пожаловать на Московский вокзал! Что желаете сделать?");

        while (true)
        {
            Console.WriteLine($"{Create} - Создать направление\n{Sell} - Продать билеты\n{Send} - Отправка\n{Info} - План поездов\n{Exit} - Выход");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case Create:
                    CreateDirection();
                    break;

                case Sell:
                    SellTickets();
                    break;

                case Send:
                    SendTrain();
                    break;

                case Info:
                    PrintCurrentInfo();
                    break;

                case Exit:
                    return;

                default:
                    Console.WriteLine("Ошибка! Нет такой команды :(");
                    break;
            }
        }
    }

    private void CreateDirection()
    {
        Console.Write("Укажите пункт назначения: ");
        _endpoint = Console.ReadLine();
    }

    private void SellTickets()
    {
        if (_endpoint != null)
        {
            int minimumPassengerCount = 1500;
            int maximumPassengerCount = 2000;

            _numbersPassengers = _random.Next(minimumPassengerCount, maximumPassengerCount);
            Console.WriteLine($"Количество пассажиров: {_numbersPassengers}");
        }
        else
        {
            Console.WriteLine("Ошибка! Создайте направление.");
        }
    }

    private void SendTrain()
    {
        if (_numbersPassengers > 0)
        {
            string startingPoint = "Санкт-Петербург";

            var randomTrain = new Train();

            randomTrain.ChooseeTrain(out string train);

            FormTrain();

            if (_endpoint != null && _numbersPassengers != 0)
            {
                Console.WriteLine($"Поезд: {train}. Отправлен в направлении: {startingPoint} - {_endpoint}. Количество пассажиров: {_numbersPassengers}");
                _endpoint = null;
                _numbersPassengers = 0;
                _train.Clear();
            }
            else
            {
                Console.WriteLine("Текущий план поездов отсутствует.");
            }
        }
        else
        {
            Console.WriteLine("Поезд не может отправиться пустым!");
        }
    }

    private void FormTrain()
    {
        var placesOnTrain = new Wagon();

        placesOnTrain.CreatePlaces(out int places);

        int countTrainWagon = (int)Math.Ceiling((double)_numbersPassengers / places);

        for (int i = 0; i < countTrainWagon; i++)
        {
            _train.Add(0);
        }

        Console.WriteLine($"Количество вагонов состава: {countTrainWagon}\nВместимость одного вагона: {places}");
    }

    public void PrintCurrentInfo()
    {
        if (_endpoint != null && _numbersPassengers != 0)
        {
            Console.WriteLine($"Текущее место назначения: {_endpoint}");
            Console.WriteLine($"Текущее количество пассажиров: {_numbersPassengers}");
            Console.WriteLine($"Текущее количество вагонов состава: {_train.Count}");
        }
        else
        {
            Console.WriteLine("Текущий план поездов отсутствует.");
        }
    }
}

class Train
{
    private List<string> _trains = new List<string>
    {
        "Сапсан", "Ласточка", "Экспресс", "Карелия"
    };

    private Random _random = new Random();

    public void ChooseeTrain(out string train)
    {
        train = _trains[_random.Next(0, _trains.Count)];
    }
}

class Wagon
{
    private Random _random = new Random();

    public void CreatePlaces(out int places)
    {
        int _minimumPlaces = 200;
        int _maximumPlaces = 300;

        places = _random.Next(_minimumPlaces, _maximumPlaces);
    }
}