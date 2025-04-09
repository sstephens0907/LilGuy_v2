using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpLayer;
    public Vector2 Direction { get; set; }

    private GameObject itemHolding;
    private LilGuy lilGuy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lilGuy = GetComponent<LilGuy>();
        Debug.Log("PickUp Update running");
    }

    // Update is called once per frame
    void Update()
    {
        Direction = lilGuy.spriteRenderer.flipX ? Vector2.left : Vector2.right;

        // 💡 Always flip the holdSpot based on direction
        if (Direction == Vector2.left)
        {
            holdSpot.localPosition = new Vector3(-Mathf.Abs(holdSpot.localPosition.x), holdSpot.localPosition.y, holdSpot.localPosition.z);
        }
        else
        {
            holdSpot.localPosition = new Vector3(Mathf.Abs(holdSpot.localPosition.x), holdSpot.localPosition.y, holdSpot.localPosition.z);
        }

        // 💡 Always move the held item to match holdSpot
        if (itemHolding)
        {
            itemHolding.transform.position = holdSpot.position;
        }

        // 🔑 Pick up or drop
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemHolding)
            {
                itemHolding.transform.position = (Vector2)transform.position + Direction;
                itemHolding.transform.parent = null;
                if (itemHolding.TryGetComponent<Rigidbody2D>(out var rb))
                    rb.simulated = true;
                itemHolding = null;
            }
            else
            {
                Vector2 offset = Direction + new Vector2(0, -0.3f);
                Collider2D pickUpItem = Physics2D.OverlapCircle((Vector2)transform.position + offset, 0.2f, pickUpLayer);

                if (pickUpItem)
                {
                    itemHolding = pickUpItem.gameObject;
                    itemHolding.transform.position = holdSpot.position;
                    itemHolding.transform.parent = transform;
                    if (itemHolding.TryGetComponent<Rigidbody2D>(out var rb))
                        rb.simulated = false;
                    Debug.Log("Picked up " + pickUpItem.name);
                }
                else
                {
                    Debug.Log("No pickup item found");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (itemHolding)
            {
                ThrowItem(itemHolding);
                itemHolding = null;
            }
        }
    }


    void ThrowItem(GameObject item)
    {
        item.transform.parent = null;
        if (item.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.simulated = true;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            
            Vector2 throwDirection = (Vector2)Direction.normalized + Vector2.up * 0.75f;
            float throwForce = 9f;
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            Debug.Log("Throwing " + item.name);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + Direction, 0.3f);
    }
}
