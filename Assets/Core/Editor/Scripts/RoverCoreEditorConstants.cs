using CelesteEditor.Tools.Utils;
using Rover.Core.Components;
using System;

namespace RoverEditor.Core
{
    public static class RoverCoreEditorConstants
    {
        #region Properties and Fields

        public static readonly Type[] AllAnimalComponentTypes;
        public static readonly string[] AllAnimalComponentDisplayNames;

        #endregion

        static RoverCoreEditorConstants()
        {
            TypeUtils.LoadTypes<AnimalComponent>(ref AllAnimalComponentTypes, ref AllAnimalComponentDisplayNames);
        }
    }
}
