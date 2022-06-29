using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Cameras
{
    public class ObstacleAvoidance : MonoBehaviour
    {
        [SerializeField]
        Transform _camera;
        [SerializeField]
        float _collisionDetectionRadius;
        [SerializeField]
        float _lerpValue;
        [SerializeField]
        Vector3 _camInitialPos;

        private void Update()
        {
            RaycastHit hit;
            var distance = Vector3.Distance(transform.position, _camera.position) ;
            var result = Physics.SphereCast(transform.position, _collisionDetectionRadius, (_camera.position - transform.position).normalized, out hit, distance);
            var currentPos = _camera.transform.localPosition;
            Vector3 nextPos = currentPos;
            if (result)
            {
                nextPos = Vector3.Lerp(currentPos, Vector3.zero, _lerpValue * Time.deltaTime);
            }
            else
            {
                var dir = _camera.position - transform.position;
                result = Physics.Raycast(transform.position, dir, out hit, 3.5f);
                if(!result)
                    nextPos = Vector3.Lerp(currentPos, _camInitialPos, _lerpValue * Time.deltaTime);
            }
            _camera.transform.localPosition = nextPos;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_camera.position, _collisionDetectionRadius);
        }
    }
}
