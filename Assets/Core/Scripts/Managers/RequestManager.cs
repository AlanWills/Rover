using Celeste.Persistence;
using Rover.Core.Objects;
using Rover.Core.Persistence;
using Rover.Core.Record;
using Rover.Core.Runtime;
using UnityEngine;

namespace Rover.Core.Managers
{
    [AddComponentMenu("Rover/Core/Managers/Request Manager")]
    public class RequestManager : PersistentSceneManager<RequestManager, RequestRecordDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "Requests.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private RequestRecord requestRecord;
        [SerializeField] private AnimalCatalogue animalCatalogue;

        #endregion

        #region Save/Load

        protected override RequestRecordDTO Serialize()
        {
            return new RequestRecordDTO(requestRecord);
        }

        protected override void Deserialize(RequestRecordDTO dto)
        {
            foreach (RequestDTO request in dto.currentRequests)
            {
                Animal animal = animalCatalogue.FindByGuid(request.animal.guid);
                UnityEngine.Debug.Assert(animal != null, $"Could not find animal with guid {request.animal.guid}.");

                if (animal != null)
                {
                    AnimalRuntime animalRuntime = new AnimalRuntime(animal);
                    animalRuntime.InitializeComponents(animal);
                    animalRuntime.LoadComponents(request.animal.components);
                    requestRecord.AddRequest(new Request(animalRuntime));
                }
            }
        }

        protected override void SetDefaultValues()
        {
        }

        #endregion

        #region Callbacks

        public void OnAcceptRequest(Request request)
        {
            requestRecord.AcceptRequest(request);
        }

        public void OnDeclineRequest(Request request)
        {
            requestRecord.RemoveRequest(request);
        }

        #endregion
    }
}
