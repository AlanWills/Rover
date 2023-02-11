using Rover.Core.Objects;
using System;

namespace Rover.Core.Persistence
{
    [Serializable]
    public class RequestDTO
    {
        public AnimalRuntimeDTO animal;

        public RequestDTO(Request request)
        {
            animal = new AnimalRuntimeDTO(request.Animal);
        }
    }
}
