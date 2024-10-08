﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    #region Consts
    private const float SMOOTH_TIME = 0.3f;
    #endregion

    #region Public Properties
    public bool LockX;
    public bool LockY;
    public bool LockZ;
    public bool useSmoothing;
    public Transform target;
    public float speed;
    #endregion

    #region Private Properties
    private Transform thisTransform;
    private Vector3 velocity;
    #endregion

    private void Awake()
    {
        thisTransform = transform;

        velocity = new Vector3(0.5f, 0.5f, 0.5f);
    }
    
    private void FixedUpdate()
    {
        var newPos = Vector3.zero;

        if (useSmoothing)
        {
            newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
            newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
            newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z, ref velocity.z, SMOOTH_TIME);
        }
        else
        {
            newPos.x = target.position.x;
            newPos.y = target.position.y;
            newPos.z = target.position.z;
        }

        #region Locks
        if (LockX)
        {
            newPos.x = thisTransform.position.x;
        }

        if (LockY)
        {
            newPos.y = thisTransform.position.y;
        }

        if (LockZ)
        {
            newPos.z = thisTransform.position.z;
        }
        #endregion

        transform.position = Vector3.Slerp(transform.position, newPos, Time.deltaTime * speed);
    }
}
