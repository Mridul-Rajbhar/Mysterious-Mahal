using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    #region Private Variables
    private Rigidbody2D myBody;
    private bool rightDirection = true;
    #endregion


    #region Public Variables
    #endregion

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if(rightDirection == true)
        { }
    }
}
