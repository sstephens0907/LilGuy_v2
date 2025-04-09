using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void youWin()
    {
        SceneManager.LoadScene(2);
    }
}
