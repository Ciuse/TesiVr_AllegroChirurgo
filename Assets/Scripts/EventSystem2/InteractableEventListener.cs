using System;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem2
{
    public class Interactable
    {
        public GameObject interactedObject;
        public int id;
    }
    
    [Serializable]
    public class InteractableEvent : UnityEvent<Interactable>
    {
    }
    
    public class InteractableEventListener : MonoBehaviour
    {
        public ObjectEvent Event;
        public InteractableEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Interactable interactable)
        {
            Response.Invoke(interactable);
        }
    }
}