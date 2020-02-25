using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public float selectDelay;
    public float victoryDelay;
    public float victoryTimer;
    public player_controller playerController;

    public MonsterAI monsterAI;

    private Animator anim;
    static int selectedItem;
    float time;

    // Use this for initialization
    void Awake()
    {
        
        anim = GetComponent<Animator>();
        time = 0f;
        playerController.menu_actived(true);
        selectedItem = 1; // new game
        anim.SetBool("HoverMainNewGame",true);
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        victoryTimer -= Time.deltaTime;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Play") && Input.GetButtonDown("Start"))
        {
            Pause();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pause") && Input.GetButtonDown("Cancel"))
        {
            UnPause();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Credits") && Input.GetButtonDown("Cancel")) {
            anim.SetBool("credits",false);
        }

        // Pause controller
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pause") && (Input.GetAxisRaw("Menu Select") == 1 || Input.GetAxisRaw("Menu Select") == -1) && time > selectDelay)
        {
            time = 0f;
            switch (selectedItem) {
                case 0: // nothing was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverPauseResume",true);
                        anim.SetBool("HoverPauseRestart",false);
                        anim.SetBool("HoverPauseExit",false);
                        selectedItem = 1;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverPauseResume",false);
                        anim.SetBool("HoverPauseRestart",false);
                        anim.SetBool("HoverPauseExit",true);
                        selectedItem = 3;
                    }
                    break;
                case 1: // resume was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverPauseResume",false);
                        anim.SetBool("HoverPauseRestart",true);
                        anim.SetBool("HoverPauseExit",false);
                        selectedItem = 2;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverPauseResume",false);
                        anim.SetBool("HoverPauseRestart",false);
                        anim.SetBool("HoverPauseExit",true);
                        selectedItem = 3;
                    }
                    break;
                case 2: // restart was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverPauseResume",false);
                        anim.SetBool("HoverPauseRestart",false);
                        anim.SetBool("HoverPauseExit",true);
                        selectedItem = 3;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverPauseResume",true);
                        anim.SetBool("HoverPauseRestart",false);
                        anim.SetBool("HoverPauseExit",false);
                        selectedItem = 1;
                    }
                    break;
                case 3: // exit is selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverPauseResume",true);
                        anim.SetBool("HoverPauseRestart",false);
                        anim.SetBool("HoverPauseExit",false);
                        selectedItem = 1;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverPauseResume",false);
                        anim.SetBool("HoverPauseRestart",true);
                        anim.SetBool("HoverPauseExit",false);
                        selectedItem = 2;
                    }
                    break;
            }
        } // Game over controller
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("GameOver") && (Input.GetAxisRaw("Menu Select") == 1 || Input.GetAxisRaw("Menu Select") == -1) && time > selectDelay) {
            time = 0f;
            Debug.Log("Test");
            switch (selectedItem) {
                case 0: // nothing was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverGameOverRestart",true);
                        anim.SetBool("HoverGameOverExit",false);
                        selectedItem = 1;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverGameOverRestart",false);
                        anim.SetBool("HoverGameOverExit",true);
                        selectedItem = 2;
                    }
                    break;
                case 1: // restart was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverGameOverRestart",false);
                        anim.SetBool("HoverGameOverExit",true);
                        selectedItem = 2;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverGameOverRestart",false);
                        anim.SetBool("HoverGameOverExit",true);
                        selectedItem = 2;
                    }
                    break;
                case 2: // exit was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverGameOverRestart",true);
                        anim.SetBool("HoverGameOverExit",false);
                        selectedItem = 1;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverGameOverRestart",true);
                        anim.SetBool("HoverGameOverExit",false);
                        selectedItem = 1;
                    }
                    break;
            }
        } // Main menu controller
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("MainMenu") && (Input.GetAxisRaw("Menu Select") == 1 || Input.GetAxisRaw("Menu Select") == -1) && time > selectDelay) {
            time = 0f;
            switch (selectedItem) {
                case 0: // nothing was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverMainNewGame",true);
                        anim.SetBool("HoverMainCredits",false);
                        anim.SetBool("HoverMainExit",false);
                        selectedItem = 1;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverMainNewGame",false);
                        anim.SetBool("HoverMainCredits",false);
                        anim.SetBool("HoverMainExit",true);
                        selectedItem = 3;
                    }
                    break;
                case 1: // resume was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverMainNewGame",false);
                        anim.SetBool("HoverMainCredits",true);
                        anim.SetBool("HoverMainExit",false);
                        selectedItem = 2;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverMainNewGame",false);
                        anim.SetBool("HoverMainCredits",false);
                        anim.SetBool("HoverMainExit",true);
                        selectedItem = 3;
                    }
                    break;
                case 2: // restart was selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverMainNewGame",false);
                        anim.SetBool("HoverMainCredits",false);
                        anim.SetBool("HoverMainExit",true);
                        selectedItem = 3;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverMainNewGame",true);
                        anim.SetBool("HoverMainCredits",false);
                        anim.SetBool("HoverMainExit",false);
                        selectedItem = 1;
                    }
                    break;
                case 3: // exit is selected
                    if (Input.GetAxisRaw("Menu Select") == -1) {
                        anim.SetBool("HoverMainNewGame",true);
                        anim.SetBool("HoverMainCredits",false);
                        anim.SetBool("HoverMainExit",false);
                        selectedItem = 1;
                    }
                    else if (Input.GetAxisRaw("Menu Select") == 1) {
                        anim.SetBool("HoverMainNewGame",false);
                        anim.SetBool("HoverMainCredits",true);
                        anim.SetBool("HoverMainExit",false);
                        selectedItem = 2;
                    }
                    break;
            }
        }

        // Pause selector
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pause") && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")))
        {
            switch (selectedItem)
            {

                case 0:
                    break;

                case 1: // resume
                    UnPause();
                    break;

                case 2: // restart
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;

                case 3: //exit
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
                    break;
            }
        } // Game over selector
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("GameOver") && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")))
        {
            switch (selectedItem)
            {

                case 0:
                    break;

                case 1: // restart
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;

                case 2: //exit
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
                    break;
            }
        } // Main menu selector
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("MainMenu") && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))) {
            switch (selectedItem) {
                case 0:
                    break;
                
                case 1: // new game
                    selectedItem = 0;
                    anim.SetTrigger("start");
                    playerController.menu_actived(false);
                    anim.SetBool("HoverMainNewGame",false);
                    anim.SetBool("HoverMainCredits",false);
                    anim.SetBool("HoverMainExit",false);
                    anim.SetTrigger("ShowControlHelp");
                    break;

                case 2: // credits
                    anim.SetBool("credits",true);
                    anim.SetTrigger("EnterCredits");
                    break;
                
                case 3: // exit
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
                    break;
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Victory") && Input.anyKey && victoryTimer <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetSelectedItem(string name) {
        switch(name) {
            case "None":
                selectedItem = 0;
                break;
            case "HoverPauseResume":
                selectedItem = 1;
                break;
            case "HoverPauseRestart":
                selectedItem = 2;
                break;
            case "HoverPauseExit":
                selectedItem = 3;
                break;
            default:
                selectedItem = 0;
                break;
        }
    }

    public void Pause()
    {
        anim.SetBool("pause",true);
        playerController.menu_actived(true);
        monsterAI.SetPause(true);
    }
    public void UnPause()
    {
        selectedItem = 0;
        anim.SetBool("HoverPauseResume",false);
        anim.SetBool("HoverPauseRestart",false);
        anim.SetBool("HoverPauseExit",false);
        anim.SetBool("pause",false);
        playerController.menu_actived(false);
        monsterAI.SetPause(false);
    }
}
