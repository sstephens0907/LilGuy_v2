using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BadGuyHealth : MonoBehaviour
{
    public float maxHealth = 5;
    public float currentHealth;
    private BadGuyScript badGuyScript;
    public Animator Anim;
    public Sprite greyHeart;
    public Sprite purpleHeart;
    public Image[] Hearts;
    

    void Start()
    {
        currentHealth = maxHealth;
        badGuyScript = GetComponent<BadGuyScript>();
        Debug.Log("BadGuy started");
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
        Debug.Log("BadGuy took damage " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            badGuyScript.TakeHit();
        }
    }

    void Die()
    {
        Anim.Play("Die");
        Debug.Log("BadGuy defeated");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Win");
        
    }
}
