using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public Vector3Variable dogPlayerPosition;
    [SerializeField] public Vector3Variable boomPlayerPosition;
    [SerializeField] private float rangeOfDogAttack;

    [SerializeField] private float rangeOfBoomGust;

    public List<GameObject> enemyList = new List<GameObject> ();

    //0, enemy is inactive, 1 enemy is active
    private List<int> enemyBookkeeping = new List<int> ();

    public void AddToEnemyManager (GameObject newIEnemy)
    {
        enemyList.Add (newIEnemy);
        enemyBookkeeping.Add (1);
    }

    public void SetEnemyInactive (GameObject deadEnemy)
    {
        for (int rep = 0; rep < enemyList.Count - 1; rep++)
        {
            if (enemyList[rep].Equals (deadEnemy))
            {
                deadEnemy.SetActive (false);
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
                enemyList[rep].SetActive (true);
                enemyList[rep].transform.position = position;
                enemyBookkeeping[rep] = 1;

            }
        }
    }

    public void DogAttackResponse ()
    {
        foreach (GameObject item in enemyList)
        {
            if (Vector3.Distance (item.transform.position, dogPlayerPosition.value) < rangeOfDogAttack)
            {
                if (item.CompareTag ("TargetDummy"))
                {
                    item.GetComponent<DummyEnemy> ().KillEnemy ();
                }
            }
        }
    }

    public void GustResponse ()
    {

        List<GameObject> enemiesInRangeOfGust = new List<GameObject> ();
        foreach (GameObject item in enemyList)
        {
            if (boomPlayerPosition.value.z + rangeOfBoomGust >= item.transform.position.z && item.transform.position.z > boomPlayerPosition.value.z)
            {

                enemiesInRangeOfGust.Add (item);

            }
        }

        StartCoroutine (ThreadedGustResponse (enemiesInRangeOfGust, boomPlayerPosition.value));
    }

    private IEnumerator ThreadedGustResponse (List<GameObject> inRangeEnemies, Vector3 startingPosition)
    {
        float incrementer = 0.2f;

        while (incrementer <= 1)
        {
            //Foreach loop for each wave of destruction!
            for (int rep = inRangeEnemies.Count - 1; rep >= 0; rep--)
            {
                GameObject item = inRangeEnemies[rep];
                if (item.transform.position.z <= startingPosition.z + (rangeOfBoomGust * incrementer))
                {
                    if (item.CompareTag ("TargetDummy"))
                    {
                        item.GetComponent<DummyEnemy> ().KillEnemy ();
                        inRangeEnemies.Remove (item);
                    }
                }
            }

            incrementer += 0.2f;
            yield return new WaitForSeconds (0.1f);
        }
    }
}