using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnPlayerTouch : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        print("From trigger");
        if (other.gameObject.tag.Equals("Dog"))
        {
            GameObject dog = other.gameObject;
            Destroy(dog);
        }
    }
}
