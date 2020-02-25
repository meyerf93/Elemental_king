using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_shooter : MonoBehaviour {

    public GameObject fireball_prefab;
    public GameObject Start_fireball;
    public float distance;
    public float fireball_speed;
    public int max_simul = 2;

    private int num_fireball;

    public void shoot()
    {
        StartCoroutine(fireballAnim());
    }

    IEnumerator fireballAnim()
    {
        if (num_fireball < max_simul)
        {
            GameObject fireball = Instantiate(fireball_prefab);
            num_fireball++;

            fireball.transform.position = Start_fireball.transform.position;
            fireball.transform.rotation = Start_fireball.transform.rotation;

            float fireball_timing = distance / fireball_speed;

            for (float t = 0; t <= fireball_timing; t += Time.deltaTime)
            {
                if (fireball != null)
                    fireball.transform.position += fireball.transform.forward * fireball_speed * Time.deltaTime;
                yield return null;
            }

            yield return null;

            if (fireball != null) Destroy(fireball);
            num_fireball--;
        }
        yield return null;
    }
}
