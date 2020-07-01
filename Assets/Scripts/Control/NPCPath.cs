using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Control
{
    public class NPCPath : MonoBehaviour
    {
        [SerializeField] float waypointSize = 0.1f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 childPosition = GetWayPoint(i);
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(childPosition, waypointSize);
                Gizmos.DrawLine(childPosition, GetWayPoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i == (transform.childCount - 1))
            {
                return 0;
            }
            else
            {
                return i + 1;
            }
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNumberOfWayPoints()
        {
            return transform.childCount;
        }
    }
}
