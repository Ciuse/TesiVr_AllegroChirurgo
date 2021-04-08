using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public abstract class DynamicObjectAbstract : MonoBehaviour

    {
        public ObjectEvent objectToResetEvent;
        
        public bool hasInteract;
        [HideInInspector]
        public Quaternion defaultRotation;
        [HideInInspector]
        public Vector3 defaultPosition;
        [HideInInspector]
        public int gameObjectHash;

        public void StartHash()
        {
            gameObjectHash = gameObject.GetHashCode();
        }

        public abstract void SaveState();


        public abstract void ResetState();



        public void Interact()
        {
            StartHash();
            if (!hasInteract)
            {
                SaveState();
                Interactable interactable = new Interactable {interactedObject = gameObject, id = GetHashCode()};
                objectToResetEvent.Raise(interactable);
            }
        }

        public void InteractWithoutSaving()
        {
            StartHash();
            if (!hasInteract)
            {
                Interactable interactable = new Interactable {interactedObject = gameObject, id = GetHashCode()};
                objectToResetEvent.Raise(interactable);
            }
        }


        public void SaveStatePosition()
        {
            Transform gameObjectTransform = gameObject.transform;
            defaultRotation = new Quaternion(gameObjectTransform.rotation.x,gameObjectTransform.rotation.y,gameObjectTransform.rotation.z,gameObjectTransform.rotation.w);;
            defaultPosition = new Vector3(gameObjectTransform.position.x,gameObjectTransform.position.y,gameObjectTransform.position.z);
                
        }
        public void ResetStatePosition()
        {
            gameObject.transform.position = new Vector3(defaultPosition.x,defaultPosition.y,defaultPosition.z);
            gameObject.transform.rotation = new Quaternion(defaultRotation.x,defaultRotation.y,defaultRotation.z, defaultRotation.w);
            print("pos resettata");
        }

        public void SetHasInteract()
        {
            hasInteract = true;
        }
        
        public void  ResetHasInteracted()
        {
            hasInteract = false;
        }

    }
