using Rover.Core.Record;
using System;
using System.Collections.Generic;

namespace Rover.Core.Persistence
{
    [Serializable]
    public class AnimalRecordDTO
    {
        public int maxNumAnimals;
        public List<AnimalRuntimeDTO> currentAnimals = new List<AnimalRuntimeDTO>();

        public AnimalRecordDTO(AnimalRecord animalRecord)
        {
            maxNumAnimals = animalRecord.MaxNumAnimals;

            currentAnimals.Capacity = animalRecord.NumCurrentAnimals;

            for (int i = 0, n = animalRecord.NumCurrentAnimals; i < n; ++i)
            {
                currentAnimals.Add(new AnimalRuntimeDTO(animalRecord.GetAnimal(i)));
            }
        }
    }
}
