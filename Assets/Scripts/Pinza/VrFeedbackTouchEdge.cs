using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class VrFeedbackTouchEdge : MonoBehaviour
{
    public GameEvent pinzaTouchElectricEdge;
    public GameEvent pinzaStopTouchElectricEdge;
    public int countCollision;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
           
            if (countCollision == 0)
            {
                pinzaTouchElectricEdge.Raise();
            }
            countCollision++;
        }
            
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
            countCollision--;
            if (countCollision == 0)
            {
                pinzaStopTouchElectricEdge.Raise();
            }
        }

       

    }

}
