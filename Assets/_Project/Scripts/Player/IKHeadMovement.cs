using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Player
{
    public class IKHeadMovement : MonoBehaviour
    {
        Animator _anim;
        public bool IsActive = true;
        [SerializeField]
        Transform _fowardRef;
        [SerializeField]
        float _headMovementLerp = 4;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }
        Vector3 currentTarget;
        [SerializeField]
        float _ikWeightValue;
        private void OnAnimatorIK(int layerIndex)
        {
            if (_anim)
            {
                if (IsActive)
                {
                    if (_fowardRef != null && _target == null)
                    {
                        _anim.SetLookAtWeight(_ikWeightValue);
                        _anim.SetLookAtPosition(_fowardRef.position + _fowardRef.forward * 10);
                    }
                    else if (_target != null)
                    {
                        _anim.SetLookAtWeight(1);
                        currentTarget = Vector3.Lerp(currentTarget, _target.position, Time.fixedDeltaTime * _headMovementLerp);
                        _anim.SetLookAtPosition(currentTarget);
                    }
                }
            }
        }
        Transform _target = null;
        public void LookAtT(Transform target)
        {
            _target = target;
        }
        public void FreeHead()
        {
            _target = null;
        }
    }
}
