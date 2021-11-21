using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehavior : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime));
        transform.Translate(moveSpeed * (Vector3.forward - Vector3.right) * Time.deltaTime, Space.World);
    }
}
