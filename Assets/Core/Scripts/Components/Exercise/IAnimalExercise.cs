using Celeste.Components;

namespace Rover.Core.Components
{
    public enum ExerciseState
    {
        None,
        WaitingToStart,
        InProgess,
        WaitingToComplete,
        Completed
    }

    public interface IAnimalExercise
    {
        ExerciseState GetExerciseState(Instance instance);

        void StartExercise(Instance instance);
        void FinishExercise(Instance instance);
        void CompleteExercise(Instance instance);
    }
}
