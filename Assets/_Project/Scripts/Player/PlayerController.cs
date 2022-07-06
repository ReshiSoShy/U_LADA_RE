using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ReshiSoShy.Main.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        GameObject _player;
        Movement _movement;
        GraphicsRotator _rotator;
        [Header("Movement")]
        [SerializeReference]
        Transform _movementRef;
        [SerializeField]
        float _walkSpeed;
        [SerializeField]
        float _runMultiplier;
        Vector3 _movementVector = Vector3.zero;
        Vector3 _inputVector;
        bool _runKeyBeingHold = false;
        bool _interactKeyPressed = false;
        [Header("Interactions")]
        InteractionsSelector _interactionsSelector;
        [SerializeField]
        Animator _anim;
        private void Awake()
        {
            _movement = _player.GetComponent<Movement>();
            _rotator = _player.GetComponent<GraphicsRotator>();
            _interactionsSelector = _player.GetComponent<InteractionsSelector>();
        }
        public void Update()
        {
            _inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            _runKeyBeingHold = Input.GetButton("Run");
            _interactKeyPressed = Input.GetButtonUp("Interact");
            CalculateMovementVectorFromInput();
            Move();
            if (_movementVector != Vector3.zero)
            {
                StopAllCoroutines();
                _rotator.RotateTowards(_movementVector);
            }
            SetAnimations();
            if (_movementVector != Vector3.zero)
            {
               _interactionsSelector.ActiveSelectionUpdate();
            }
            if (_interactKeyPressed)
            {
                _anim.SetTrigger("Interact");
                StopAllCoroutines();
                _interactionsSelector.Lock();
                _interactionsSelector.Interact();
                StartCoroutine(RotationTowardsInteraction());
            }
        }
        [SerializeField]
        float _rotationDuration;
        IEnumerator RotationTowardsInteraction()
        {
            float currentTime = 0.00f;
            var target = _interactionsSelector.GetCurrentSelectedTarget();
            if(target != null)
            {
                var dir = target.position - _player.transform.position;
                Debug.DrawRay(transform.position, dir.normalized * 3, Color.magenta);
                dir.y = 0;
                while (currentTime < _rotationDuration)
                {
                    _rotator.RotateTowards(dir.normalized);
                    currentTime += Time.deltaTime;
                    yield return null;
                }
            }
        }
        void CalculateMovementVectorFromInput()
        {
            var forward = _movementRef.forward;
            var right = _movementRef.right;
            forward.y = 0;
            right.y = 0;
            _movementVector = _inputVector.x * right + _inputVector.y * forward;
            _movementVector.Normalize();
        }
        void Move()
        {
            var currentSpeed = _runKeyBeingHold ? _walkSpeed * _runMultiplier : _walkSpeed;
            _movement.Move(currentSpeed * Time.deltaTime * _movementVector);
        }
        [SerializeField]
        float _walkRunLerpValue = 0.4f;
        public void SetAnimations()
        {
            var speed = _movementVector != Vector3.zero ? _runKeyBeingHold ? 2: 1 : 0;
            _anim.SetFloat("WalkSpeed", speed );
        }
        public Vector3 GetInputVector()
        {
            return _movementVector;
        }
    }
}
