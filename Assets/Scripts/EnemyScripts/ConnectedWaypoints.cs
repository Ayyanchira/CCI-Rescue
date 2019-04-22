using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedWaypoints : Waypoints
{
    [SerializeField] public float connectivityRadius = 50f;
    List<ConnectedWaypoints> connection;

    public void Start()
    {
        GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");

        connection = new List<ConnectedWaypoints>();

        for(int i=0;i<allWayPoints.Length; i++)
        {
            ConnectedWaypoints nextWaypoint = allWayPoints[i].GetComponent<ConnectedWaypoints>();
            
            if(nextWaypoint != null)
            {
                if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectivityRadius && nextWaypoint!=this)
                {
                    connection.Add(nextWaypoint);
                }
            }
        }
    }


    public ConnectedWaypoints NextWayPoint(ConnectedWaypoints previous, GameObject enemy)
    {
        if(connection.Count == 0)
        {
            Debug.LogError("Insufficient");
            return null;
        }
        else if(connection.Count==1 && connection.Contains(previous))
        {
            return previous;
        }
        else
        {
            ConnectedWaypoints next = null;
            int index = 0;

            do
            {
                index = UnityEngine.Random.Range(0, connection.Count);
                int i = 0;
                while(index < connection.Count)
                {
                    if(Vector3.Distance(connection[index].transform.position, enemy.transform.position) <= connectivityRadius)
                    {
                        next = connection[index];
                        break;
                    }
                    else
                    {
                        int temp = UnityEngine.Random.Range(0, connection.Count);
                        index = (temp == index) ? UnityEngine.Random.Range(0, connection.Count) : temp ;
                    }
                    i++;
                }   
            } while (next == previous);

            return next;
        }
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, connectivityRadius);
    }
}
