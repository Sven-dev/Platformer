using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWriter : MonoBehaviour
{
    public static DialogueWriter Instance;

    [SerializeField] private float Speed = 0.1f;
    [Space]
    [SerializeField] private GameObject Textbox;
    [SerializeField] private Text Label;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Write(Dialogue dialogue)
    {
        StartCoroutine(_Write(dialogue));
    }

    private IEnumerator _Write(Dialogue dialogue)
    {
        //Activate the dialogue so the rest of the system knows it's happening
        dialogue.Active = true;

        yield return null;

        //Clean up the textbox and enable it
        Label.text = "";
        Textbox.SetActive(true);

        int lettercount = 0;
        float cooldown = 0;
        bool writing = true;
        while(writing)
        {
            //If the player skips the dialogue, write the whole text down at once
            if (Controller.InteractButton.Pressed)
            {
                Label.text = dialogue.Text;
                writing = false;
            }
            else if (cooldown <= 0)
            {
                //Write the text 1 character at a time
                Label.text += dialogue.Text.Substring(lettercount, 1);
                lettercount++;
                
                //If the whole text is written, stop writing
                if (lettercount == dialogue.Text.Length)
                {
                    writing = false;
                }

                cooldown = Speed;
            }

            cooldown -= Time.deltaTime;
            yield return null;
        }

        //Wait until the player skips the text box
        while (!Controller.InteractButton.Pressed)
        {
            yield return null;
        }

        Textbox.SetActive(false);

        //Set the dialogue to inactive so the rest of the system can continue
        dialogue.Active = false;
    }
}
