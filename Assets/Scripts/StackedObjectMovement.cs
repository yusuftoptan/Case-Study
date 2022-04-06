using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedObjectMovement : MonoBehaviour
{
    private Transform _objectToFollow;
    private bool _follow = false;
    private float _gap;

    private void FixedUpdate()
    {
        if (!_follow || _objectToFollow == null)
            return;


        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, _objectToFollow.position.x, Time.fixedDeltaTime * 15),
            _objectToFollow.position.y,
            _objectToFollow.position.z + _gap
            );

    }

    public void FollowQueue(Transform lastObjectInQueue, float gapBetween)
    {
        _objectToFollow = lastObjectInQueue;
        _gap = gapBetween;
        _follow = true;
    }

    public void StopFollowingQueue()
    {
        _follow = false;
    }
}
