using CelesteEditor.Components;
using Rover.Core.Components;
using Rover.Core.Objects;
using System;
using UnityEditor;

namespace RoverEditor.Core.Objects
{
    [CustomEditor(typeof(Animal))]
    public class AnimalEditor : ComponentContainerUsingSubAssetsEditor<AnimalComponent>
    {
        protected override Type[] AllComponentTypes => RoverCoreEditorConstants.AllAnimalComponentTypes;
        protected override string[] AllComponentDisplayNames => RoverCoreEditorConstants.AllAnimalComponentDisplayNames;
    }
}
