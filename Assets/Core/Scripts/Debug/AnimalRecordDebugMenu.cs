using Celeste;
using Celeste.Debug.Menus;
using Celeste.Tools;
using Rover.Core.Objects;
using Rover.Core.Record;
using Rover.Core.Runtime;
using UnityEngine;

namespace Rover.Core.Debug
{
    [CreateAssetMenu(fileName = nameof(AnimalRecordDebugMenu), menuName = "Rover/Core/Debug/Animal Record Debug Menu")]
    public class AnimalRecordDebugMenu : DebugMenu
    {
        #region Properties and Fields

        [SerializeField] private AnimalRecord animalRecord;
        [SerializeField] private AnimalCatalogue animalCatalogue;
        [SerializeField] private int animalEntriesPerPage = 10;

        private int currentPage = 0;

        #endregion

        protected override void OnDrawMenu()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Max Num Animals");

                using (new GUIEnabledScope(animalRecord.MaxNumAnimals > 1))
                {
                    if (GUILayout.Button("-", GUILayout.ExpandWidth(false)))
                    {
                        --animalRecord.MaxNumAnimals;
                    }
                }

                if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
                {
                    ++animalRecord.MaxNumAnimals;
                }
            }

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

                            using (new GUIEnabledScope(!animalRecord.HasReachedAnimalCapacity))
                            {
                                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                                {
                                    animalRecord.AddAnimal(animal);
                                }
                            }
                        }
                    });
            }

            GUILayout.Space(4);
            GUILayout.Label("Current Animals", CelesteGUIStyles.BoldLabel);

            using (new GUIIndentScope())
            {
                for (int i = 0, n = animalRecord.NumCurrentAnimals; i < n; ++i)
                {
                    AnimalRuntime animalRuntime = animalRecord.GetAnimal(i);
                    GUILayout.Label($"{animalRuntime.DisplayName} ({animalRuntime.Guid})");
                }
            }
        }
    }
}
