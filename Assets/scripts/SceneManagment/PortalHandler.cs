using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

namespace RPG.SceneManagment
{
    public class PortalHandler : MonoBehaviour
    {
        enum PortalIdentifier
        {
            A,B,C,D,E,F,G
        }

        [SerializeField] Transform spawnPoint;
        [SerializeField] PortalIdentifier portalIdentifier;
        [SerializeField] int sceneToLoad = 0;
         private float waitPortalLoad = 1f;
        
        private void OnTriggerEnter(Collider other) 
        {
           if(other.tag == "Player")
           {
               StartCoroutine(changingScene(waitPortalLoad));
           } 
        }

        IEnumerator changingScene(float waitTime)
        {
            if(sceneToLoad < 0) yield break;

            /*we dont destroy the old object
            so that we can take the older portal*/
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.fadeOut(fader.fadeOutTime);

            //we load the scene 
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            PortalHandler portal = getPortal();
            
            updatePlayerPosition(portal);

            yield return new WaitForSeconds(.3f);

            yield return fader.fadeIn(fader.fadeInTime);
    
            Destroy(gameObject);

        }

        /*we gonna take the portal filtered*/
        private PortalHandler getPortal()
        {
            /*take all the available portals*/
            foreach(var portal in FindObjectsOfType<PortalHandler>())
            {
                /*we just filter the portals as we want*/
                if(portal == this) continue;
                /*using the portal to identify in wich portal to go*/
                if(portal.portalIdentifier != this.portalIdentifier) continue;

                return portal;
            }
            
            return null;
        }
        /*we just update the position of the player
            after the teleport in the right position (spawn point)*/
        private void updatePlayerPosition(PortalHandler portal)
        {
            /*getting the player object*/
            GameObject player = GameObject.FindWithTag("Player");
            /*we take the navMesh because if 
            you just change the position of the player
            sometimes it can bug apart, and just 
            let you spawn in a random position, wrapping
            the navmesh with the position we want to end up to
            we can avoid this problem*/
            player.GetComponent<NavMeshAgent>().Warp(portal.spawnPoint.position);
            /*the rotation doesnt break the navmesh*/
            player.transform.rotation = portal.spawnPoint.rotation;
        }
    }
}