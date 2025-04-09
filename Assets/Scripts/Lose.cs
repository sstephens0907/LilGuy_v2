using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public void youLose()
    {
        SceneManager.LoadScene(3);
    }
}
