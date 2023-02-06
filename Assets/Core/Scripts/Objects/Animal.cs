using Celeste.Components;
using Celeste.Objects;
using Rover.Core.Components;
using UnityEngine;

namespace Rover.Core.Objects
{
    [CreateAssetMenu(fileName = nameof(Animal), menuName = "Rover/Core/Objects/Animal")]
    public class Animal : ComponentContainerUsingSubAssets<AnimalComponent>, IGuid
    {
        #region Properties and Fields

        public int Guid
        {
            get => guid;
            set
            {
                if (guid != value)
                {
                    guid = value;
#if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(this);
#endif
                }
            }
        }

        [SerializeField] private int guid;

        #endregion
    }
}
