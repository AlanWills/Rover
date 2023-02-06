using CelesteEditor.Components.Catalogue;
using Rover.Core.Components;
using Rover.Core.Catalogue;
using UnityEditor;

namespace RoverEditor.Core.Catalogue
{
    [CustomEditor(typeof(AnimalComponentCatalogue))]
    public class AnimalComponentCatalogueEditor : ComponentCatalogueEditor<AnimalComponent>
    {
    }
}
