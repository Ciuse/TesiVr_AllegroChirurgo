using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class VR_Training : MonoBehaviour
{
   public GameEvent startingTraining;

   public void StartTraining()
   {
      startingTraining.Raise();
   }
}
