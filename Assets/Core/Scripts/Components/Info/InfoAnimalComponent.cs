using Celeste.Components;
using System.ComponentModel;
using UnityEngine;

namespace Rover.Core.Components
{
    [DisplayName("Static Info")]
    public class StaticInfoAnimalComponent : AnimalComponent, IAnimalInfo
    {
        #region Properties and Fields

        [SerializeField] private string displayName;

        #endregion

        public string GetDisplayName(Instance instance)
        {
            return displayName;
        }
    }
}
