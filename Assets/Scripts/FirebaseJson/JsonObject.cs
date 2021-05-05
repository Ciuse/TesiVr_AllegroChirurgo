using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public class JsonObject
{
    public int idObject=-1;
    public int executionNumber=0;
    public int numberOfErrorPinza=0;
    public int numberOfErrorObject=0;
    public int numberOfWarningPinza=0;

    public List<int> wrongObjectPicked = new List<int>();
    [NonSerialized]
    public DateTime startTime;
    [NonSerialized]
    public DateTime endTime;

    public int duration;
    
    public List<TrajectoryObject> trajectoryList = new List<TrajectoryObject>();
    
    
}
