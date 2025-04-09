
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float damage = 2f;
    private BadGuyHealth badGuyHealth;
    public Animator Anim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (badGuyHealth == null)
            {
                badGuyHealth = collision.gameObject.GetComponent<BadGuyHealth>();
            }
            badGuyHealth.TakeDamage(damage);
            Anim.Play("Die");
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}