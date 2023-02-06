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

        private InterfaceHandle<IAnimalInfo> animalInfo = InterfaceHandle<IAnimalInfo>.NULL;

        #endregion

        public AnimalRuntime(Animal animal)
        {
            Guid = animal.Guid;
        }
    }
}
