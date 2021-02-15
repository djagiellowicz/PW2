using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CombatBase
{
    // Start is called before the first frame update
    void Start()
    {
        if (Health <= 0) Health = 1000;
        if (Damage <= 0) Damage = 50;
        if (Team == null) Team = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
