﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovablePlatform : MonoBehaviour
{
	void Start ()
    {
        if(_pathFlow.Count == 0)
        {
            AddPathNode();
        }
        gameObject.transform.position = _pathFlow[0];
        StartCoroutine(FollowPath());
        Messenger.AddListener<GameObject>("Chilled", OnCoolToggle);
	}
    
    public void AddPathNode()
    {
        _pathFlow.Add(gameObject.transform.position);
    }

    private void OnCoolToggle(GameObject go) //*
    {
        if(go == gameObject)
        {
            if (_chilled)
            {
                _chilled = false;
            }

            else
            {
                _chilled = true;
            }
        }
        
    }

    IEnumerator FollowPath()
    {
        if (_pathFlow.Count == _count)
        {
            _count = 0;
        }

        while(Vector3.Distance(transform.position, _pathFlow[_count]) > 0.3f)
        {
            transform.position = Vector3.Lerp(transform.position, _pathFlow[_count], _speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(_platformDelay);

        StopCoroutine(FollowPath());
        _count++;
        StartCoroutine(FollowPath());
    }

    [SerializeField]
    private List<Vector3> _pathFlow;

    private int _count = 0;

    [SerializeField]
    private float _platformDelay;
    [SerializeField]
    private float _speed;

    private bool _chilled = false; //*
}

///
/// Zac King
///