using UnityEngine;
using Cinemachine;

namespace Shakespeare.Control
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] GameObject freeLookCamera;
        [SerializeField] float maxScrollTime = 1f;
        CinemachineFreeLook freeLookComponent;
        PlayerController playerController;

        private bool isScrolling;
        private float timeSinceScroll;

        private void Awake()
        {
            freeLookComponent = freeLookCamera.GetComponent<CinemachineFreeLook>();
            playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            timeSinceScroll += Time.deltaTime;
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(1))
            {
                // if(playerController.isDraggingUI) return;
                freeLookComponent.m_XAxis.m_MaxSpeed = 500;
            }
            if (Input.GetMouseButtonUp(1))
            {
                freeLookComponent.m_XAxis.m_MaxSpeed = 0;
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                freeLookComponent.m_YAxis.m_MaxSpeed = 10;
                timeSinceScroll = 0;
            }
            if (timeSinceScroll >= maxScrollTime && Input.mouseScrollDelta.y == 0)
            {
                freeLookComponent.m_YAxis.m_MaxSpeed = 0;
            }
        }

        // TODO implement smooth scroll.
        public void SetConversationMode()
        {
            freeLookComponent.m_YAxis.Value = 0.05f;
        }
    }

}