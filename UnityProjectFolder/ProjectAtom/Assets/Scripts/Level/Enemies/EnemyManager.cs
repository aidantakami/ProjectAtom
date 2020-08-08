using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public Vector3Variable dogPlayerPosition;
    [SerializeField] public Vector3Variable boomPlayerPosition;
    [SerializeField] private float rangeOfDogAttack;

    [SerializeField] private float rangeOfBoomGust;

    public List<IEnemy> enemyList = new List<IEnemy> ();

    //0, enemy is inactive, 1 enemy is active
    private List<int> enemyBookkeeping = new List<int> ();

    public void AddToEnemyManager (IEnemy newIEnemy)
    {
        enemyList.Add (newIEnemy);
        enemyBookkeeping.Add (1);
    }

    public void SetEnemyInactive (IEnemy deadEnemy)
    {
        for (int rep = 0; rep < enemyList.Count - 1; rep++)
        {
            if (enemyList[rep].Equals (deadEnemy))
            {
                deadEnemy.EnemySetActive (false);
                enemyBookkeeping[rep] = 0;
            }
        }
    }

    public void PlaceNewEnemy (Vector3 position)
    {
        for (int rep = 0; rep < enemyList.Count - 1; rep++)
        {
            if (enemyBookkeeping[rep] == 0)
            {
                enemyList[rep].EnemySetActive (true);
                enemyList[rep].SetPosition (position);
                enemyBookkeeping[rep] = 1;

            }
        }
    }

    public void DogAttackResponse ()
    {
        foreach (IEnemy item in enemyList)
        {
            if (Vector3.Distance (item.GetPosition (), dogPlayerPosition.value) < rangeOfDogAttack)
            {
                item.KillEnemy ();
            }
        }
    }

    public void GustResponse ()
    {

        List<IEnemy> enemiesInRangeOfGust = new List<IEnemy> ();
        foreach (IEnemy item in enemyList)
        {
            if (boomPlayerPosition.value.z + rangeOfBoomGust >= item.GetPosition ().z && item.GetPosition ().z > boomPlayerPosition.value.z)
            {

                enemiesInRangeOfGust.Add (item);

            }
        }

        StartCoroutine (ThreadedGustResponse (enemiesInRangeOfGust, boomPlayerPosition.value));
    }

    private IEnumerator ThreadedGustResponse (List<IEnemy> inRangeEnemies, Vector3 startingPosition)
    {
        float incrementer = 0.2f;

        while (incrementer <= 1)
        {
            //Foreach loop for each wave of destruction!
            for (int rep = inRangeEnemies.Count - 1; rep >= 0; rep--)
            {
                IEnemy item = inRangeEnemies[rep];
                if (item.GetPosition ().z <= startingPosition.z + (rangeOfBoomGust * incrementer))
                {
                    item.KillEnemy ();
                    inRangeEnemies.Remove (item);
                }
            }

            incrementer += 0.2f;
            yield return new WaitForSeconds (0.1f);
        }
    }
}