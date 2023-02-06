using UnityEngine;

namespace Rover.Core.Record
{
    [CreateAssetMenu(fileName = nameof(AnimalRecordSetup), menuName = "Rover/Core/Record/Animal Record Setup")]
    public class AnimalRecordSetup : ScriptableObject
    {
        public int startingMaxAvailableAnimals;
    }
}
