using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_controller : MonoBehaviour {

    public float actual_health;
    public int Max_health;

    public float guarding_resistance;
    public float guarding_shield;
    public float armor;
    public float delay_healing;
    public float passiv_healing;

    public ChangeHealthUI ui;
    public Animator UI_anim;

    private Animator animator;
    private capacity_controller capacity_Controller;
    private player_controller player_Controller;

    private Coroutine routine;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        capacity_Controller = GetComponent<capacity_controller>();
        player_Controller = GetComponent<player_controller>();
        ui.setLife(actual_health);
        ui.setMaxHealth(Max_health);
    }
	
	// Update is called once per frame
	void Update () {
        ui.setLife(actual_health);
        ui.setMaxHealth(Max_health);

        //if (Input.GetKeyDown("l")) takeDamage(4);
        //if (Input.GetKeyDown("k")) receiveHeal(1);

        if (capacity_Controller.list_of_capacity.Contains(Capacity.LIGHT_FIRE_PASSIV_HEAL) && routine == null)
        {
            routine = StartCoroutine(passivHealing());
        }
    }

    IEnumerator passivHealing()
    {
        while (true)
        {
            receiveHeal(passiv_healing);
            yield return new WaitForSeconds(delay_healing);
        }
    }
    
    public void takeDamage(float damage)
    {
        if(actual_health != 0)
        {
            if (animator.GetBool("guarding") && !capacity_Controller.list_of_capacity.Contains(Capacity.DARK_WATER_SHIELD))
            {
                damage -= guarding_resistance;
            }
            else if (animator.GetBool("guarding") && capacity_Controller.list_of_capacity.Contains(Capacity.DARK_WATER_SHIELD))
            {
                damage -= guarding_shield;
            }

            if (capacity_Controller.list_of_capacity.Contains(Capacity.LIGHT_EARTH_ARMOR))
            {
                damage -= guarding_shield;
            }

            if (damage < 0) damage = 0;

            if (actual_health - damage <= 0)
            {
                actual_health = 0;
                animator.SetTrigger("death");
                UI_anim.SetTrigger("lose");
                player_Controller.menu_actived(true);
            }
            else
            {
                animator.SetTrigger("hit");
                actual_health -= damage;
            }
            ui.setLife(actual_health);
        }
    }
    
    public void receiveHeal(float heal)
    {
        if (heal < 0) heal = 0;

        if (actual_health + heal > Max_health) actual_health = Max_health;
        else actual_health += heal;
        ui.setLife(actual_health);
    }
}
