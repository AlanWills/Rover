using Rover.Core.Components;
using Rover.Core.Runtime;
using TMPro;
using UnityEngine;

namespace Rover.Home.UI
{
    [AddComponentMenu("Rover/Home/UI/Animal Panel UI Controller")]
    public class AnimalPanelUIController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private TextMeshProUGUI nameText;

        #endregion

        public void Hookup(AnimalRuntime animalRuntime)
        {
            nameText.text = animalRuntime.DisplayName;
        }

        public void HookupEmpty()
        {
            nameText.text = "Vacant";
        }
    }
}
