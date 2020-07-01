using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] NPCPath path;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float dwellTime = 3f;

        [Range(0, 1)]
        [SerializeField] float speed = 0.2f;

        GameObject player;
        Mover mover;
        Vector3 position;
        int currentWayPointIndex = 0;

        float timeAtWayPoint = Mathf.Infinity;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            mover = GetComponent<Mover>();
            position = GetPosition();
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }

        private void Update()
        {
            NPCBehaviour();
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeAtWayPoint += Time.deltaTime;
        }

        private void NPCBehaviour()
        {
            Vector3 nextPosition = position;

            if (path != null)
            {
                if (AtWaypoint())
                {
                    timeAtWayPoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWayPoint();
            }

            if (timeAtWayPoint > dwellTime)
            {
                // This also cancels the fight action
                mover.StartMoveAction(nextPosition, speed);
            }
        }

        private Vector3 GetCurrentWayPoint()
        {
            return path.GetWayPoint(currentWayPointIndex);
        }

        private void CycleWaypoint()
        {
            currentWayPointIndex = path.GetNextIndex(currentWayPointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWaypoint < waypointTolerance;
        }
    }

}