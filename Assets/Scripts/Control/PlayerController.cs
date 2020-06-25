using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Shakespeare.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float maxPathLength = 40f;

        private void Update()
        {
            if (InteractWithUI()) return;

            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsActive() && EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            return false;
        }

        // Not hitting character? Make sure Use Gravity is turned off!
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();

            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();

                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            float[] distances = new float[hits.Length];

            int i = 0;

            foreach (RaycastHit hit in hits)
            {
                distances[i] = hit.distance;
                i++;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            Vector3 target = new Vector3();
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (!hasHit) return false;

            NavMeshHit navMeshHit;

            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);

            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            if (Input.GetMouseButton(0))
            {
                GetComponent<Mover>().StartMoveAction(target, 1f);
            }

            return true;
        }

        private static Ray GetMouseRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            return ray;
        }
    }
}