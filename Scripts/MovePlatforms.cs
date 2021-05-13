using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatforms : MonoBehaviour
{
    // Start is called before the first frame update

    public float xmax, ymax, xmin,ymin,speed;
    bool counter = true;
    public string move;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((move == "vertical") || (move == "Vertical") || (move == "VERTICAL"))
            moveVertical();
    }
    
    void moveVertical()
    {


        if (counter == true)
        {
            if (transform.position.y < ymax)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                counter = false;
            }
        }
        if (counter == false)
        {
            if (transform.position.y > ymin)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                counter = true;
            }
        }
    }
}
