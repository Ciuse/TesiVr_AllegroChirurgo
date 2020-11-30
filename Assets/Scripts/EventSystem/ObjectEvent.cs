using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Events/ObjectEvent")]
    public class ObjectEvent : ScriptableObject
    {
        private List<InteractableEventListener> listeners = new List<InteractableEventListener>();

        public void Raise(Interactable interactable)
        {
            for (var i = listeners.Count -1; i >= 0 ; i--)
            {
                listeners[i].OnEventRaised(interactable);
            }
        }

        public void RegisterListener(InteractableEventListener listener)
        {
            listeners.Add(listener);
        }
        
        public void UnregisterListener(InteractableEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
