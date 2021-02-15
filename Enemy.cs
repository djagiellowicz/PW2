using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CombatBase
{
    public NavMeshAgent NavMeshAgent;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent = this.GetComponent<NavMeshAgent>();
        if (Health <= 0) Health = 60;
        if (Damage <= 0) Damage = 50;
        Team = "Enemy";
        if (AttackSpeed == 0) AttackSpeed = 2;
        Player = GameObject.Find("Player");

    }

    private void FixedUpdate()
    {
        NavMeshAgent.SetDestination(Player.transform.position);
    }


}
