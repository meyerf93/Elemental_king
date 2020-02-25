using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

	public float selectDelay;
  public player_controller playerController;

	Animator anim;
	static bool paused = false;
	static int selectedItem;
	float time;

	// Use this for initialization
	void Awake () {
		selectedItem = 0; // none
		anim = GetComponent<Animator>();
		time = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;
		if(!paused && Input.GetButtonDown("Start")) {
			Pause();
		}
		else if(paused && Input.GetButtonDown("Cancel")) {
			UnPause();
		}

		
		if(paused && (Input.GetAxisRaw("Menu Select") == 1 || Input.GetAxisRaw("Menu Select") == -1) && time > selectDelay) {
			time = 0f;
			switch(selectedItem) {

				case 0: // nothing is selected
					UnHoverItems();
					if(Input.GetAxisRaw("Menu Select") == -1) HoverItem(1);
					else if(Input.GetAxisRaw("Menu Select") == 1) HoverItem(3);
					break;
				case 1: // resume is selected
					UnHoverItems();
					if(Input.GetAxisRaw("Menu Select") == -1) HoverItem(2);
					else if(Input.GetAxisRaw("Menu Select") == 1) HoverItem(3);
					break;
				case 2: // options is selected
					UnHoverItems();
					if(Input.GetAxisRaw("Menu Select") == -1) HoverItem(3);
					else if(Input.GetAxisRaw("Menu Select") == 1) HoverItem(1);
					break;
				case 3: // exit is selected
					UnHoverItems();
					if(Input.GetAxisRaw("Menu Select") == -1) HoverItem(1);
					else if (Input.GetAxisRaw("Menu Select") == 1) HoverItem(2);
					break;
			}
		}

		if(paused && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))) {
			
			switch(selectedItem) {

				case 0:
					break;

				case 1: // resume
					UnPause();
					break;

				case 2: //options
					break;

				case 3: //exit
					#if UNITY_EDITOR
						UnityEditor.EditorApplication.isPlaying = false;
					#else
						Application.Quit();
					#endif
					break;
			}
		}
	}

	public void Pause() {
		anim.SetBool("Paused",true);
        playerController.menu_actived(true);
		paused = true;
    }
	public void UnPause() {
		anim.SetBool("Paused",false);
        playerController.menu_actived(false);
        paused = false;
    }
	public bool isPaused() {
		return paused;
	}

	public void HoverItem(int id) {
		UnHoverItems();
		Animator textAnimator = GetComponentsInChildren<Animator>()[id];
		textAnimator.SetBool("Hover",true);
		selectedItem = id;
	}
	public void UnHoverItems() {
		foreach(Animator anim in GetComponentsInChildren<Animator>()) {
			if(anim.GetComponent<MouseOverText>() != null) {
				anim.SetBool("Hover",false);
			}
		}
		selectedItem = 0;
	}
}
