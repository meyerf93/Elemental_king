using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class SwapWorldText : MonoBehaviour {

	public player_controller playerController;

	Text text;
	Shadow shadow;
	 
	void Start() {
		text = GetComponent<Text>();
	}
	// Update is called once per frame
	void Update () {
		
		if(playerController.world == World.DARK) {
			text.text = "Dark";
			text.color = new Color32(172,104,233,255);
		}
		else {
			text.text = "Light";
			text.color = new Color32(210,210,210,255);
		}
	}
}
