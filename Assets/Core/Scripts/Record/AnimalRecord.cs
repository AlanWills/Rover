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

        public void AddAnimal(AnimalRuntime animalRuntime)
        {
            UnityEngine.Debug.Assert(NumCurrentAnimals < MaxNumAnimals, $"Could not add animal {animalRuntime.Guid} because we already have the maximum number.");
            if (NumCurrentAnimals < MaxNumAnimals)
            {
                animalRuntime.ComponentDataChanged.AddListener(OnAnimalComponentDataChanged);
                currentAnimals.Add(animalRuntime);
                onChange.Invoke();
            }
        }

        public void RemoveAnimal(int index)
        {
            if (index < NumCurrentAnimals)
            {
                AnimalRuntime animalRuntime = currentAnimals[index];
                animalRuntime.ComponentDataChanged.RemoveListener(OnAnimalComponentDataChanged);
                animalRuntime.ShutdownComponents();
                currentAnimals.RemoveAt(index);

                onChange.Invoke();
            }
        }

        public AnimalRuntime GetAnimal(int index)
        {
            return currentAnimals.Get(index);
        }

        #region Callbacks

        private void OnAnimalComponentDataChanged()
        {
            for (int i = NumCurrentAnimals - 1; i >= 0; --i)
            {
                var animal = GetAnimal(i);

                if (animal.ExerciseState == Components.ExerciseState.Completed)
                {
                    RemoveAnimal(i);
                }
            }

            onChange.Invoke();
        }

        #endregion
    }
}
