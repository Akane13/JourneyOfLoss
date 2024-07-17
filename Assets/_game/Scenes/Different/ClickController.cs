using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //检查鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit object: "+hit.collider.gameObject.name);

                if (hit.collider.gameObject.GetComponent<Circle>() != null)
                {
                    hit.collider.gameObject.GetComponent<Circle>().ShowCircle();
                    //Add score
                    UIController.Instance.AddScore();
                }
            }
        }
    }
}
