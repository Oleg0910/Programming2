using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Obchis_2_C
{
    public enum City
    {
        Lviv,
        Krakow,
        London,
        NewYork,
        Tokyo
    }

    public enum Airline
    {
        WizzAir,
        Ryanair,
        LOT,
        United,
        Delta
    }

    public class Flight
    {
        public City DepartureCity { get; set; }
        public City ArrivalCity { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public Airline Airline { get; set; }
        public decimal Price { get; set; }

        public Flight(City departurecity, City arrivalcity, DateTime departuredatetime, DateTime arrivaldatetime, Airline airline, decimal price)
        {
            DepartureCity = departurecity;
            ArrivalCity = arrivalcity;
            DepartureDatetime = departuredatetime;
            ArrivalDatetime = arrivaldatetime;
            Airline = airline;
            Price = price;
        }

    }

    public class DijkstraAlgorithm
    {
        private readonly List<Flight> _flights;

        public DijkstraAlgorithm(List<Flight> flights)
        {
            _flights = flights;
        }

        public List<Flight> FindShortestPath(City source, City destination)
        {
            Dictionary<City, decimal> distances = new Dictionary<City, decimal>();
            Dictionary<City, City> _previous = new Dictionary<City, City>();
            HashSet<City> unvisited = new HashSet<City>();

            // Initialize distances and unvisited set
            foreach (Flight flight in _flights)
            {
                if (!distances.ContainsKey(flight.DepartureCity))
                {
                    distances.Add(flight.DepartureCity, decimal.MaxValue);
                    unvisited.Add(flight.DepartureCity);
                }
                if (!distances.ContainsKey(flight.ArrivalCity))
                {
                    distances.Add(flight.ArrivalCity, decimal.MaxValue);
                    unvisited.Add(flight.ArrivalCity);
                }
            }

            distances[source] = 0;

            while (unvisited.Count > 0)
            {
                // Get the unvisited city with the smallest distance
                City current = unvisited.OrderBy(c => distances[c]).First();

                // Stop if the destination has been visited
                if (current == destination)
                {
                    break;
                }

                unvisited.Remove(current);

                // Update the distances of the neighboring cities
                List<Flight> neighboringFlights = _flights.Where(f => f.DepartureCity == current && unvisited.Contains(f.ArrivalCity)).ToList();
                foreach (Flight flight in neighboringFlights)
                {
                    if (distances[current] == decimal.MaxValue)
                    {
                        // The distance to the current node is already at maximum, no need to update its neighbors
                        break;
                    }

                    decimal alternativeDistance = distances[current] + flight.Price;
                    if (alternativeDistance < distances[flight.ArrivalCity])
                    {
                        // Check for overflow
                        if (decimal.MaxValue - alternativeDistance < 0)
                        {
                            // The alternative distance is too large to fit in the decimal data type, set it to the maximum value instead
                            distances[flight.ArrivalCity] = decimal.MaxValue;
                        }
                        else
                        {
                            distances[flight.ArrivalCity] = alternativeDistance;
                        }
                        _previous[flight.ArrivalCity] = current;
                    }
                }
            }

            if (_previous.ContainsKey(destination))
            {
                // Construct the shortest path
                List<Flight> shortestPath = new List<Flight>();
                City current = destination;
                while (_previous.ContainsKey(current))
                {
                    Flight flight = _flights.FirstOrDefault(f => f.DepartureCity == _previous[current] && f.ArrivalCity == current);
                    shortestPath.Insert(0, flight);
                    current = _previous[current];
                }

                return shortestPath;
            }
            else
            {
                // No path exists
                return null;
            }
        }
    }

    public class Utility
    {
        public bool Validation(string line)
        {
            var values = line.Split(',');
            Console.ForegroundColor = ConsoleColor.Red;
            if (values.Length != 6)
            {
                Console.WriteLine("Invalid number of fields: {0}", line);
                Console.ResetColor();
                return false;
            }
            if (!Enum.TryParse<City>(values[0], out var departureCity))
            {
                Console.WriteLine("Invalid departure city: {0}", line);
                Console.ResetColor();
                return false;
            }
            if (!Enum.TryParse<City>(values[1], out var arrivalCity))
            {
                Console.WriteLine("Invalid arrival city: {0}", line);
                Console.ResetColor();
                return false;
            }
            if (!DateTime.TryParseExact(values[2], "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out var departureDatetime))
            {
                Console.WriteLine("Invalid departure datetime: {0}", line);
                Console.ResetColor();
                return false;
            }
            if (!DateTime.TryParseExact(values[3], "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out var arrivalDatetime))
            {
                Console.WriteLine("Invalid arrival datetime: {0}", line);
                Console.ResetColor();
                return false;
            }
            if (!Enum.TryParse<Airline>(values[4], out var airline))
            {
                Console.WriteLine("Invalid airline: {0}", line);
                Console.ResetColor();
                return false;
            }
            if (!decimal.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
            {
                Console.WriteLine("Invalid price: {0}", line);
                Console.ResetColor();
                return false;
            }
            if (arrivalDatetime <= departureDatetime)
            {
                Console.WriteLine("Arrival datetime must be after departure datetime: {0}", line);
                Console.ResetColor();
                return false;
            }
            if ((arrivalDatetime - departureDatetime) < TimeSpan.FromHours(1) || (arrivalDatetime - departureDatetime) > TimeSpan.FromHours(7))
            {
                Console.WriteLine("The time between arrival and departure should be between 1 and 7 hours: {0}", line);
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }

        public List<Flight> UserFlightInput()
        {
            {
                List<Flight> flights = new List<Flight>();
                Console.WriteLine("Enter flight information like in the example:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("DepartureCity,ArrivalCity,DepartureDatetime,ArrivalDatetime,Airline,Price");
                Console.WriteLine("Tokyo,Lviv,2023-03-16T14:00:00Z,2023-03-16T22:00:00Z,LOT,200.00");
                Console.ResetColor();
                string line = Console.ReadLine();
                bool correct = Validation(line);
                if (correct)
                {
                    var values = line.Split(',');
                    var departureCity = (City)Enum.Parse(typeof(City), values[0]);
                    var arrivalCity = (City)Enum.Parse(typeof(City), values[1]);
                    var departureDatetime = DateTime.ParseExact(values[2], "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                    var arrivalDatetime = DateTime.ParseExact(values[3], "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                    var airline = (Airline)Enum.Parse(typeof(Airline), values[4]);
                    var price = decimal.Parse(values[5], CultureInfo.InvariantCulture);
                    Flight flight = new Flight(departureCity, arrivalCity, departureDatetime, arrivalDatetime, airline, price);
                    flights.Add(flight);
                }
                return flights;
            }
        }

        public List<Flight> FileFlights(string filePath)
        {
            List<Flight> flights = new List<Flight>();

            using (var reader = new StreamReader(filePath))
            {
                bool headerRow = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (headerRow)
                    {
                        headerRow = false;
                        continue;
                    }

                    if (!Validation(line)) { continue; }


                    var values = line.Split(',');
                    var departureCity = (City)Enum.Parse(typeof(City), values[0]);
                    var arrivalCity = (City)Enum.Parse(typeof(City), values[1]);
                    var departureDatetime = DateTime.ParseExact(values[2], "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                    var arrivalDatetime = DateTime.ParseExact(values[3], "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                    var airline = (Airline)Enum.Parse(typeof(Airline), values[4]);
                    var price = decimal.Parse(values[5], CultureInfo.InvariantCulture);

                    bool correct = true;
                    var flight = new Flight(departureCity, arrivalCity, departureDatetime, arrivalDatetime, airline, price);
                    if (correct)
                    {
                        flights.Add(flight);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid flight: DepartureCity={departureCity}, ArrivalCity={arrivalCity}, DepartureDatetime={departureDatetime}, ArrivalDatetime={arrivalDatetime}, Airline={airline}, Price={price}");
                        Console.ResetColor();
                    }
                }

                return flights;
            }
        }

        public void PrintFlights(List<Flight> flights)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var flight in flights)
            {
                Console.WriteLine($"{flight.DepartureCity}, {flight.ArrivalCity}, {flight.DepartureDatetime}, {flight.ArrivalDatetime}, {flight.Airline}, {flight.Price}");
            }
            Console.ResetColor();
        }
    }
    class Program
    {

        static void HelpPrint()
        {
            Console.WriteLine("1) file input");
            Console.WriteLine("2) user input");
            Console.WriteLine("3) print");
            Console.WriteLine("4) min flight");
            Console.WriteLine("5) help");
            Console.WriteLine("6) exit");
        }


        static bool InputLogic(string value, ref List<Flight> flights, ref Utility utility)
        {
            switch (value)
            {
                case "file input":
                    var fileflights = utility.FileFlights("C:\\Users\09102\\Documents\\Навчальна_Практика\\Tests\\Np2\\Np2\\Flights.csv");
                    foreach (var flight in fileflights)
                    {
                        flights.Add(flight);
                    }
                    return true;
                case "user input":
                    var userflight = utility.UserFlightInput();
                    if (userflight.Count != 0)
                    {
                        flights.AddRange(userflight);
                    }
                    return true;
                case "print":
                    utility.PrintFlights(flights);
                    return true;
                case "min flight":
                    var minflightfinder = new DijkstraAlgorithm(flights);
                    Console.WriteLine("Enter departure city:");
                    string departureInput = Console.ReadLine();
                    Console.WriteLine("Enter arrival city:");
                    string arrivalInput = Console.ReadLine();

                    City departureCity;
                    City arrivalCity;

                    if (Enum.TryParse(departureInput, out departureCity) && Enum.TryParse(arrivalInput, out arrivalCity))
                    {
                        if (Enum.IsDefined(typeof(City), departureCity) && Enum.IsDefined(typeof(City), arrivalCity))
                        {
                            List<Flight> shortestPath = minflightfinder.FindShortestPath(departureCity, arrivalCity);
                            if (shortestPath == null)
                            {
                                Console.WriteLine("No path found.");
                            }
                            else
                            {
                                Console.WriteLine($"Shortest path from {departureCity} to {arrivalCity}:");
                                foreach (Flight flight in shortestPath)
                                {
                                    Console.WriteLine($"- {flight.DepartureCity} ({flight.DepartureDatetime}) -> {flight.ArrivalCity} ({flight.ArrivalDatetime}) by {flight.Airline}, ${flight.Price}");
                                }
                                Console.WriteLine($"Total cost: ${shortestPath.Sum(f => f.Price)}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid city.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid city.");
                    }

                    return true;
                case "help":
                    HelpPrint();
                    return true;
                case "exit":
                    return false;
                default:
                    Console.WriteLine("Wrong input!!!");
                    return true;
            }
        }

        static void Main(string[] args)
        {
            Utility utility = new Utility();
            List<Flight> flights = new List<Flight>();
            bool programContinue = true;
            HelpPrint();

            while (programContinue)
            {
                Console.Write("Enter your choice: ");
                string value = Console.ReadLine().ToLower();
                programContinue = InputLogic(value, ref flights, ref utility);
            }
        }
    }
}

