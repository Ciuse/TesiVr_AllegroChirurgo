using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace EventSystem2
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Events/Event")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (var i = listeners.Count -1; i >= 0 ; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }
        
        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}