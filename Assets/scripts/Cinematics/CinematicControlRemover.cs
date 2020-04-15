using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
       private GameObject player;
       /*  private PlayerController control;
        private GameObject player; */

        private void Start()
        {
            GetComponent<PlayableDirector>().played += controlStop;
            GetComponent<PlayableDirector>().stopped += controlEnable;
            player = GameObject.FindWithTag("Player");
        }

        public void controlStop(PlayableDirector director)
        {
            player.GetComponent<ActionScheduler>().straigthStopAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        public void controlEnable(PlayableDirector director)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

    }
}