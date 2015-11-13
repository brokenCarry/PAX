﻿using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour
{
    public string listeningFor;
    Transform trigger;
    bool locked = false;

    void Awake()
    {
        Messenger.AddListener<GameObject, Transform>(listeningFor, Activate);

        //grab the game object in the SlidingDoor hierarchy with the 'CameraBroadcast' component
        trigger = transform.root.GetComponentInChildren<CameraBroadcast>().gameObject.transform;
        //trigger = t.name;
        //BoxCollider[] children = GetComponentsInParent<BoxCollider>();
        //foreach (Collider c in children)
        //{
        //    if (c.isTrigger == true)
        //    {
        //        activator = c.name;
        //        return;
        //    }
        //}
    }

    void Activate(GameObject o,Transform broadcaster)
    {
        if(o.tag == "Player" && locked == false && broadcaster == trigger)
        {
            transform.position += new Vector3(0, 5, 0);
            locked = true;
        }
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<GameObject, Transform>(listeningFor, Activate);
    }
}
