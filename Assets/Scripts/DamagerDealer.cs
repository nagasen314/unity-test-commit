using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerDealer : MonoBehaviour
{

    [SerializeField] int damage = 100;

    // accessors
    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject); // destroy this object when Hit() is called
    }

}
