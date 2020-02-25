using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Capacity{ LIGHT_FIRE_PASSIV_HEAL,DARK_FIRE_FIRESHOOT,
                        LIGHT_WATER_WALLK,DARK_WATER_SHIELD,
                        LIGHT_WIND_JUMP,DARK_WIND_DASH,
                        LIGHT_EARTH_ARMOR,DARK_EARTH_PUNCH,
                        LIGHT,DARK}

public class capacity_controller : MonoBehaviour {

    public ChangeOrbState CapacityUI;

    public GameObject[] Light_fire;
    public GameObject Dark_fire;
    public GameObject Light_water;
    public GameObject Dark_water;
    public GameObject[] Light_air;
    public GameObject Dark_air;
    public GameObject[] Light_earth;
    public GameObject[] Dark_earth;
    public GameObject Light;
    public GameObject Dark;

    public Animator ui_anim;
    public Animator model_anim;


    public Transform endPos;
    bool end = false;

    public List<Capacity> list_of_capacity;

	// Update is called once per frame
	void Update () {
        //activeUI();
        updateModel();
    }

    private void activeUI()
    {
        CapacityUI.ActivateDarkEarth(list_of_capacity.Contains(Capacity.DARK_EARTH_PUNCH));
        CapacityUI.ActivateDarkFire(list_of_capacity.Contains(Capacity.DARK_FIRE_FIRESHOOT));
        CapacityUI.ActivateDarkWater(list_of_capacity.Contains(Capacity.DARK_WATER_SHIELD));
        CapacityUI.ActivateDarkWind(list_of_capacity.Contains(Capacity.DARK_WIND_DASH));
        CapacityUI.ActivateLightEarth(list_of_capacity.Contains(Capacity.LIGHT_EARTH_ARMOR));
        CapacityUI.ActivateLightFire(list_of_capacity.Contains(Capacity.LIGHT_FIRE_PASSIV_HEAL));
        CapacityUI.ActivateLightWater(list_of_capacity.Contains(Capacity.LIGHT_WATER_WALLK));
        CapacityUI.ActivateLightWind(list_of_capacity.Contains(Capacity.LIGHT_WIND_JUMP));
    }

    public void AddRange(List<Capacity> listCapa)
    {

        StartCoroutine(animCapa(listCapa));
    }

    private IEnumerator sendUIanim(Capacity capa)
    {

        list_of_capacity.Add(capa);
        switch (capa)
        {
            case Capacity.DARK_EARTH_PUNCH:
                CapacityUI.ActivateDarkEarth(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(3).IsName("Finish"));
                model_anim.SetTrigger("DarkEarthOrb");
                break;
            case Capacity.DARK_FIRE_FIRESHOOT:
                CapacityUI.ActivateDarkFire(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(2).IsName("Finish"));
                model_anim.SetTrigger("DarkFireOrb");
                break;
            case Capacity.DARK_WATER_SHIELD:
                CapacityUI.ActivateDarkWater(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(5).IsName("Finish"));
                model_anim.SetTrigger("DarkWaterOrb");
                break;
            case Capacity.DARK_WIND_DASH:
                CapacityUI.ActivateDarkWind(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(4).IsName("Finish"));
                model_anim.SetTrigger("DarkWindOrb");
                break;
            case Capacity.LIGHT_EARTH_ARMOR:
                CapacityUI.ActivateLightEarth(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(7).IsName("Finish"));
                model_anim.SetTrigger("LightEarthOrb");
                break;
            case Capacity.LIGHT_FIRE_PASSIV_HEAL:
                CapacityUI.ActivateLightFire(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(6).IsName("Finish"));
                model_anim.SetTrigger("LightFireOrb");
                break;
            case Capacity.LIGHT_WATER_WALLK:
                CapacityUI.ActivateLightWater(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(8).IsName("Finish"));
                model_anim.SetTrigger("LightWaterOrb");
                yield return new WaitForSeconds((3.5f));
                ui_anim.SetTrigger("ShowTeleportHelp");
                break;
            case Capacity.LIGHT_WIND_JUMP:
                model_anim.SetBool("wing", true);
                CapacityUI.ActivateLightWind(true);
                yield return new WaitUntil(() => ui_anim.GetCurrentAnimatorStateInfo(9).IsName("Finish"));
                model_anim.SetTrigger("LightWindOrb");
                break;
        }
    }

    IEnumerator animCapa(List<Capacity> listCapa)
    {
        foreach(Capacity capa in listCapa)
        {
            if(!list_of_capacity.Contains(capa))
                yield return sendUIanim(capa);
        }
        yield return null;
    }

    private void updateModel()
    {
        foreach (GameObject model_elem in Light_fire)
        {
            model_elem.SetActive(list_of_capacity.Contains(Capacity.LIGHT_FIRE_PASSIV_HEAL));
        }
        Dark_fire.SetActive(list_of_capacity.Contains(Capacity.DARK_FIRE_FIRESHOOT));
        Light_water.SetActive(list_of_capacity.Contains(Capacity.LIGHT_WATER_WALLK));
        Dark_water.SetActive(list_of_capacity.Contains(Capacity.DARK_WATER_SHIELD));
        foreach (GameObject model_elem in Light_air)
        {
            model_elem.SetActive(list_of_capacity.Contains(Capacity.LIGHT_WIND_JUMP));
        }
        Dark_air.SetActive(list_of_capacity.Contains(Capacity.DARK_WIND_DASH));
        foreach (GameObject model_elem in Light_earth)
        {
            model_elem.SetActive(list_of_capacity.Contains(Capacity.LIGHT_EARTH_ARMOR));
        }
        foreach (GameObject model_elem in Dark_earth)
        {
            model_elem.SetActive(list_of_capacity.Contains(Capacity.DARK_EARTH_PUNCH));
        }
        
        Light.SetActive(list_of_capacity.Contains(Capacity.LIGHT));
        Dark.SetActive(list_of_capacity.Contains(Capacity.DARK));
        
        if(!end && list_of_capacity.Contains(Capacity.LIGHT) & list_of_capacity.Contains(Capacity.DARK))
        {
            end = true;
            transform.position = endPos.transform.position;
            GetComponent<player_controller>().Savepoint = endPos.transform.position;
        }
    }

}
