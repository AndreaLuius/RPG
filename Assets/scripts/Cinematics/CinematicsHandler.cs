using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicsHandler : MonoBehaviour
    {
        private bool isExecuted = false;
        void OnTriggerEnter(Collider other)
        {
            /*just used to start the cinematic*/
            if(!isExecuted && other.transform.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();  
                isExecuted = true;
            }
        }
    }
}