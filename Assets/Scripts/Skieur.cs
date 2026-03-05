using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using NUnit.Framework;

[RequireComponent(typeof(Collider2D))]
public class Skieur : MonoBehaviour
{
    // References
    GameObject sprite;

    // Mouvement
    public InputAction hMove;
    public InputAction vMove;
    public float spd = 7.5f;

    // État
    public bool isDead;
    public bool isWin;

    // Score
    public int points;

    // Sons
    AudioSource audioSource;
    public AudioClip sPoint;
    public AudioClip sOw;

    // Utiliser ces fonctions pour activer et d�sactiver les InputActions
    private void OnEnable()
    {
        hMove.Enable();
        vMove.Enable();
    }
    private void OnDisable()
    {
        hMove.Disable();
        vMove.Disable();
    }

    void Start()
    {
        sprite = GameObject.FindGameObjectWithTag("PSprite");
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        float horizontalTran;
        float verticalTran;

        if (isDead == false && isWin == false)
        {

            horizontalTran = hMove.ReadValue<float>();
            verticalTran = vMove.ReadValue<float>();
        }
        else
        {
            horizontalTran = 0;
            verticalTran = 0;
        }
     
        transform.Translate(Vector2.right * horizontalTran * spd * Time.deltaTime);
        transform.Translate(Vector2.up * verticalTran * spd * Time.deltaTime);

        if(horizontalTran==-1 && verticalTran==-1) {sprite.transform.localRotation = Quaternion.Euler(0, 0, -45);}
        else if(horizontalTran==1 && verticalTran==-1) {sprite.transform.localRotation = Quaternion.Euler(0, 0, 45);}
        else if(horizontalTran==-1) {sprite.transform.localRotation = Quaternion.Euler(0, 0, -90);}
        else if(horizontalTran==1) {sprite.transform.localRotation = Quaternion.Euler(0, 0, 90);}
        else {sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);}
    }

    // Il faut appeller cette fonction dans la collision avec le y�ti.
    void DeconnecterCamera()
    {
        Camera.main.GetComponent<PositionConstraint>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Danger"))
        {
            isDead = true;
            GetComponent<Collider2D>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(sOw);
        }
        else {audioSource.PlayOneShot(sOw);}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
        {
            points ++;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            audioSource.PlayOneShot(sPoint);
        }

        if(collision.gameObject.CompareTag("Points"))
        {
            points ++;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            audioSource.PlayOneShot(sPoint);
        }

        if(collision.gameObject.CompareTag("FinJeu"))
        {
            isWin = true;
        }
    }
}
