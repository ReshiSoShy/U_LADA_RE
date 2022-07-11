using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReshiSoShy.Main.Dialogues;
namespace ReshiSoShy.Main.Player
{
    public class InteractionsSelector : MonoBehaviour
    {
        InteractionsRadar _interactionsRadar;
        List<GameObject> _availableInteractions = new();
        GameObject _currentSelection = null;
        int _currentIndex = 0;
        [SerializeField]
        Color _selectionColor;
        [SerializeField]
        float _selectionWidth;
        [SerializeField]
        Outline.Mode _outLineMode;
        [SerializeField]
        IKHeadMovement _headIk;
        private void Awake()
        {
            _interactionsRadar = GetComponent<InteractionsRadar>();
        }
        private void Update()
        {
            List<GameObject> updatedAvailableInteractions = _interactionsRadar.ScanForInteractions();
            if(updatedAvailableInteractions.Count == 0)
            {
                ClearSelectionOutput();
                Unlock();
                _headIk.FreeHead();
            }
            foreach (GameObject interaction in updatedAvailableInteractions)
            {
                if (!_availableInteractions.Contains(interaction))
                    _availableInteractions.Add(interaction);
            }
            List<GameObject> interactionsForRemoval = new();
            foreach (GameObject interaction in _availableInteractions)
            {
                if (!updatedAvailableInteractions.Contains(interaction))
                    interactionsForRemoval.Add(interaction);
            }
            foreach (GameObject interaction in interactionsForRemoval)
                _availableInteractions.Remove(interaction);
            m_timeSinceLastUpdate += Time.deltaTime;
            if (m_timeSinceLastUpdate >= m_inactiveTimeSelection)
            {
                m_timeSinceLastUpdate = 0.00f;
                PassiveSelectionUpdate();
            }
            if (_currentSelection != null)
            {
                Debug.DrawLine(transform.position, _currentSelection.transform.position, Color.magenta);
                var hasOutline = _currentSelection.GetComponent<Outline>();
                Outline outline = null;
                if (!hasOutline)
                    outline = _currentSelection.AddComponent<Outline>();
                else
                    outline = hasOutline;
                outline.OutlineMode = _outLineMode;
                outline.OutlineColor = _selectionColor;
                outline.OutlineWidth = _selectionWidth;
                _headIk.LookAtT(_currentSelection.transform);
            }
        }

        private void ClearSelectionOutput()
        {
            if (_currentSelection != null)
            {
                var hasOutline = _currentSelection.gameObject.GetComponent<Outline>();
                if (hasOutline != null)
                {
                    Destroy(obj: hasOutline);
                }
            }
            _currentSelection = null;
        }

        float m_timeSinceLastUpdate = 0.00f;
        float m_inactiveTimeSelection = 2.5f;
        public void ActiveSelectionUpdate()
        {
            if (_locked)
                return;
            m_timeSinceLastUpdate = 0.00f;
            GameObject closestInteraction = null;
            float closestDOT = -1;
            foreach (GameObject interaction in _availableInteractions)
            {
                var dirToInteraction = interaction.transform.position - transform.position;
                var distanceToInteraction = Vector3.Dot(transform.forward, dirToInteraction.normalized);
                if (distanceToInteraction > closestDOT)
                {
                    closestInteraction = interaction;
                    closestDOT = distanceToInteraction;
                }
            }
            if(closestInteraction != _currentSelection)
            {
                ClearSelectionOutput();
                _currentSelection = closestInteraction;
                _currentIndex = _availableInteractions.IndexOf(_currentSelection);
            }
        }
        void PassiveSelectionUpdate()
        {
            if (_locked)
                return;
            _currentIndex++;
            if (_currentIndex >= _availableInteractions.Count)
            {
                _currentIndex = 0;
            }
            if(_availableInteractions.Count != 0)
            {
                ClearSelectionOutput();
                _currentSelection = _availableInteractions[_currentIndex];
            }
        }
        public void Interact()
        {
            m_timeSinceLastUpdate = 0.00f;
            var hasInteraction = _currentSelection.GetComponent<IInteractable>();
            // some logic 
            // we choose to talk
            if (hasInteraction == null)
                return;
            //DialogueController.Instance.LoadNewDialogueLine(this.gameObject, _currentSelection, "hablar"); 
        }
        bool _locked = false;
        public void Lock()
        {
            _locked = true;
        }
        public void Unlock()
        {
            _locked = false;
        }
        public Transform GetCurrentSelectedTarget()
        {
            return _currentSelection.transform;
        }
    }
}
