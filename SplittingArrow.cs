using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingArrow : Arrow {

    public Transform target;

    [Header("Splitting")]
    private bool hasSplit;
    public GameObject splitProjectile;
    [SerializeField]
    int numberOfProjectiles;
    [SerializeField]
    float waitTime;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Split());
    }

    private IEnumerator Split()
    {
        yield return new WaitForSeconds(waitTime);

        if (Frozen == false)
        {
            float angleStep = 360f / numberOfProjectiles;
            float angle = 0f;
            Vector2 startPoint = gameObject.transform.position;
            for (int i = 0; i <= numberOfProjectiles - 1; i++)
            {
                float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180 * 5f);
                float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180 * 5f);
                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;
                var proj = Instantiate(splitProjectile, startPoint, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
                angle += angleStep;

            }
        }
    }
}
