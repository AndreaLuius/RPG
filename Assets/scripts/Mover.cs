using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private Ray lastRay;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            moveOnCursor();
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
}
