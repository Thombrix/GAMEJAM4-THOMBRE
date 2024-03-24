using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PowerUpManager;

public class GrabMechanic : MonoBehaviour
{
    public float grabHeight;

    private GameObject heldItem;
    private GameObject nearest;

    private List<GameObject> GetGrabables()
    {
        return GameObject.FindGameObjectsWithTag("GRABABLE").ToList();
    }

    private List<GameObject> GetUnGrabables()
    {
        return GameObject.FindGameObjectsWithTag("Goober").ToList().Where(go => go != this.gameObject).ToList();
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

    private void Start()
    {
        grabHeight = 2.5f;
    }

    private void FixedUpdate()
    {
        PowerUpManager.PowerUp powerUp = GetComponent<PowerUpManager>().GetActivePowerUp();

        if(powerUp == PowerUpManager.PowerUp.GRAB)
        {
            Debug.Log("POWER UP GRAB");
            nearest = FindNearestObject(GetUnGrabables());
            if (nearest == null)
            {
                nearest = FindNearestObject(GetGrabables());
            }
        }
        else
        {
            nearest = FindNearestObject(GetGrabables());

        }
    }

    public void Grab()
    {
        if(heldItem == null && nearest != null)
        {
            nearest.transform.position = new Vector3(transform.position.x, transform.position.y + grabHeight, transform.position.z);
            heldItem = nearest;
        }
        else if(heldItem != null)
        {
            //heldItem.GetComponentInParent<Collider>().enabled = false;
            if (GetComponent<PowerUpManager>().GetActivePowerUp() == PowerUpManager.PowerUp.GRAB)
                heldItem.GetComponentInParent<Rigidbody>().GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

            else
            {
                heldItem.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            }
            heldItem = null;
        }
    }

    private void Update()
    {
        if (heldItem != null)
        {
            heldItem.transform.position = new Vector3(transform.position.x, transform.position.y + grabHeight, transform.position.z);
        }
    }

    
}
