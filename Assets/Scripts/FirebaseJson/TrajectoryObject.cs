﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryObject
{
    public int id; //da rimuovere
    public TimeSpan duration;
    public List<Vector3> positionList = new List<Vector3>();
    public List<Quaternion> rotationList = new List<Quaternion>();


}
