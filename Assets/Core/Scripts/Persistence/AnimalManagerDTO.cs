using Rover.Core.Record;
using System;

namespace Rover.Core.Persistence
{
    [Serializable]
    public class AnimalRecordDTO
    {
        public int maxNumAnimals;

        public AnimalRecordDTO(AnimalRecord animalRecord)
        {
            maxNumAnimals = animalRecord.MaxNumAnimals;
        }
    }
}
