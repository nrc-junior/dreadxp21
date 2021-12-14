using UnityEngine;
using UnityEngine.AI;

// https://youtu.be/CHV1ymlw-P8?t=390

public class PlayerNpc : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            print('a');
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            print(hit.point);
            // agent.SetDestination(hit.point);


            if (Physics.Raycast(ray, out hit))
            {
                print('b');
                agent.SetDestination(hit.point);
            }
        }
    }
}
