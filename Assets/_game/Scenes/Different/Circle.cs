using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    public void ShowCircle()
    {
        Transform parentTransform = transform.parent;
        if (parentTransform !=null)
        {
            Transform[] children = parentTransform.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if(child.gameObject.GetComponent<SpriteRenderer>() !=null && child.gameObject.GetComponent<Collider2D>() !=null)
                {
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    child.gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }
}
