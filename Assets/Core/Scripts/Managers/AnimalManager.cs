using Celeste.Persistence;
using Rover.Core.Persistence;
using Rover.Core.Record;
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

        #endregion

        #region Save/Load

        protected override AnimalRecordDTO Serialize()
        {
            return new AnimalRecordDTO(animalRecord);
        }

        protected override void Deserialize(AnimalRecordDTO dto)
        {
            animalRecord.MaxNumAnimals = dto.maxNumAnimals;
        }

        protected override void SetDefaultValues()
        {
            animalRecord.SetupUsing(animalRecordSetup);
        }

        #endregion
    }
}
