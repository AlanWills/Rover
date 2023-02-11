using Celeste.Persistence;
using Rover.Core.Objects;
using Rover.Core.Persistence;
using Rover.Core.Record;
using Rover.Core.Runtime;
using UnityEngine;

namespace Rover.Core.Managers
{
    [CreateAssetMenu(fileName = nameof(AnimalManager), menuName = "Rover/Core/Managers/Animal Manager")]
    public class AnimalManager : PersistentSceneManager<AnimalManager, AnimalRecordDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "Animals.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private AnimalRecordSetup animalRecordSetup;
        [SerializeField] private AnimalRecord animalRecord;
        [SerializeField] private AnimalCatalogue animalCatalogue;

        #endregion

        #region Save/Load

        protected override AnimalRecordDTO Serialize()
        {
            return new AnimalRecordDTO(animalRecord);
        }

        protected override void Deserialize(AnimalRecordDTO dto)
        {
            animalRecord.MaxNumAnimals = dto.maxNumAnimals;

            foreach (AnimalRuntimeDTO animalDTO in dto.currentAnimals)
            {
                Animal animal = animalCatalogue.FindByGuid(animalDTO.guid);
                UnityEngine.Debug.Assert(animal != null, $"Could not find animal with guid {animalDTO.guid}.");

                AnimalRuntime animalRuntime = new AnimalRuntime(animal);
                animalRuntime.InitializeComponents(animal);
                animalRuntime.LoadComponents(animalDTO.components);
                animalRecord.AddAnimal(animalRuntime);
            }
        }

        protected override void SetDefaultValues()
        {
            animalRecord.SetupUsing(animalRecordSetup);
        }

        #endregion
    }
}
