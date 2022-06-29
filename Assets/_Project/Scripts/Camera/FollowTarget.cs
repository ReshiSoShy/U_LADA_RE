using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Cameras
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField]
        Transform _target;
        [SerializeField]
        UpdateMode _updateMode;
        [SerializeField]
        float _lerpValue;
        [SerializeField]
        float _distance;
        [SerializeField]
        float _deathZone;
        private void Update()
        {
            if (_updateMode != UpdateMode.Update)
                return;
            Follow();
        }
        private void FixedUpdate()
        {
            if (_updateMode != UpdateMode.FixedUpdate)
                return;
            Follow();
        }
        private void LateUpdate()
        {
            if (_updateMode != UpdateMode.LateUpdate)
                return;
            Follow();
        }
        void Follow()
        {
            float deltaTime = _updateMode == UpdateMode.Update ? Time.deltaTime : _updateMode == UpdateMode.FixedUpdate ? Time.fixedDeltaTime : Time.deltaTime;
            Vector3 dirToTarget = _target.position - transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);
            Vector3 movementVector = Vector3.zero;
            if(distanceToTarget > _distance + _deathZone)
            {
                movementVector = dirToTarget;
            }
            else if(distanceToTarget < _distance)
            {
                movementVector = -dirToTarget;
            }
            var nextPos = Vector3.Lerp(transform.position, transform.position + movementVector, deltaTime * _lerpValue) ;
            transform.position = nextPos;
        }
    }
    public enum UpdateMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }
}
