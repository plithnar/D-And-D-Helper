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

        static void Main(string[] args)
        {
            var input = ' ';
            while(input != 'x')
            {

                RollCharacterStats();

                Console.WriteLine("Enter 'x' to exit");
                var line = Console.ReadLine();
                if(line.Length > 0)
                {
                    input = line[0];
                }
                Console.WriteLine(input);
            }
        }

        static void RollCharacterStats(int minSumStats = 0)
        {
            var statResults = new List<int>();

            for (int i = 0; i < NUM_STATS; i++)
            {
                Console.WriteLine(RollSingleStat());
            }
            // Whitespace for readability
            Console.WriteLine();
            
        }

        static int RollSingleStat(int numRolls = 4)
        {
            var rollResults = new List<int>();

            if(numRolls < MIN_ROLLS_FOR_STAT)
            {
                Console.WriteLine("ERROR: Character creation requires at least 3 tolls for a single stat");
                return -1;
            }

            var die = new Die(_rng, 6);

            for(int i = 0; i < numRolls; i++)
            {
                rollResults.Add(die.Roll());
            }

            rollResults.Sort();
            rollResults.Reverse();
            rollResults.RemoveRange(MIN_ROLLS_FOR_STAT, numRolls - MIN_ROLLS_FOR_STAT);
            
            return rollResults.Sum();
        }
    }
}
