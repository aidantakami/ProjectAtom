using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public Vector3Variable dogPlayerPosition;
    [SerializeField] private float rangeOfDogAttack;

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
}