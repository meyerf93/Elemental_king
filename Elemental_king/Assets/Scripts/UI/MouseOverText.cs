using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverText : MonoBehaviour {

	public string triggerName;

	MenuController controller;
	Animator controllerAnim;

	// Use this for initialization
	void Start () {
		controller = GetComponentInParent<MenuController>();
		controllerAnim = controller.GetComponent<Animator>();

		EventTrigger trigger = this.gameObject.AddComponent<EventTrigger>();

		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerEnter;
		entry1.callback.AddListener((eventData) =>  Hover());
		trigger.triggers.Add(entry1);

		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback.AddListener((eventData) => UnHover());
		trigger.triggers.Add(entry2);
	}
	
	void Hover() {
		if(controllerAnim.GetCurrentAnimatorStateInfo(0).IsName("Pause") || controllerAnim.GetCurrentAnimatorStateInfo(0).IsName("GameOver") || controllerAnim.GetCurrentAnimatorStateInfo(0).IsName("MainMenu")) {
			controller.SetSelectedItem(triggerName);
			controllerAnim.SetBool(triggerName,true);
		}
	}

	void UnHover() {
			controllerAnim.SetBool(triggerName,false);
			controller.SetSelectedItem("None");
	}
}
