using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour {

    public Camera cam;
    public GameObject player;
    public int Rotate_x_speed;
    public int Rotate_y_speed;
    public float min_angle;
    public float max_angle;
    public player_controller player_controller;


    private void FixedUpdate()
    {
        this.transform.position = player.transform.position + player.transform.up*1.5f;

        float rotate_X = Input.GetAxis("Mouse X");
        float rotate_Y = Input.GetAxis("Mouse Y");

        this.transform.Rotate(this.transform.up, rotate_X * Rotate_x_speed);

        float angle = Vector3.Angle(this.transform.up, cam.transform.up);

        if (cam.transform.position.y >= player.transform.position.y)
        {
            angle = -angle;
        }

        if (angle <= min_angle && rotate_Y < 0)
        {
            cam.transform.RotateAround(this.transform.position, this.transform.right, rotate_Y * Rotate_y_speed);
        }
        else if (angle >= max_angle && rotate_Y > 0)
        {
            cam.transform.RotateAround(this.transform.position, this.transform.right, rotate_Y * Rotate_y_speed);
        }
        else if (angle >= min_angle && angle <= max_angle)
        {
            cam.transform.RotateAround(this.transform.position, this.transform.right, rotate_Y * Rotate_y_speed);
        }
    }

}
