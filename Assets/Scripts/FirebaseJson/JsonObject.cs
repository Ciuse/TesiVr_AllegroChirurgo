using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public class JsonObject
{
    public int idObject;
    public int executionNumber;
    public int numberOfErrorPinza;
    public int numberOfErrorObject;
    
    public List<int> wrongObjectPicked = new List<int>();
    [NonSerialized]
    public DateTime startTime;
    [NonSerialized]
    public DateTime endTime;

    public int duration;
    
    public List<TrajectoryObject> trajectoryList = new List<TrajectoryObject>();
    
    
}
