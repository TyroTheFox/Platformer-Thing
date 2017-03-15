using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    float hitPoints = 10;


    public void ApplyDamage(float damage)
    {
        Debug.Log("Apply Damage");
        if (hitPoints <= 0)
            return;

        hitPoints -= damage;
        if (hitPoints <= 0.0)
        {
            Invoke("DelayedDetonate", 0);
        }
    }

    public void DelayedDetonate()
    {
        Destroy(gameObject);
    }
}
