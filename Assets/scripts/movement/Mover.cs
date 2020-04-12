using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction
    {
        public NavMeshAgent navMesh;
        private Animator animator;
        private bool canceler = true;
    

        private void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            movementAnimation();
        }

        private void movementAnimation()
        {
            Vector3 velocity = navMesh.velocity;
            /*transform a passed velocity from global to local
            and its useful using the animator because it just cares
            about the local velocity to work*/
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            /*we use the z axis base on movement animation*/
            float speed = localVelocity.z;
            animator.SetFloat("speed", speed);
        }

        public void startMoving(Vector3 destination, float range)
        {
            navMesh.SetDestination(destination);
            navMesh.stoppingDistance = range;
        }

        public void startMoveAction(Vector3 destination,float range)
        {
            GetComponent<ActionScheduler>().startAction(this);
            startMoving(destination,range);
        }

        public void stopExecution()
        {
            navMesh.ResetPath();
        }
    }
}