using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimization : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] bgs;
    public GameObject[] pipes;
    public GameObject bird;
    void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.SetActive(false);
        }
        for(int i = 2; i < bgs.Length; i++)
        {
            bgs[i].gameObject.SetActive(false);
        }
        for (int i = 1; i < pipes.Length; i++)
        {
            pipes[i].gameObject.SetActive(false);
        }
        InvokeRepeating("TurnOnOffThings", 1f, 0.1f);
    }


    public void TurnOnOffThings()
    {
        for (int i = 0; i < bgs.Length; i++)
        {
            if (bgs[i] != null && bgs[i].activeInHierarchy && bgs[i].transform.position.x < bird.transform.position.x - 500f)
            {
                Destroy(bgs[i]);
            }
            else if (bgs[i] != null && !bgs[i].activeInHierarchy && bgs[i].transform.position.x > bird.transform.position.x && bgs[i].transform.position.x < bird.transform.position.x + 700f)
            {
                bgs[i].gameObject.SetActive(true);

            }
        }
        for (int i = 0; i < pipes.Length; i++)
        {
            if (pipes[i] != null && pipes[i].activeInHierarchy && pipes[i].transform.position.x < bird.transform.position.x - 500f)
            {
                Destroy(pipes[i]);
            }
            else if (pipes[i] != null && !pipes[i].activeInHierarchy && pipes[i].transform.position.x > bird.transform.position.x && pipes[i].transform.position.x < bird.transform.position.x + 700f)
            {
                pipes[i].gameObject.SetActive(true);
            }
        }        
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] != null && items[i].activeInHierarchy && items[i].transform.position.x < bird.transform.position.x - 50f)
            {
                Destroy(items[i]);
            }
            else if (items[i] != null && !items[i].activeInHierarchy && items[i].transform.position.x > bird.transform.position.x && items[i].transform.position.x < bird.transform.position.x + 200f )
            {
                items[i].gameObject.SetActive(true);
            }
        }        
    }
}
