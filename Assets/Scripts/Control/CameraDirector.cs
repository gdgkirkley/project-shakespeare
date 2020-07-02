using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Shakespeare.Control
{
    public class CameraDirector : MonoBehaviour
    {
        CinemachineBrain brain;
        [SerializeField] CinemachineFreeLook followCamera;
        [SerializeField] CinemachineVirtualCamera playerDialogueCamera;
        [SerializeField] CinemachineVirtualCamera sourceDialogueCamera;

        private void Awake()
        {
            brain = GetComponent<CinemachineBrain>();
        }

        public void DialogueInit(Transform player, Transform source)
        {
            followCamera.gameObject.SetActive(false);
            playerDialogueCamera.Follow = player;
            playerDialogueCamera.LookAt = player;
            playerDialogueCamera.transform.position = player.transform.forward;
            sourceDialogueCamera.Follow = source;
            sourceDialogueCamera.LookAt = source;
            sourceDialogueCamera.transform.position = new Vector3(source.forward.x - 10f, source.forward.y, source.forward.z);
        }

        public void OnSourceDialogue()
        {
            playerDialogueCamera.gameObject.SetActive(false);
            sourceDialogueCamera.gameObject.SetActive(true);
        }

        public void OnPlayerDialogue()
        {
            playerDialogueCamera.gameObject.SetActive(true);
            sourceDialogueCamera.gameObject.SetActive(false);
        }

        public void EndDialogue()
        {
            followCamera.gameObject.SetActive(true);
            playerDialogueCamera.gameObject.SetActive(false);
            sourceDialogueCamera.gameObject.SetActive(false);
        }
    }

}