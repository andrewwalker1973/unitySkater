using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent <Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            PickupFish();
    }

    private void PickupFish()
    {
        anim?.SetTrigger("Pickup");
        // add fish to count
        GameStats.Instance.CollectFish();
        // increment score
        // play sfx
        // trigger animation


    }

    public void OnShowChunk()
    {
        anim?.SetTrigger("Idle");
    }

}
