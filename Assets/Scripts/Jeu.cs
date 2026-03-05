using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jeu : MonoBehaviour
{
    // Reference
    GameObject player;
    Skieur playerScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Skieur>();
    }

    void Update()
    {
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.rKey.wasPressedThisFrame) && (playerScript.isDead || playerScript.isWin))
        {
            RedemarrerScene();
        }

    }

    private void RedemarrerScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
