using Celeste.Components.Catalogue;
using Rover.Core.Components;
using UnityEngine;

namespace Rover.Core.Catalogue
{
    [CreateAssetMenu(fileName = nameof(AnimalComponentCatalogue), menuName = "Rover/Core/Catalogue/Animal Component Catalogue")]
    public class AnimalComponentCatalogue : ComponentCatalogue<AnimalComponent>
    {
    }
}
