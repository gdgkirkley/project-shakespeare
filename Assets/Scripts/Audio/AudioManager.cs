using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager manager;

        private void Awake()
        {
            if (manager != null && manager != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                manager = this;
            }
        }
    }
}
