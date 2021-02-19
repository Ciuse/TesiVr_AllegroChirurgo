using UnityEngine;

public class eventsManagement : MonoBehaviour
{


    public GameObject objectToPick;
    public BoxCollider colliderOverObject;
    private ObjectPinzabili objectPinzabili;


    private void Start()
    {
        objectPinzabili = objectToPick.GetComponent<ObjectPinzabili>();
    }

   
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectToPick.tag))
        {
            if (objectPinzabili.isActive)
            {
                objectToPick.GetComponent<MeshRenderer>().material.color=Color.yellow;
                print("Oggetto uscito dal collider");
            }else{
                objectToPick.GetComponent<MeshRenderer>().material.color=Color.red;
            }
        }
        

    }
}
