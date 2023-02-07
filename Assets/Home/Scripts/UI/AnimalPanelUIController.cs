using Rover.Core.Components;
using Rover.Core.Runtime;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rover.Home.UI
{
    [AddComponentMenu("Rover/Home/UI/Animal Panel UI Controller")]
    public class AnimalPanelUIController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button exerciseButton;
        [SerializeField] private Slider exerciseProgress;
        [SerializeField] private Button completeButton;

        private AnimalRuntime animalRuntime;
        private Coroutine progressCoroutine;

        #endregion

        public void Hookup(AnimalRuntime runtime)
        {
            animalRuntime = runtime;
            nameText.text = runtime.DisplayName;
            
            animalRuntime.ComponentDataChanged.AddListener(OnComponentDataChanged);

            RefreshUI();
        }

        public void HookupEmpty()
        {
            animalRuntime = null;
            nameText.text = "Vacant";

            RefreshUI();
        }

        private void RefreshUI()
        {
            if (animalRuntime == null)
            {
                exerciseButton.gameObject.SetActive(false);
                exerciseProgress.gameObject.SetActive(false);
                completeButton.gameObject.SetActive(false);
                return;
            }

            ExerciseState exerciseState = animalRuntime.ExerciseState;

            if (exerciseState == ExerciseState.InProgess)
            {
                StartProgressCoroutine();
            }
            else
            {
                StopProgressCoroutine();
            }

            switch (exerciseState)
            {
                case ExerciseState.None:
                    exerciseButton.gameObject.SetActive(false); 
                    exerciseProgress.gameObject.SetActive(false);
                    completeButton.gameObject.SetActive(false);
                    break;

                case ExerciseState.WaitingToStart:
                    exerciseButton.gameObject.SetActive(true);
                    exerciseProgress.gameObject.SetActive(false);
                    completeButton.gameObject.SetActive(false);
                    break;

                case ExerciseState.InProgess:
                    exerciseButton.gameObject.SetActive(false);
                    exerciseProgress.gameObject.SetActive(true);
                    completeButton.gameObject.SetActive(false);
                    break;

                case ExerciseState.WaitingToComplete:
                    exerciseButton.gameObject.SetActive(false);
                    exerciseProgress.gameObject.SetActive(false);
                    completeButton.gameObject.SetActive(true);
                    break;

                case ExerciseState.Completed:
                    UnhookRuntime();
                    HookupEmpty();
                    break;
            }
        }

        private void UnhookRuntime()
        {
            StopProgressCoroutine();

            if (animalRuntime != null)
            {
                animalRuntime.ComponentDataChanged.RemoveListener(OnComponentDataChanged);
                animalRuntime = null;
            }
        }

        private void StartProgressCoroutine()
        {
            StopProgressCoroutine();

            progressCoroutine = StartCoroutine(ProgressTick());
        }

        private void StopProgressCoroutine()
        {
            if (progressCoroutine != null)
            {
                StopCoroutine(progressCoroutine);
                progressCoroutine = null;
            }
        }

        private IEnumerator ProgressTick()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                exerciseProgress.value = animalRuntime.ExerciseProgressRatio;
            }
        }

        #region Unity Methods

        private void OnDisable()
        {
            UnhookRuntime();
        }

        #endregion

        #region Callbacks

        private void OnComponentDataChanged()
        {
            RefreshUI();
        }

        public void OnStartExercise()
        {
            animalRuntime.StartExercise();
        }

        public void OnCompleteExercise()
        {
            animalRuntime.CompleteExercise();
        }

        #endregion
    }
}
