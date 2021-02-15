using System.Collections.Generic;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    public float Health;
    public float Damage;
    public float AttackSpeed;
    public List<CombatBase> NearbyEnemies;
    public string Team;
    public float TimeToAttack;

    public void Awake()
    {
        TimeToAttack = AttackSpeed;
    }
    public void TryAttack()
    {

        if (TimeToAttack == 0)
        {


            float closestDistance = 99999;
            CombatBase nearestEnemy = null;
            bool isEnemyDestroyed = false;

            if (NearbyEnemies.Count >= 1)
            {
                Debug.Log("Attacking " + this.name);
                foreach (var enemy in NearbyEnemies)
                {
                    var currentDistance = Vector3.Distance(enemy.transform.position, this.transform.position);

                    if (currentDistance <= closestDistance)
                    {
                        closestDistance = currentDistance;
                        nearestEnemy = enemy;
                    }
                }

                isEnemyDestroyed = nearestEnemy.TakeDamage(Damage);
                TimeToAttack = AttackSpeed;

                if (isEnemyDestroyed) NearbyEnemies.Remove(nearestEnemy);
            }
        }
    }

    private void Update()
    {
        TimeToAttack -= Time.deltaTime;
        if (TimeToAttack <= 0) TimeToAttack = 0;

        TryAttack();
    }

    public bool TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    public void OnTriggerEnter(Collider col)
    {
        var colCombatBase = col.gameObject.GetComponent<CombatBase>();

        if (colCombatBase != null)
        {
            if (!colCombatBase.Team.Equals(Team))
            {
                if (!NearbyEnemies.Contains(colCombatBase))
                {
                    NearbyEnemies.Add(colCombatBase);
                }
            }
        }

    }

    public void OnTriggerExit(Collider col)
    {
        var colCombatBase = col.gameObject.GetComponent<CombatBase>();

        if (colCombatBase != null)
        {
            if (!colCombatBase.Team.Equals(Team))
            {
                if (NearbyEnemies.Contains(colCombatBase))
                {
                    NearbyEnemies.Remove(colCombatBase);
                }

            }
        }
    }
}

