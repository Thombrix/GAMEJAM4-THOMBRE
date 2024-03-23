using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabNearestObject : MonoBehaviour
{
    public float grabHeight = 10.0f;
    private GameObject heldItem;
    private GameObject nearest;

    private List<GameObject> GetGrabables()
    {
        return GameObject.FindGameObjectsWithTag("GRABABLE").ToList();
    }

    private List<GameObject> GetUnGrabables()
    {
        return GameObject.FindGameObjectsWithTag("UNGRABABLE").ToList().Where(go => go != this.gameObject).ToList();
    }

    public GameObject FindNearestObject(List<GameObject> gameObjects)
    {
        GameObject nearestObject = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject go in gameObjects)
        {
            float distance = Vector3.Distance(go.transform.position, currentPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = go;
            }
        }

        return nearestObject;
    }

    private void FixedUpdate()
    {
        // Get nearest grabable object
        nearest = FindNearestObject(GetGrabables());

        // define order by looking up power up
    }

    private void Update()
    {
        // If input for grab, 


        if (Input.GetKeyDown(KeyCode.E) && heldItem == null)
        {
            if (nearest != null)
            {
                nearest.transform.position = new Vector3(transform.position.x, transform.position.y + grabHeight, transform.position.z);
                heldItem = nearest;

                
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && heldItem != null)
        {
            heldItem.GetComponent<Rigidbody>().AddForce(transform.forward * 10000);
            heldItem = null;
        }

        if (heldItem != null)
        {
            heldItem.transform.position = new Vector3(transform.position.x, transform.position.y + grabHeight, transform.position.z);
        }
    }

    
}
