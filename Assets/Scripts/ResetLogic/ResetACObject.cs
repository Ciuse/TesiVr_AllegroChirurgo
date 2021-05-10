using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class ResetACObject : MonoBehaviour
{ 
    public List<GameObject> currentInteractedObject = new List<GameObject>();
    public List<DynamicObjectAbstract> currentDynamicObjects = new List<DynamicObjectAbstract>();
    public List<GameObject> allInteractedObject = new List<GameObject>();
    public List<DynamicObjectAbstract> allObjectDynamicObjects = new List<DynamicObjectAbstract>();
        
    public void AddInteractedObjectToCurrentList(Interactable interactable)
    {
        DynamicObjectAbstract dynamicObjectState = interactable.interactedObject.GetComponent<DynamicObjectAbstract>();
        dynamicObjectState.SetHasInteract();
        
        allInteractedObject.Add(interactable.interactedObject);
        currentInteractedObject.Add(interactable.interactedObject);

        currentDynamicObjects.Add(dynamicObjectState);
        allObjectDynamicObjects.Add(dynamicObjectState);

    }

    public void ResetRoom()
    {
        foreach(GameObject objToRes in currentInteractedObject)
        {
            foreach (DynamicObjectAbstract beforeState in currentDynamicObjects)
            {
                if (objToRes.GetHashCode() == beforeState.gameObjectHash)
                {
                    beforeState.ResetState();
                    print("non usato");
                }
            }
        }
        SaveRoomChanges();
    }
    public void ResetAllChanges()
    {                    

        foreach(GameObject objToRes in allInteractedObject)
        {

            foreach (DynamicObjectAbstract beforeState in allObjectDynamicObjects)
            {

                if (objToRes.GetHashCode() == beforeState.gameObjectHash)
                {
                    beforeState.ResetState();
                    print("non usato2");

                }
            }
        }
        SaveAllChanges();
    }

    public void SaveRoomChanges()
    {
        foreach (DynamicObjectAbstract objToRes in currentDynamicObjects)
        {
            objToRes.ResetHasInteracted();
        }
        currentInteractedObject.Clear();
        currentDynamicObjects.Clear();
    }

   
    public void SaveAllChanges()
    {
        foreach (DynamicObjectAbstract objToRes in allObjectDynamicObjects)
        {
            objToRes.ResetHasInteracted();
        }
        currentInteractedObject.Clear();
        allInteractedObject.Clear();
        currentDynamicObjects.Clear();
        allObjectDynamicObjects.Clear();
    }
    
    //todo creare un interactible object da cui tutti derivano e implementano le funzioni "AddToResetList" e "Resetting" e " Saving" in modo diverso
        
}
