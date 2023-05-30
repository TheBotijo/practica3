using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ratonFumon : MonoBehaviour
{

    public Transform _player;
    public Animator Animator;
    public float DetectionDistance;
    public DialogueManager dialogueMan;
    //public float FOV = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTriggers();
    }

    private void CheckTriggers()
    {
        bool isPlayerClose = IsPlayerClose();
        if (isPlayerClose)
        {
            transform.LookAt(_player.position);
            if (Input.GetButtonDown("Coger") && dialogueMan.talking == false && dialogueMan.mision == false )
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                FindObjectOfType<DialogueTrigger>().TriggerDialogue();
                isPlayerClose = false;
                Animator.SetBool("playerClose", false);
                Animator.SetBool("talking", true);
            }
        }

        Animator.SetBool("playerClose", isPlayerClose);

    }

    private bool IsPlayerClose()
    {
        Vector3 direction = _player.position - transform.position;
        return Vector3.Distance(_player.position, transform.position) < DetectionDistance ;
    }
}
