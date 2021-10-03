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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!NetworkManager.Singleton.IsServer) return;
            PlayerScript player = other.GetComponent<PlayerScript>();
            player.netHealth.Value--;
        }
        Destroy(gameObject);
    }
}
