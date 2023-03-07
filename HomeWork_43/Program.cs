using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Direction direction = new Direction();
        TicketOffice ticketOffice = new TicketOffice(direction);
        TrainDepot trainDepot = new TrainDepot();
        Wagon wagon = new Wagon();
        Station station = new Station(direction, ticketOffice, trainDepot, wagon);
        Menu menu = new Menu(direction, ticketOffice, station);
        
        menu.Work();
    }
}

class Menu
{
    private Station _station;
    private TicketOffice _ticketOffice;
    private Direction _direction;

    public Menu(Direction direction, TicketOffice ticketOffice, Station station)
    {
        _direction = direction;
        _ticketOffice = ticketOffice;
        _station = station;
    }

    public void Work()
    {
        const string Create = "1";
        const string Sell = "2";
        const string Send = "3";
        const string Info = "4";
        const string Exit = "5";

        bool isWork = true;

        Console.WriteLine("Добро пожаловать на Московский вокзал! Что желаете сделать?");

        while (isWork)
        {
            Console.WriteLine($"{Create} - Создать направление" +
                              $"\n{Sell} - Продать билеты\n{Send} - Отправка" +
                              $"\n{Info} - План поездов\n{Exit} - Выход");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case Create:
                    _direction.CreateDirection();
                    break;

                case Sell:
                    _ticketOffice.SellTickets();
                    break;

                case Send:
                    _station.SendTrain();
                    break;

                case Info:
                    _station.PrintCurrentInfo();
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

class Station
{
    private List<Wagon> _wagons = new List<Wagon>();

    private Direction _direction;
    private TicketOffice _ticketOffice;
    private TrainDepot _trainDepot;
    private Wagon _wagon;

    public Station(Direction direction, TicketOffice ticketOffice, TrainDepot trainDepot, Wagon wagon)
    {
        _direction = direction;
        _ticketOffice = ticketOffice;
        _trainDepot = trainDepot;
        _wagon = wagon;
    }

    public void SendTrain()
    {
        if (_ticketOffice.NumberPassengers != 0)
        {
            var train = _trainDepot.ChooseTrain();

            FormTrain();

            Console.WriteLine($"Поезд: {train.Name}. Отправлен в направлении: {_direction.StartPoint} - {_direction.Endpoint}." +
                              $" Количество пассажиров: {_ticketOffice.NumberPassengers}");

            _wagons.Clear();
        }
        else
        {
            Console.WriteLine("Ошибка! Поезд не может отправиться пустым.");
        }
    }

    public void FormTrain()
    {
        _wagon.CreatePlaces();

        int countTrainWagon = (int)Math.Ceiling((double)_ticketOffice.NumberPassengers / _wagon.Places);

        for (int i = 0; i < countTrainWagon; i++)
        {
            _wagons.Add(new Wagon());
        }

        Console.WriteLine($"Количество вагонов состава: {_wagons.Count}\nВместимость одного вагона: {_wagon.Places}");
    }

    public void PrintCurrentInfo()
    {
        if (_direction.Endpoint != null && _ticketOffice.NumberPassengers != 0)
        {
            Console.WriteLine($"Текущее место назначения: {_direction.Endpoint}");
            Console.WriteLine($"Текущее количество пассажиров: {_ticketOffice.NumberPassengers}");
            Console.WriteLine($"Текущее количество вагонов состава: {_wagons.Count}");
        }
        else
        {
            Console.WriteLine("Ошибка! Текущий план поездов отсутствует.");
        }
    }
}

class TicketOffice
{
    private Random _random = new Random();
    private Direction _direction;

    public TicketOffice(Direction direction)
    {
        _direction = direction;
    }

    public int NumberPassengers { get; private set; }

    public void SellTickets()
    {
        if (_direction.Endpoint != null)
        {
            int minimumPassengerCount = 1500;
            int maximumPassengerCount = 2000;

            NumberPassengers = _random.Next(minimumPassengerCount, maximumPassengerCount);
            Console.WriteLine($"Количество пассажиров: {NumberPassengers}");
        }
        else
        {
            Console.WriteLine("Ошибка! Создайте направление.");
        }
    }
}

class Direction
{
    public string StartPoint { get; private set; } = "Санкт-Петербург";

    public string Endpoint { get; private set; }

    public void CreateDirection()
    {
        Console.Write("Укажите пункт назначения: ");
        Endpoint = Console.ReadLine();
    }
}

class TrainDepot
{
    private List<Train> _trains = new List<Train>
    {
        new Train("Сапсан"),
        new Train("Ласточка"),
        new Train("Экспресс"),
        new Train("Карелия")
    };

    private Random _random = new Random();

    public Train ChooseTrain()
    {
        return _trains[_random.Next(_trains.Count)];
    }
}

class Train
{
    public Train(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}

class Wagon
{
    private Random _random = new Random();

    public int Places { get; private set; }

    public void CreatePlaces()
    {
        int _minimumPlaces = 200;
        int _maximumPlaces = 300;

        Places = _random.Next(_minimumPlaces, _maximumPlaces);
    }
}