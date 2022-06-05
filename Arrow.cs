using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [Header("References")]
    [SerializeField]
    protected Rigidbody2D _rigidbody;
    private PlayerActions playerActions;

    [Header("Customisation")]
    [SerializeField]
    protected float moveSpeed;

    [Header("Arrow")]
    [SerializeField]
    private float freezeDuration;
    private bool frozen;
    public float damage;
    public float abilityChargeGained;
    private Vector3 velocity;

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public bool Frozen
    {
        get
        {
            return frozen;
        }

        set
        {
            frozen = value;
        }
    }

    public void Initialize(PlayerActions _playerActions)
    {
        playerActions = _playerActions;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            transform.SetParent(collision.transform);
            Destroy(gameObject, freezeDuration);
            Freeze();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Boss") || collision.gameObject.layer == LayerMask.NameToLayer("MiniBoss"))
        {
            if (frozen == false)
            {
                ObjectReference reference = null;
                if (collision.gameObject.GetComponent<ObjectReference>())
                {
                    reference = collision.gameObject.GetComponent<ObjectReference>();
                }

                if (playerActions != null)
                {
                    playerActions.GainCharge(abilityChargeGained);
                }
                if (reference != null)
                {
                    Boss b = reference.References[0].GetComponent<Boss>();
                    b.TakeDamage(damage);
                    transform.SetParent(reference.References[0].transform);
                }
                else
                {
                    Boss b = collision.gameObject.GetComponent<Boss>();
                    b.TakeDamage(damage);
                    transform.SetParent(collision.transform);
                }
                Destroy(gameObject, freezeDuration);
                Freeze();
            }
        }
    }

    public void Freeze()
    {
        frozen = true;
        velocity = _rigidbody.velocity;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    public void Unfreeze()
    {
        frozen = false;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.velocity = velocity;
    }

    private void Update()
    {
        if (frozen == false)
        {
            transform.right = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
        }
    }

    protected virtual void Start()
    {
        _rigidbody.AddRelativeForce(transform.right * moveSpeed * Time.deltaTime);
    }
}
