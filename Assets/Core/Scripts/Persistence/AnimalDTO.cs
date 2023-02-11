using Celeste.Components.Persistence;
using Rover.Core.Runtime;
using System;
using System.Collections.Generic;

namespace Rover.Core.Persistence
{
    [Serializable]
    public class AnimalRuntimeDTO
    {
        public int guid;
        public List<ComponentDTO> components = new List<ComponentDTO>();

        public AnimalRuntimeDTO(AnimalRuntime animalRuntime)
        {
            guid = animalRuntime.Guid;

            components.Capacity = animalRuntime.NumComponents;
            
            for (int i = 0, n = animalRuntime.NumComponents; i < n; ++i)
            {
                components.Add(ComponentDTO.From(animalRuntime.GetComponent(i)));
            }
        }
    }
}
