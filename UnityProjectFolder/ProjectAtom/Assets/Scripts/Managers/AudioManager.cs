using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource MenuSelectionSound;
    [SerializeField] private AudioSource BoomCaughtSound;
    [SerializeField] private AudioSource DogAttackSound;
    [SerializeField] private AudioSource BoomAttackSound;
    [SerializeField] private AudioSource BoomThrownSound;
    [SerializeField] private AudioSource BoomReviveSound;
    [SerializeField] private AudioSource EndGameSound;
    [SerializeField] private AudioSource BoomMagnetSound;

    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void MenuSelectionPlay (bool isDog)
    {
        MenuSelectionSound.Play ();
    }

    public void BoomCaughtPlay ()
    {
        BoomCaughtSound.Play ();
    }

    public void DogAttackPlay ()
    {
        DogAttackSound.Play ();
    }

    public void BoomAttackPlay ()
    {
        BoomAttackSound.Play ();
    }

    public void BoomThrownPlay ()
    {
        BoomThrownSound.Play ();
    }

    public void BoomRevivePlay ()
    {
        BoomReviveSound.Play ();
    }

    public void EndGamePlay ()
    {
        EndGameSound.Play ();
    }

    public void BoomMagnetPlay ()
    {
        BoomMagnetSound.Play ();
    }

}