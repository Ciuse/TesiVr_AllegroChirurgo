using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonObject
{
    public int idObject { get; set; }
    public int executionNumber { get; set; }
    public int numberOfErrorPinza { get; set; }
    public int numberOfErrorObject { get; set; }
    public DateTime startTime  { get; set; }
    public DateTime endTime  { get; set; }
    public List<TrackObject> TrackObjects = new List<TrackObject>();
}
