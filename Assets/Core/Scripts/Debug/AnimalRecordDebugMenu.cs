using Celeste;
using Celeste.Debug.Menus;
using Celeste.Tools;
using Rover.Core.Components;
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
            GUILayout.Label("Current Animals", CelesteGUIStyles.BoldLabel);

            using (new GUIIndentScope())
            {
                for (int i = animalRecord.NumCurrentAnimals - 1; i >= 0; --i)
                {
                    AnimalRuntime animalRuntime = animalRecord.GetAnimal(i);

                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label($"{animalRuntime.DisplayName} ({animalRuntime.Guid})");

                        if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                        {
                            animalRecord.RemoveAnimal(i);
                        }
                    }

                    ExerciseState exerciseState = animalRuntime.ExerciseState;

                    if (exerciseState != ExerciseState.None)
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            using (new GUIEnabledScope(exerciseState == ExerciseState.WaitingToStart))
                            {
                                if (GUILayout.Button("Start", GUILayout.ExpandWidth(false)))
                                {
                                    animalRuntime.StartExercise();
                                }
                            }

                            using (new GUIEnabledScope(exerciseState == ExerciseState.InProgess))
                            {
                                if (GUILayout.Button("Finish", GUILayout.ExpandWidth(false)))
                                {
                                    animalRuntime.FinishExercise();
                                }
                            }

                            using (new GUIEnabledScope(exerciseState == ExerciseState.WaitingToComplete))
                            {
                                if (GUILayout.Button("Complete", GUILayout.ExpandWidth(false)))
                                {
                                    animalRuntime.CompleteExercise();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
