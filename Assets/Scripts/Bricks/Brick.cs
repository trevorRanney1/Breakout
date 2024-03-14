using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BrickParent
{
    [SerializeField] Transform explosion;
    [SerializeField] Transform contents;

    public override void TakeDamage(int damageAmount)
    {
        print("Taking damage in child");
        base.TakeDamage(damageAmount);

        hitPoints -= damageAmount;

        if (hitPoints <= 0)
        {
            ApplyBrickEffect();
            DestroyBrick();
        }
        else
        {
            DamageBrick();
        }

        base.TakeDamage(damageAmount);
    }

    void DestroyBrick()
    {
        GameManager.instance.UpdateBrickNum();
        GameManager.instance.UpdateScore(pointValue);

        var go = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(go.gameObject, 2.25f);

        Destroy(gameObject);
    }

    void ApplyBrickEffect()
    {
        if (Random.Range(0f, 1f) > 0.2)
        {
            Instantiate(contents, transform.position, Quaternion.identity);
        }
    }

    void DamageBrick()
    {
        GetComponent<SpriteRenderer>().sprite = damageSprites[0];
        // Scale the Brick
        GetComponent<Transform>().localScale = new Vector3(0.4f, 0.4f, 0);
    }
}
