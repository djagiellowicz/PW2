using System.Collections.Generic;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    public float Health;
    public float Damage;
    public float AttackSpeed;
    public List<CombatBase> NearbyEnemies;
    public string Team;
    public float LastAttack = Time.deltaTime;

    public void TryAttack()
    {
        float closestDistance = 99999;
        CombatBase nearestEnemy = null;
        bool isEnemyDestroyed = false;

        if (NearbyEnemies.Count >= 1)
        {

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

            if (isEnemyDestroyed) NearbyEnemies.Remove(nearestEnemy);
        }
    }

    public bool TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(this);
            return true;
        }
        return false;
    }

    public void OnCollisionEnter(Collision col)
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

    public void OnCollisionExit(Collision col)
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

