using Celeste;
using Celeste.Debug.Menus;
using Celeste.Tools;
using Rover.Core.Objects;
using Rover.Core.Record;
using Rover.Core.Runtime;
using UnityEngine;

namespace Rover.Core.Debug
{
    [CreateAssetMenu(fileName = nameof(RequestRecordDebugMenu), menuName = "Rover/Core/Debug/Request Record Debug Menu")]
    public class RequestRecordDebugMenu : DebugMenu
    {
        #region Properties and Fields

        [SerializeField] private RequestRecord requestRecord;
        [SerializeField] private AnimalCatalogue animalCatalogue;
        [SerializeField] private RequestEvent acceptRequestEvent;
        [SerializeField] private RequestEvent declineRequestEvent;
        [SerializeField] private int animalEntriesPerPage = 10;

        private int currentPage = 0;

        #endregion

        protected override void OnDrawMenu()
        {
            GUILayout.Space(4);
            GUILayout.Label("Available Animals", CelesteGUIStyles.BoldLabel);

            using (new GUIIndentScope())
            {
                GUIUtils.ReadOnlyPaginatedList(
                    currentPage,
                    animalEntriesPerPage,
                    animalCatalogue.NumItems,
                    (index) =>
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            Animal animal = animalCatalogue.GetItem(index);
                            GUILayout.Label($"{animal.name} ({animal.Guid})");

                            if (GUILayout.Button("Add Request", GUILayout.ExpandWidth(false)))
                            {
                                AnimalRuntime animalRuntime = new AnimalRuntime(animal);
                                animalRuntime.InitializeComponents(animal);
                                requestRecord.AddRequest(new Request(animalRuntime));
                            }
                        }
                    });
            }

            GUILayout.Space(4);
            GUILayout.Label("Current Requests", CelesteGUIStyles.BoldLabel);

            using (new GUIIndentScope())
            {
                for (int i = requestRecord.NumPendingRequests - 1; i >= 0; --i)
                {
                    Request request = requestRecord.GetRequest(i);

                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label($"{request.DisplayName} ({request.Guid})");

                        using (new GUIEnabledScope(requestRecord.CanAcceptRequest(i)))
                        {
                            if (GUILayout.Button("Accept", GUILayout.ExpandWidth(false)))
                            {
                                acceptRequestEvent.Invoke(request);
                            }
                        }

                        if (GUILayout.Button("Decline", GUILayout.ExpandWidth(false)))
                        {
                            declineRequestEvent.Invoke(request);
                        }
                    }
                }
            }
        }
    }
}
