using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public Material glowMaterial;


    void glowToggle()
    {
      glowMaterial.EnableKeyword("_EMISSION");
    }

    public void endGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            glowMaterial.DisableKeyword("_EMISSION");
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
            //Invoke("glowToggle", 2f);
        }
        
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //glowMaterial.EnableKeyword("_EMISSION")

    }

    private void Start()
    {
        glowToggle();
    }


}
