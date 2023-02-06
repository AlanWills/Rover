using Celeste.DataStructures;
using Rover.Core.Objects;
using Rover.Core.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rover.Core.Record
{
    [CreateAssetMenu(fileName = nameof(AnimalRecord), menuName = "Rover/Core/Record/Animal Record")]
    public class AnimalRecord : ScriptableObject
    {
        #region Properties and Fields

        public int NumCurrentAnimals => currentAnimals.Count;
        public bool HasReachedAnimalCapacity => NumCurrentAnimals >= MaxNumAnimals;

        public int MaxNumAnimals
        {
            get => maxNumAnimals;
            set
            {
                if (maxNumAnimals != value)
                {
                    maxNumAnimals = value;
                    onChange.Invoke();
                }
            }
        }

        [Header("Events")]
        [SerializeField] private Celeste.Events.Event onChange;

        [NonSerialized] private int maxNumAnimals = 0;
        [NonSerialized] private List<AnimalRuntime> currentAnimals = new List<AnimalRuntime>();

        #endregion

        public void SetupUsing(AnimalRecordSetup setup)
        {
            maxNumAnimals = setup.startingMaxAvailableAnimals;
        }

        public void AddAnimal(Animal animal)
        {
            if (NumCurrentAnimals < MaxNumAnimals)
            {
                AnimalRuntime animalRuntime = new AnimalRuntime(animal);
                animalRuntime.InitializeComponents(animal);
                currentAnimals.Add(animalRuntime);
                onChange.Invoke();
            }
        }

        public AnimalRuntime GetAnimal(int index)
        {
            return currentAnimals.Get(index);
        }
    }
}
