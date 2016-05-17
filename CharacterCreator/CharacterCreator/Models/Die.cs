using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models
{
    public class Die
    {
        private int _numFaces;
        private Random _rng;

        public Die(Random rng, int numFaces)
        {
            _numFaces = numFaces;
            _rng = rng;
        }

        public int NumFaces
        {
            get { return _numFaces; }
        }

        /// <summary>
        /// Rolls the die and returns a random value between 1 and the number of faces
        /// that the die has
        /// </summary>
        /// <returns></returns>
        public int Roll()
        {
            var outcome = _rng.Next(1, NumFaces + 1);
            return outcome;
        }
    }
}
