using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum World { LIGHT_FIRE, LIGHT_EARTH, LIGHT_WIND, LIGHT_WATER, LIGHT, DARK, DARK_FIRE, DARK_EARTH, DARK_WIND, DARK_WATER, FINAL };
public class player_controller : MonoBehaviour {

    private Rigidbody rb;
    public int speed_movement;
    public int hight_jump;
    public GameObject camera_controller;
    public Camera_controller cam_Controller;
    public float swap_timing;
    public float target_rotate_x;
    public GameObject minimap_camera;
    public Animator player_animator;
    public fire_shooter fire_Shooter;
    public World world;
    public Vector3 Savepoint;
    public World SaveWorld;
    public Image blackScreen;
    public int timeBlackScreen;

    public int wind_jump;
    public float dash_speed;
    public float dash_distance;


    private bool swapp_trigger = false;
    private capacity_controller capacity_Controller;
    private Animator animator;

    private Coroutine dashRoutine;

    private int num_jump = 1;
    private float jump = 0;

    private bool menu_active;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        capacity_Controller = GetComponent<capacity_controller>();
        animator = GetComponent<Animator>();
        world = World.LIGHT;
        Savepoint = this.transform.position;
        SaveWorld = this.world;
    }


    private void FixedUpdate()
    {
        if (!menu_active) {

            float horizontal_speed = Input.GetAxis("Horizontal") * speed_movement;
            float vertical_speed = Input.GetAxis("Vertical") * speed_movement;

            Vector3 Direction = camera_controller.transform.right * horizontal_speed + camera_controller.transform.forward * vertical_speed;

            if (Input.GetButtonDown("Jump"))
            {
                if (capacity_Controller.list_of_capacity.Contains(Capacity.LIGHT_WIND_JUMP) && num_jump < wind_jump)
                {
                    player_animator.SetTrigger("jump");
                    jump = hight_jump;
                    num_jump++;
                }
                else if (Physics.Raycast(this.transform.position + this.transform.up * 0.1f, -this.transform.up, 0.3f))
                {
                    player_animator.SetTrigger("jump");
                    jump = hight_jump;
                    num_jump = 1;
                }
            }
            else
            {
                jump = rb.velocity.y;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                player_animator.SetTrigger("attack01");
            }

            if (Input.GetButtonDown("Fire3"))
            {
                if (capacity_Controller.list_of_capacity.Contains(Capacity.DARK_FIRE_FIRESHOOT) && !animator.GetBool("guarding"))
                {
                    player_animator.SetTrigger("attack02");
                    fire_Shooter.shoot();
                }

            }
            if (Input.GetButtonDown("Guarding"))
            {
                if (capacity_Controller.list_of_capacity.Contains(Capacity.DARK_WATER_SHIELD))
                {
                    player_animator.SetBool("shield", true);
                }
                else
                {
                    player_animator.SetBool("shield", false);
                }
                player_animator.SetBool("guarding", true);
            }
            if (Input.GetButtonUp("Guarding"))
            {
                player_animator.SetBool("guarding", false);
            }
            if (Input.GetButtonDown("Dodge"))
            {
                if (capacity_Controller.list_of_capacity.Contains(Capacity.DARK_WIND_DASH) && dashRoutine == null)
                {
                    player_animator.SetTrigger("dodge");
                    dashRoutine =StartCoroutine(dashAnim());
                }
            }

            rb.velocity = Direction + transform.up * jump;
            float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude / speed_movement;

            if (rb.velocity.x != 0 || rb.velocity.z != 0)
            {
                player_animator.SetFloat("walk_speed", speed);

                Vector3 Rotate = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                this.transform.LookAt(this.transform.position + Rotate);
            }
            else
            {
                player_animator.SetFloat("walk_speed", speed);
            }
        }
        else
        {
            if (rb != null)
                rb.velocity = Vector3.zero;
        }

    }

    IEnumerator dashAnim()
    {
        float fireball_timing = dash_distance / dash_speed;

        for (float t = 0; t <= fireball_timing; t += Time.deltaTime)
        {
            this.transform.position += this.transform.forward * dash_speed * Time.deltaTime;
            camera_controller.transform.position = this.transform.position+this.transform.up*1.5f;
            yield return null;
        }

        yield return null;
        dashRoutine = null;
    }

    IEnumerator dropInWater(float damages)
    {
        rb.isKinematic = true;

        //drop
        //black screen
        blackScreen.enabled = true;
        for(float t = 0; t < timeBlackScreen/2; t += Time.deltaTime)
        {
            this.transform.position -= this.transform.up * t * 0.1f;
            camera_controller.transform.position = this.transform.position+this.transform.up*1.5f;
            blackScreen.color = new Color(0, 0, 0, 2f * t / timeBlackScreen);
            yield return null;
        }
        this.transform.position = Savepoint;
        this.world = SaveWorld;
        GetComponent<Health_controller>().takeDamage(damages);

        for (float t = 0; t < timeBlackScreen / 2; t += Time.deltaTime)
        {
            blackScreen.color = new Color(0, 0, 0, 1f - 2f * t / timeBlackScreen);
            yield return null;
        }
        blackScreen.enabled = false;
        yield return null;
        rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Water")
        {
            if (!capacity_Controller.list_of_capacity.Contains(Capacity.LIGHT_WATER_WALLK))
            {
                StartCoroutine(dropInWater(0.25f));
            }
        }
        if (collision.collider.tag == "Lava")
        {
            StartCoroutine(dropInWater(2f));
        }
    }



    private void OnTriggerStay(Collider other)
    {
        World target_world = World.DARK;

        if (world == World.LIGHT)
        {
            target_world = World.DARK;
        }
        else if(world == World.DARK)
        {
            target_world = World.LIGHT;
        }
        if (other.CompareTag("Swap"))
        {
            if (Input.GetButtonDown("Fire1") && !swapp_trigger)
            {
                swapp_trigger = true;
                RaycastHit hitInfo = new RaycastHit();
                bool rayCastResult = false;
                if (world == World.DARK)
                {
                    rayCastResult = Physics.Raycast(this.transform.position + 2 * this.transform.up, this.transform.up, out hitInfo, 300, 1 << 8);
                }
                else if (world == World.LIGHT)
                {
                    rayCastResult = Physics.Raycast(this.transform.position -2 * this.transform.up, -this.transform.up, out hitInfo, 300, 1 << 8);
                }
                if (rayCastResult)
                {
                    Vector3 hitPos = hitInfo.point + this.transform.up;
                    StartCoroutine(anim(hitPos, world, target_world));
                }
            }
        }
    }

    IEnumerator anim(Vector3 target, World actual_world, World target_world)
    {
        Collider coll = GetComponent<Collider>();
        coll.enabled = false;
        float startY = this.transform.position.y;
        float endY = target.y;

        float actualY = 0.0f;

        for (float t = 0; t <= swap_timing; t += Time.deltaTime)
        {
            actualY = Mathf.Lerp(startY, endY, t/ swap_timing);
            this.transform.position = new Vector3(target.x, actualY, target.z);
            camera_controller.transform.position = this.transform.position;
            yield return null;
        }

        swapp_trigger = false;
        coll.enabled = true;
        world = target_world;
    }

    public void menu_actived(bool on)
    {
        menu_active = on;
    }
}
