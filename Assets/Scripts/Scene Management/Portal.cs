using System.Collections;
using System.Collections.Generic;
using Shakespeare.Control;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Shakespeare.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneIndex = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        GameObject player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneIndex < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }


            Fader fader = FindObjectOfType<Fader>();

            DontDestroyOnLoad(gameObject);

            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneIndex);

            PlayerController newPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

            newPlayer.GetComponent<ActionScheduler>().CancelCurrentAction();
            newPlayer.GetComponent<PlayerController>().enabled = false;

            Portal otherPortal = GetOtherPortal();

            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);

            fader.FadeIn(fadeInTime);

            newPlayer.GetComponent<PlayerController>().enabled = true;

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this || portal.destination != destination) continue;

                return portal;
            }

            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}