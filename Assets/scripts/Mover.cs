using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            moveOnCursor();
        movementAnimation();
    }

    private void moveOnCursor()
    {
        /*A ray is basically (a ray) from an specified origin
        to a specified position*/
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        /*we can analyze the ray like(where it hits,position hits,the surface he hits etc..
        thanks to the ray cast hit*/
        RaycastHit raycastHit; 
        /*by using the raycast we cast just store the information about our ray
        in our raycast variable*/
        bool hasHit = Physics.Raycast(ray,out raycastHit);
        if(hasHit)
            GetComponent<NavMeshAgent>().SetDestination(raycastHit.point);
    }

    private void movementAnimation()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        /*transform a passed velocity from global to local
        and its useful using the animator because it just cares
        about the local velocity to work*/
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        /*we use the z axis base on movement animation*/
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("speed",speed);
    }


}
