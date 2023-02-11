using Celeste.DataStructures;
using Rover.Core.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rover.Core.Record
{
    [CreateAssetMenu(fileName = nameof(RequestRecord), menuName = "Rover/Core/Record/Request Record")]
    public class RequestRecord : ScriptableObject
    {
        #region Properties and Fields

        public int NumPendingRequests => pendingRequests.Count;

        [SerializeField] private AnimalRecord animalRecord;
        [SerializeField] private Celeste.Events.Event onChange;

        [NonSerialized] private List<Request> pendingRequests = new List<Request>();

        #endregion

        public Request GetRequest(int index)
        {
            return pendingRequests.Get(index);
        }

        public void AddRequest(Request request)
        {
            pendingRequests.Add(request);
            onChange.Invoke();
        }

        public void RemoveRequest(int index)
        {
            if (index < NumPendingRequests)
            {
                pendingRequests.RemoveAt(index);
                onChange.Invoke();
            }
        }

        public void RemoveRequest(Request request)
        {
            int requestIndex = pendingRequests.IndexOf(request);
            RemoveRequest(requestIndex);
        }

        public bool CanAcceptRequest(int index)
        {
            return !animalRecord.HasReachedAnimalCapacity;
        }

        public void AcceptRequest(int index)
        {
            if (index < NumPendingRequests)
            {
                Request request = pendingRequests[index];
                animalRecord.AddAnimal(request.Animal);
                RemoveRequest(index);
            }
        }

        public void AcceptRequest(Request request)
        {
            int requestIndex = pendingRequests.IndexOf(request);
            AcceptRequest(requestIndex);
        }
    }
}
