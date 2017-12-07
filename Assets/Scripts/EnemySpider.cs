using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : Enemy
{
    public Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Die()
    {
        // Animate the spider
        anim.SetBool("spiderDead", true);

        base.Die();
    }
}
