using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Transform parentScreen;
    public Transform spawnPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Screen"))
        {
            Debug.Log("Entered");
            //GameObject go = Instantiate(other.transform.parent.gameObject);
            //go.transform.parent = parentScreen;
            //go.transform.position = spawnPos.position;
            other.transform.parent.transform.position = spawnPos.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Screen"))
        {
            //Destroy(other.transform.parent.gameObject);
        }
    }
}
