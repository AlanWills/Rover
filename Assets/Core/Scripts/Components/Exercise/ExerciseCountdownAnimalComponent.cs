using Celeste.Components;
using Celeste.Core;
using Celeste.Events;
using System;
using System.ComponentModel;
using UnityEngine;

namespace Rover.Core.Components
{
    [DisplayName("Exercise Countdown")]
    public class ExerciseCountdownAnimalComponent : AnimalComponent, IAnimalExercise, ITimer
    {
        #region Save Data

        [Serializable]
        private class SaveData : ComponentData
        {
            public ExerciseState currentState = ExerciseState.WaitingToStart;
            public long exerciseStartTime;
        }

        #endregion

        #region Properties and Fields

        [SerializeField] private ScheduledCallbacks scheduledCallbacks;
        [SerializeField] private int exerciseCountdownInSeconds = GameTime.SECONDS_PER_HOUR;

        private ICallbackHandle exerciseCompleteCallbackHandle = CallbackHandle.Invalid;

        #endregion

        public override ComponentData CreateData()
        {
            return new SaveData();
        }

        public override void Load(Instance instance)
        {
            base.Load(instance);

            SaveData saveData = instance.data as SaveData;
            
            if (saveData.currentState == ExerciseState.InProgess &&
                GameTime.ElapsedSecondsUntilNow(saveData.exerciseStartTime) >= exerciseCountdownInSeconds)
            {
                FinishExercise(instance);
            }
        }

        public override void Shutdown(Instance instance)
        {
            base.Shutdown(instance);

            scheduledCallbacks.Cancel(exerciseCompleteCallbackHandle);
        }

        public ExerciseState GetExerciseState(Instance instance)
        {
            SaveData saveData = instance.data as SaveData;
            return saveData.currentState;
        }

        public void StartExercise(Instance instance)
        {
            ExerciseState exerciseState = GetExerciseState(instance);
            UnityEngine.Debug.Assert(exerciseState == ExerciseState.WaitingToStart,
                $"Could not start exercise.  Current state is {exerciseState}, but needed {ExerciseState.WaitingToStart}.");

            if (exerciseState == ExerciseState.WaitingToStart)
            {
                SaveData saveData = instance.data as SaveData;
                saveData.currentState = ExerciseState.InProgess;
                saveData.exerciseStartTime = GameTime.UtcNow;
                instance.events.ComponentDataChanged.Invoke();
                exerciseCompleteCallbackHandle = scheduledCallbacks.Schedule(saveData.exerciseStartTime + exerciseCountdownInSeconds, () => FinishExercise(instance));
            }
        }

        public void FinishExercise(Instance instance)
        {
            ExerciseState exerciseState = GetExerciseState(instance);
            UnityEngine.Debug.Assert(exerciseState == ExerciseState.InProgess,
                $"Could not finish exercise.  Current state is {exerciseState}, but needed {ExerciseState.InProgess}.");

            if (exerciseState == ExerciseState.InProgess)
            {
                SaveData saveData = instance.data as SaveData;
                saveData.currentState = ExerciseState.WaitingToComplete;
                instance.events.ComponentDataChanged.Invoke();
            }
        }

        public void Complete(Instance instance)
        {
            ExerciseState exerciseState = GetExerciseState(instance);
            UnityEngine.Debug.Assert(exerciseState == ExerciseState.WaitingToComplete,
                $"Could not complete exercise.  Current state is {exerciseState}, but needed {ExerciseState.WaitingToComplete}.");

            if (exerciseState == ExerciseState.WaitingToComplete)
            {
                SaveData saveData = instance.data as SaveData;
                saveData.currentState = ExerciseState.Completed;
                instance.events.ComponentDataChanged.Invoke();
            }
        }

        public long GetEndTimestamp(Instance instance)
        {
            SaveData saveData = instance.data as SaveData;
            return saveData.currentState == ExerciseState.InProgess ? saveData.exerciseStartTime + exerciseCountdownInSeconds : GameTime.EpochTimestamp;
        }

        public long GetRemainingTime(Instance instance)
        {
            SaveData saveData = instance.data as SaveData;
            return saveData.currentState == ExerciseState.InProgess ? Math.Max(0, exerciseCountdownInSeconds - GameTime.ElapsedSecondsUntilNow(saveData.exerciseStartTime)) : GameTime.EpochTimestamp;
        }

        public float GetRemainingTimeRatio(Instance instance)
        {
            long remainingTime = GetRemainingTime(instance);
            return (float)remainingTime / exerciseCountdownInSeconds;
        }

        public long GetElapsedTime(Instance instance)
        {
            SaveData saveData = instance.data as SaveData;
            return saveData.currentState == ExerciseState.InProgess ? Math.Min(exerciseCountdownInSeconds, GameTime.ElapsedSecondsUntilNow(saveData.exerciseStartTime)) : 0;
        }

        public float GetElapsedTimeRatio(Instance instance)
        {
            long elapsedTime = GetElapsedTime(instance);
            return (float)elapsedTime / exerciseCountdownInSeconds;
        }
    }
}
