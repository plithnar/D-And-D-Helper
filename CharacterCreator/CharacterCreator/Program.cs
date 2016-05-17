using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterCreator.Models;

namespace CharacterCreator
{
    class Program
    {
        private const int MIN_ROLLS_FOR_STAT = 3;
        private const int NUM_STATS          = 6;
        private static Random _rng = new Random();
        private static Settings _settings = new Settings();


        static void Main(string[] args)
        {
            EnterMenuLoop();
        }

        static void EnterMenuLoop()
        {
            var input = DisplayMenu();
            while (true)
            {
                switch(input)
                {
                    case 'c':
                        ChangeSettings();
                        break;
                    case 'r':
                        RollCharacterStats();
                        break;
                    case 'h':
                        // Configure help options
                        break;
                    case 'x':
                        return;
                    default:
                        Console.WriteLine("ERROR: '" + input + "' is not a valid option");
                        break;
                }
                input = DisplayMenu();
            }

        }

        static char DisplayMenu()
        {
            var input = ' ';
            Console.WriteLine("(c): Configure options");
            Console.WriteLine("(r): Roll character stats");
            Console.WriteLine("(h): Display help information");
            Console.WriteLine("(x): Exit program");
            Console.Write("User input: ");
            var line = Console.ReadLine();
            if (line.Length > 0)
            {
                input = line[0];
            }
            // For readability
            Console.WriteLine();
            return input;
        }

        static void RollCharacterStats()
        {
            var rolledStats = new List<int>();
            Console.WriteLine("Rolled Stats:");
            do
            {
                rolledStats.Clear();
                for (int i = 0; i < NUM_STATS; i++)
                {
                    var stat = RollSingleStat();
                    Console.WriteLine(stat);
                    rolledStats.Add(stat);
                }
                if(rolledStats.Sum() < _settings.MinStatSum)
                {
                    Console.WriteLine("Sum stats below threshhold: Re-rolling");
                }
            } while (rolledStats.Sum() < _settings.MinStatSum);
            Console.WriteLine("Sum of rolled stats: " + rolledStats.Sum());
            // Whitespace for readability
            Console.WriteLine();
            
        }

        static int RollSingleStat()
        {
            var rollResults = new List<int>();

            if(_settings.NumRolls < MIN_ROLLS_FOR_STAT)
            {
                Console.WriteLine("ERROR: Character creation requires at least 3 tolls for a single stat");
                return -1;
            }

            var die = new Die(_rng, 6);

            for(int i = 0; i < _settings.NumRolls; i++)
            {
                rollResults.Add(die.Roll());
            }

            rollResults.Sort();
            rollResults.Reverse();
            rollResults.RemoveRange(MIN_ROLLS_FOR_STAT, _settings.NumRolls - MIN_ROLLS_FOR_STAT);
            
            return rollResults.Sum();
        }

        static void ChangeSettings()
        {
            while (true)
            {
                try
                {
                    Console.Write("Please enter the number of rolls for a single stat: ");
                    var numRolls = Convert.ToInt32(Console.ReadLine());
                    if (numRolls < 1)
                    {
                        throw new FormatException();
                    }
                    _settings.NumRolls = numRolls;
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("ERROR: Entry must be a positive integer value");
                }
            }
            while (true)
            {
                try
                {
                    Console.Write("Please enter the minimum summed stat value: ");
                    var minStatSum = Convert.ToInt32(Console.ReadLine());
                    if (minStatSum < 0 || minStatSum > 108)
                    {
                        throw new FormatException();
                    }
                    _settings.MinStatSum = minStatSum;
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("ERROR: Entry must be an integer value between 0 and 108");
                }
            }
        }
    }
}
