using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReshiSoShy.Main;
namespace ReshiSoShy.Main.Player
{
    [ExecuteAlways]
    public class InteractionsRadar : MonoBehaviour
    {
        [SerializeField]
        float m_radius;
        [SerializeField]
        [Range(0,1)]
        float m_dotProductThreshold;
        [SerializeField]
        Transform _forwardRef;
        public List<GameObject> ScanForInteractions()
        {
            var inRangeInteraction = new List<GameObject>();
            var results = Physics.OverlapSphere(transform.position + transform.up * m_radius, m_radius);
            foreach (Collider hit in results)
            {
                var hasInteraction = hit.GetComponent<IInteractable>() != null;
                if (hasInteraction)
                {
                    var dirToInteraction = hit.transform.position - transform.position;
                    dirToInteraction.y = 0;
                    var closeToForwardValue = Vector3.Dot(_forwardRef.forward, dirToInteraction.normalized);
                    var isInFront = closeToForwardValue >= m_dotProductThreshold;
                    if (isInFront)
                    {
                        inRangeInteraction.Add(hit.gameObject);
                    }
                }
            }
            return inRangeInteraction;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.up * m_radius, m_radius);
        }
    }
}
