using Celeste.Components;

namespace Rover.Core.Components
{
    public interface ITimer
    {
        long GetEndTimestamp(Instance instance);
        
        long GetRemainingTime(Instance instance);
        float GetRemainingTimeRatio(Instance instance);

        long GetElapsedTime(Instance instance);
        float GetElapsedTimeRatio(Instance instance);
    }
}
