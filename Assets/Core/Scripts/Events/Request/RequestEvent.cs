using System;
using UnityEngine;
using UnityEngine.Events;
using Celeste.Events;
using Rover.Core.Objects;

namespace Rover.Core
{
	[Serializable]
	public class RequestUnityEvent : UnityEvent<Request> { }
	
	[Serializable]
	[CreateAssetMenu(fileName = nameof(RequestEvent), menuName = "Rover/Core/Events/Request/Request Event")]
	public class RequestEvent : ParameterisedEvent<Request> { }
	
	[Serializable]
	public class GuaranteedRequestEvent : GuaranteedParameterisedEvent<RequestEvent, Request> { }
}
