using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class Dialogo : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.07f;
    int index;


    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            { 
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void StartDialogo()
    { 
        index = 0;
        StartCoroutine(WriteLine());

    }
    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index ++;
            dialogueText.text = string.Empty;
            StartCoroutine (WriteLine());

        }

        else 
        { 
            gameObject.SetActive(false);
        }
    }

}
