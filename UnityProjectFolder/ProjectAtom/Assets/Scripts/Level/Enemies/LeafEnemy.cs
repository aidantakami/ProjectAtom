using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeafEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] public Animator leafAnimator;
    [SerializeField] public Animator ropeAnimator;
    [SerializeField] public Animator balloonAnimator;
    [SerializeField] public GameObject waterballoon;
    [SerializeField] public GameObject ropeGO;
    [SerializeField] public Material waterShader;
    [SerializeField] public ParticleSystem popPS;
    Material balloonMaterial;

    [SerializeField] public EnemyManager enemyManager;
    [SerializeField] public bool isRight;
    [SerializeField] UnityEvent dogCollided = new UnityEvent ();

    private Vector3 balloonFallIncrement = new Vector3 (0, 0.05f, 0);
    private Vector3 posAdjust = new Vector3 (45.491f, 0, 22.32f);
    private Vector3 positionTemp;
    private bool isPaused;
    // Start is called before the first frame update
    void Start ()
    {
        balloonMaterial = waterballoon.GetComponent<MeshRenderer> ().material;
        enemyManager.AddToEnemyManager (this);
        posAdjust = waterballoon.transform.localPosition;
        positionTemp = waterballoon.transform.localPosition;
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void EnemySetActive (bool setActive)
    {
        waterballoon.SetActive (setActive);
    }

    public void EnemyReset ()
    {
        ropeAnimator.SetBool ("hit", false);
        leafAnimator.SetBool ("hit", false);
        balloonAnimator.SetBool ("hit", false);

        if (positionTemp != Vector3.zero) waterballoon.transform.localPosition = positionTemp;

        waterballoon.GetComponent<MeshRenderer> ().material = balloonMaterial;
        waterballoon.GetComponent<MeshCollider> ().enabled = true;

    }

    public void SetPosition (Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector3 GetPosition ()
    {

        if (!isRight) return gameObject.transform.position - posAdjust;
        else return gameObject.transform.position + posAdjust;

    }

    public void KillEnemy ()
    {
        popPS.Play ();
        leafAnimator.SetBool ("hit", true);
        ropeAnimator.SetBool ("hit", true);
        balloonAnimator.SetBool ("hit", true);
        waterballoon.GetComponent<MeshRenderer> ().material = waterShader;
        waterballoon.GetComponent<MeshCollider> ().enabled = false;

        StartCoroutine (DeathCooldown ());
    }

    public void EnemyPause ()
    {
        leafAnimator.speed = 0;
        ropeAnimator.speed = 0;
        balloonAnimator.speed = 0;
        isPaused = true;

    }

    public void EnemyUnpause ()
    {
        leafAnimator.speed = 1;
        ropeAnimator.speed = 1;
        balloonAnimator.speed = 1;
        isPaused = false;

    }

    private IEnumerator DeathCooldown ()
    {

        while (waterballoon.transform.localPosition.y > 0.2)
        {

            if (!isPaused)
            {
                waterballoon.transform.localPosition -= (balloonFallIncrement);
            }

            yield return new WaitForEndOfFrame ();

        }

        yield return new WaitForSeconds (3f);
        EnemyReset ();
    }
}