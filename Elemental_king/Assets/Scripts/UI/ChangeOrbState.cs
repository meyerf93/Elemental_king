using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOrbState : MonoBehaviour {

	MenuController controller;
	Animator controllerAnim;

	private bool[] triggerOrb = new bool[8];

	void Start() {
		controller = GetComponentInParent<MenuController>();
		controllerAnim = controller.GetComponent<Animator>();
		DisableAll();
	}

	public void DisableAll() {
		foreach(Image image in GetComponentsInChildren<Image>()) {
			if(image.name.ToString().Contains("Orb")){
				image.GetComponentInChildren<Image>(true).enabled =true;
			}
		}
	}

	public void ActivateAll() {
		ActivateLightFire(true);
		ActivateLightEarth(true);
		ActivateLightWater(true);
		ActivateLightWind(true);

		ActivateDarkFire(true);
		ActivateDarkEarth(true);
		ActivateDarkWater(true);
		ActivateDarkWind(true);
	}

	// light orbs
	public void ActivateLightFire(bool active) {
		if(active && !triggerOrb[0]) {
			triggerOrb[0] = true;
			controllerAnim.SetTrigger("LightFireOrb");
		}
	}
	public void ActivateLightEarth(bool active) {
		if(active && !triggerOrb[1]){
			triggerOrb[1] = true;
			controllerAnim.SetTrigger("LightEarthOrb");
		}
	}
	public void ActivateLightWater(bool active) {
		if(active && !triggerOrb[2]){
			triggerOrb[2] = true;
			controllerAnim.SetTrigger("LightWaterOrb");
		}	
	}
	public void ActivateLightWind(bool active) {
		if(active && !triggerOrb[3]) {
			triggerOrb[3] = true;
			controllerAnim.SetTrigger("LightWindOrb");
		}
	}
	// dark orbs
	public void ActivateDarkFire(bool active) {
		if(active && !triggerOrb[4]) {
			triggerOrb[4] = true;
			controllerAnim.SetTrigger("DarkFireOrb");
		}
	}
	public void ActivateDarkEarth(bool active) {
		if(active && !triggerOrb[5]) {
			triggerOrb[5] = true;
			controllerAnim.SetTrigger("DarkEarthOrb");
		}
	}
	public void ActivateDarkWater(bool active) {
		if(active && !triggerOrb[6]) {
			triggerOrb[6] = true;
			controllerAnim.SetTrigger("DarkWaterOrb");
		}
	}
	public void ActivateDarkWind(bool active) {
		if(active && !triggerOrb[7]) {
			triggerOrb[7] = true;
			controllerAnim.SetTrigger("DarkWindOrb");
		}
	}
}
