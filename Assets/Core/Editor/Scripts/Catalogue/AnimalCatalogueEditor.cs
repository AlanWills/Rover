using CelesteEditor.DataStructures;
using Rover.Core;
using Rover.Core.Objects;
using UnityEditor;

namespace RoverEditor.Core.Catalogue
{
    [CustomEditor(typeof(AnimalCatalogue))]
    public class AnimalCatalogueEditor : IIndexableItemsEditor<Animal>
    {
    }
}
