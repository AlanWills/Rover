using Celeste.Components;
using Rover.Core.Components;
using Rover.Core.Objects;

namespace Rover.Core.Runtime
{
    public class AnimalRuntime : ComponentContainerRuntime<AnimalComponent>
    {
        #region Properties and Fields

        public int Guid { get; }
        
        public string DisplayName => HasAnimalInfo ? animalInfo.iFace.GetDisplayName(animalInfo.instance) : string.Empty;
        public ExerciseState ExerciseState => HasAnimalExercise ? animalExercise.iFace.GetExerciseState(animalExercise.instance) : ExerciseState.None;
        
        public long RemainingExerciseTime
        {
            get
            {
                if (HasAnimalExercise && animalExercise.Is<ITimer>())
                {
                    var timer = animalExercise.As<ITimer>();
                    return timer.iFace.GetRemainingTime(timer.instance);
                }

                return -1;
            }
        }

        public float ExerciseProgressRatio
        {
            get
            {
                if (HasAnimalExercise && animalExercise.Is<ITimer>())
                {
                    var timer = animalExercise.As<ITimer>();
                    return timer.iFace.GetElapsedTimeRatio(timer.instance);
                }

                return 0;
            }
        }

        private bool HasAnimalInfo
        {
            get
            {
                if (!animalInfo.IsValid)
                {
                    TryFindComponent(out animalInfo);
                }

                return animalInfo.IsValid;
            }
        }

        private bool HasAnimalExercise
        {
            get
            {
                if (!animalExercise.IsValid)
                {
                    TryFindComponent(out animalExercise);
                }

                return animalExercise.IsValid;
            }
        }

        private InterfaceHandle<IAnimalInfo> animalInfo = InterfaceHandle<IAnimalInfo>.NULL;
        private InterfaceHandle<IAnimalExercise> animalExercise = InterfaceHandle<IAnimalExercise>.NULL;

        #endregion

        public AnimalRuntime(Animal animal)
        {
            Guid = animal.Guid;
        }

        public void StartExercise()
        {
            if (HasAnimalExercise)
            {
                animalExercise.iFace.StartExercise(animalExercise.instance);
            }
        }

        public void FinishExercise()
        {
            if (HasAnimalExercise)
            {
                animalExercise.iFace.FinishExercise(animalExercise.instance);
            }
        }

        public void CompleteExercise()
        {
            if (HasAnimalExercise)
            {
                animalExercise.iFace.CompleteExercise(animalExercise.instance);
            }
        }
    }
}
