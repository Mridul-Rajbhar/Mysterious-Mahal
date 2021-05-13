using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update

    #region Private 
    private GameObject Player;
    #endregion

    #region Public
    public GameObject spikes;
    public float x;
    public string dir;
    #endregion
    void Start()
    {
        Player = GameObject.Find("Player");
        //flipX = Player.GetComponent<SpriteRenderer>().flipX;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("cakkkk");
        if(checkPlayerPassed())
        {
            spikes.SetActive(true);
        }
    }
    public bool checkPlayerPassed()
    {
       // Debug.Log(flipX);
        //Debug.Log(spikes.transform.position.x);
        if(Player.transform.position.x < x && dir=="left")
        {
            Debug.Log("truee");
            return true;
        }
        else if(Player.transform.position.x > x && dir == "right")
        {
            Debug.Log("greater");
            return true ;
        }
        else
        {
            return false; ; ;
        }
    }
}
