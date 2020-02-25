using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealthUI : MonoBehaviour {

	public Object fullHeart;
	public Object threeQuartersHeart;
	public Object midHeart;
	public Object oneQuarterHeart;
	public Object emptyHeart;

	Transform heart;

    private float maxHealth;

	// Use this for initialization
	void Start () {
		setMaxHealth(10);
		setLife(4.75f);
	}

    public void setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }
	
	void addHeart(float value = 1.0f) {

        value = 4 * value;

		switch((int)value) {

			case 4:
				Instantiate(fullHeart,transform);
				break;

			case 3:
				Instantiate(threeQuartersHeart,transform);
				break;

			case 2:
				Instantiate(midHeart,transform);
				break;

			case 1:
				Instantiate(oneQuarterHeart,transform);
				break;

			case 0:
				Instantiate(emptyHeart,transform);
				break;
		}
	}
	
	void removeHeart() {
		heart = transform.GetChild(transform.childCount); // get the last heart
		GameObject.Destroy(heart.gameObject); // destroy the last heart
	}

	void removeAllHearts() {
		for(int i=0; i < transform.childCount; i++) {
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
	}

	public void setLife(float newLife) {

		if(newLife > maxHealth) { //prevent to have more health than max health
			newLife = maxHealth;
		}

        newLife *= 4;
		int fullHeartCount = (int)newLife / 4;
		int lastHeartValue = (int)newLife % 4;
		

		removeAllHearts(); // we remove all hearts

        for (int i = 0;i<fullHeartCount;i++) { // add the full hearts
            addHeart();
        }

		switch(lastHeartValue) { // add the last heart

			case 0:
				break;

			case 1:
				addHeart(0.25f);
				break;

			case 2:
				addHeart(0.5f);
				break;

			case 3:
				addHeart(0.75f);
				break;
		}

		for(int i = fullHeartCount + ((lastHeartValue != 0) ? 1 : 0);i<maxHealth;i++) { // add the empty hearts
			addHeart(0f);
		}
	}
}
