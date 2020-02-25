using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_damage : MonoBehaviour {

    public float damage_send;
    public int max_simult;
    private Animator animator;

    private int actual_simult;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        max_simult = 1;
    }

    private void Update()
    {
        if(tag == "Projectile" || !this.animator.GetCurrentAnimatorStateInfo(1).IsName("Attack 01"))
        {
            actual_simult = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(tag == "Projectile" || this.animator.GetCurrentAnimatorStateInfo(1).IsName("Attack 01") && actual_simult < max_simult)
            {
                actual_simult++;
                other.GetComponent<Health_controller>().takeDamage(damage_send);
                if (this.tag == "Projectile")
                    Destroy(this.gameObject);
            }

        }
        if(other.tag == "Monster")
        {
            if (tag == "Projectile" || this.animator.GetCurrentAnimatorStateInfo(1).IsName("Attack 01") && actual_simult < max_simult)
            {
                actual_simult++;
                other.GetComponent<monster_health_controller>().takeDamage(damage_send);
                if (this.tag == "Projectile")
                    Destroy(this.gameObject);
            }
        }

        if(other.tag == "Obstacle" && this.tag == "PowerPunch" && this.animator.GetCurrentAnimatorStateInfo(1).IsName("Attack 01"))
        {
            Destroy(other.gameObject);
        }
    }
}
