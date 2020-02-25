using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_health_controller : MonoBehaviour {
    public bool isFinalBoss;

    public float actual_health;
    public float Max_health;
    public float guarding_resistance;
    public float guarding_shield;
    public float armor;
    public capacity_controller capacity_Controller;

    private Animator animator;
    private MonsterAI monsterAI;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterAI = GetComponent<MonsterAI>();
    }

    public void takeDamage(float damage)
    {
        if(actual_health != 0)
        {
            if (animator.GetBool("guarding") && !monsterAI.capacity.Contains(Capacity.DARK_WATER_SHIELD))
            {
                damage -= guarding_resistance;
            }
            else if (animator.GetBool("guarding") && monsterAI.capacity.Contains(Capacity.DARK_WATER_SHIELD))
            {
                damage -= guarding_shield;
            }

            if (monsterAI.capacity.Contains(Capacity.LIGHT_EARTH_ARMOR))
            {
                damage -= guarding_shield;
            }

            if (damage < 0) damage = 0;

            if (actual_health - damage <= 0)
            {
                actual_health = 0;
                animator.SetTrigger("death");
                //monsterAI.SetPause(true);
                capacity_Controller.AddRange(monsterAI.capacity);
                StartCoroutine(WaitToDestroy());

                if (isFinalBoss)
                {
                    MenuController menuController = GameObject.Find("UI").GetComponent<MenuController>();
                    menuController.GetComponent<Animator>().SetTrigger("win");
                    menuController.victoryTimer = menuController.victoryDelay;
                    GameObject.Find("Player").GetComponent<player_controller>().menu_actived(true);
                    
                }

            }
            else
            {
                animator.SetTrigger("hit");
                actual_health -= damage;
            }
        }
    }

    public void receiveHeal(float heal)
    {
        if (heal < 0) heal = 0;

        if (actual_health + heal > Max_health) actual_health = Max_health;
        else actual_health += heal;
    }

    IEnumerator WaitToDestroy()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
