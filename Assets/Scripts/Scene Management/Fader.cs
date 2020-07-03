using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentlyActiveFade = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public Coroutine Fade(float time, float target)
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }

            currentlyActiveFade = StartCoroutine(FadeRoutine(time, target));
            return currentlyActiveFade;
        }

        private IEnumerator FadeRoutine(float time, float target)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(time, 0);
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(time, 1);
        }
    }

}