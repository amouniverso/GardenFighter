using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, 150 * Time.deltaTime); //rotates 50 degrees per second around z axis
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            player.health--;
        }
        Destroy(gameObject);
    }
}
