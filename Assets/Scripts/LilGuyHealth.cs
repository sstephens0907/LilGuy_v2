using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LilGuyHealth : MonoBehaviour
{
    public float maxHealth = 5;
    public float currentHealth;
    private LilGuy lilGuyScript;
    public Animator Anim;

    public Sprite greyHeart;
    public Sprite purpleHeart;
    public Image[] Hearts;

    void Start()
    {
        currentHealth = maxHealth;
        lilGuyScript = GetComponent<LilGuy>();
    }

    void Update()
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                Hearts[i].sprite = purpleHeart;
            }
            else
            {
                Hearts[i].sprite = greyHeart;
            }
            if (i < maxHealth)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("LilGuy took damage " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
       Anim.Play("dying");
        Debug.Log("LilGuy defeated");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Lose");
    }
}
