using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models
{
    public class Settings
    {
        private int _minStatSum;
        private int _numRolls;

        public Settings()
        {
            MinStatSum = 0;
            NumRolls = 4;
        }

        public int MinStatSum
        {
            get { return _minStatSum; }
            set { _minStatSum = value; }
        }

        public int NumRolls
        {
            get { return _numRolls; }
            set { _numRolls = value; }
        }
    }
}
