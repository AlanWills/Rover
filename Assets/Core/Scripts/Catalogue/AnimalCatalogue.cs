using Celeste.Objects;
using Rover.Core.Objects;
using UnityEngine;

namespace Rover.Core
{
    [CreateAssetMenu(fileName = nameof(AnimalCatalogue), menuName = "Rover/Core/Catalogue/Animal Catalogue")]
    public class AnimalCatalogue : ListScriptableObject<Animal>
    {
        public Animal FindByGuid(int guid)
        {
            return FindItem(x => x.Guid == guid);
        }
    }
}
