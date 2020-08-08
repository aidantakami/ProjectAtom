using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void KillEnemy ();

    void EnemyPause ();

    void EnemyReset ();
    void EnemySetActive (bool setActive);
    void SetPosition (Vector3 pos);
    Vector3 GetPosition ();

}