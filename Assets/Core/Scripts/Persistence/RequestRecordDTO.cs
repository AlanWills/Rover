using Rover.Core.Record;
using System;
using System.Collections.Generic;

namespace Rover.Core.Persistence
{
    [Serializable]
    public class RequestRecordDTO
    {
        public List<RequestDTO> currentRequests = new List<RequestDTO>();

        public RequestRecordDTO(RequestRecord requestRecord)
        {
            currentRequests.Capacity = requestRecord.NumPendingRequests;

            for (int i = 0, n = requestRecord.NumPendingRequests; i < n; ++i)
            {
                currentRequests.Add(new RequestDTO(requestRecord.GetRequest(i)));
            }
        }
    }
}
