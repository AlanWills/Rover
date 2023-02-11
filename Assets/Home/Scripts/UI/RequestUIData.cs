using Rover.Core.Objects;

namespace Rover.Home.UI
{
    public class RequestUIData
    {
        #region Properties and Fields

        public Request Request { get; }
        public string Name => Request.DisplayName;

        #endregion

        public RequestUIData(Request request)
        {
            Request = request;
        }
    }
}
