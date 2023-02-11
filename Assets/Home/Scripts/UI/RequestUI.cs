using PolyAndCode.UI;
using Rover.Core;
using Rover.Core.Objects;
using TMPro;
using UnityEngine;

namespace Rover.Home.UI
{
    [AddComponentMenu("Rover/Home/UI/Request UI")]
    public class RequestUI : MonoBehaviour, ICell
    {
        #region Properties and Fields

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI animalName;

        [Header("Events")]
        [SerializeField] private RequestEvent acceptRequestEvent;
        [SerializeField] private RequestEvent declineRequestEvent;

        private Request request;

        #endregion

        public void ConfigureCell(RequestUIData requestUIData, int cellIndex)
        {
            request = requestUIData.Request;

            animalName.text = requestUIData.Name;
        }

        #region Callbacks

        public void OnAcceptRequestButtonClicked()
        {
            acceptRequestEvent.Invoke(request);
        }

        public void OnDeclineRequestButtonClicked()
        {
            declineRequestEvent.Invoke(request);
        }

        #endregion
    }
}
