using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour {

    [SerializeField] float warningRange;
    [SerializeField] float agressiveRange;
    [SerializeField] float attackRange;
    [SerializeField] float secondAttackRange;

    [SerializeField] float atkTimer;
    [SerializeField] float secondAtkTimer;

    public List<Capacity> capacity;

    public float moveSpeed;

    Vector3 startPos;
    player_controller player;
    Vector3 moveTo;
    Animator anim;

    private Rigidbody rb;

    float cooldownAtk01;
    float cooldownAtk02;

    static bool paused;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<player_controller>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (paused) return;

        if (cooldownAtk01 > 0)
            cooldownAtk01 -= Time.deltaTime;
        if (cooldownAtk02 > 0)
            cooldownAtk02 -= Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < secondAttackRange && capacity.Contains(Capacity.DARK_FIRE_FIRESHOOT) && cooldownAtk02 <= 0)
            Attack02();
        else if (distance < attackRange && cooldownAtk01 <= 0)
            Attack01();
        else if (distance < agressiveRange)
            Move(player.transform.position);
        else if (distance < warningRange)
            LookAt(player.transform.position);
        else
            Move(startPos);

    }

    public void SetPause(bool value)
    {
        paused = value;
    }

    public void Attack01()
    {
        cooldownAtk01 = atkTimer;
        anim.SetTrigger("attack01");
        if (capacity.Contains(Capacity.DARK_FIRE_FIRESHOOT))
            StartCoroutine(Dodge());
    }

    IEnumerator Dodge()
    {
        anim.SetFloat("direction_dodge_Y", 1f);
        yield return new WaitForSeconds(2f);
        anim.SetFloat("direction_dodge_Y", 0f);
    }

    public void Attack02()
    {
        cooldownAtk02 = secondAtkTimer;
        anim.SetTrigger("attack02");
        GetComponentInChildren<fire_shooter>().shoot();
    }

    public void Move(Vector3 target_position)
    {
        if(Vector3.Distance(this.transform.position,target_position)>= 1f)
        {
            target_position = new Vector3(target_position.x, transform.position.y, target_position.z);
            LookAt(target_position);
            Vector3 direction = (target_position - transform.position);
            rb.velocity = direction.normalized * moveSpeed;
        }
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude / moveSpeed;
        anim.SetFloat("walk_speed", speed);
    }

    void LookAt(Vector3 target)
    {
        target = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(target);
    }
}
