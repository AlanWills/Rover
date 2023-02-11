using Rover.Core.Runtime;

namespace Rover.Core.Objects
{
    public class Request
    {
        #region Properties and Fields

        public int Guid => animalRuntime.Guid;
        public string DisplayName => animalRuntime.DisplayName;
        public AnimalRuntime Animal => animalRuntime;

        private AnimalRuntime animalRuntime;

        #endregion

        public Request(AnimalRuntime animalRuntime)
        {
            this.animalRuntime = animalRuntime;
        }
    }
}
