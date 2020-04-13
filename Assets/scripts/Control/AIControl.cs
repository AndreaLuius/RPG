using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIControl : MonoBehaviour
    {
        [SerializeField] float chaseTargetRange = 10f;
        [SerializeField] float suspiciosTime = 4f;
        [SerializeField] PatrolPath patrol;
        private GameObject player;
        private Fighter fighter;
        private Healt healt;
        private Mover mover;
        private ActionScheduler scheduler;
        private Vector3 ownBaseLocation;
        private float timeSinceLastChase = Mathf.Infinity;
        private Vector3 nextPosition;
        int waypointCounter = 0;


        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            healt = GetComponent<Healt>();
            mover = GetComponent<Mover>();
            scheduler = GetComponent<ActionScheduler>();
            /*setting up the base enemy location "memory"*/
            ownBaseLocation = transform.position;
            nextPosition = ownBaseLocation;
        }

        private void Update() {

            if(healt.isDead) return;

            if(chaseDistanceControl() < chaseTargetRange)
            {
                //when start attack the time get reset
                fighter.startAttack(player);
                timeSinceLastChase = 0;
            }
            else
            {
                //checking for the suspicios behavior
                if(timeSinceLastChase < suspiciosTime)
                    scheduler.straigthStopAction();
                else
                {
                    patrolControl();
                }
                    
            }

            timeSinceLastChase += Time.deltaTime;
        }

        private void patrolControl()
        {
            if(patrol != null)
            {
                if(Vector3.Distance(transform.position, nextPosition) < .3f)
                {
                    nextPosition = patrol.transform.GetChild(waypointCounter).position;
                    waypointCounter++;
                }
                
                if(waypointCounter == patrol.transform.childCount)
                     waypointCounter = 0;

                mover.startMoveAction(nextPosition, 0f);
            }
        }

        private float chaseDistanceControl()
        {
            return Vector3.Distance(transform.position,
                     player.transform.position);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseTargetRange);
        }
    }
}