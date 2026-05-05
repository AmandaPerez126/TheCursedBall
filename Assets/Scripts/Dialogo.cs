using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

//Muestra un diálogo línea por línea y avanza con clic izquierdo del ratón
public class Dialogo : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.07f; //Velocidad de escritura
    int index;


    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogo(); //Inicia el diálogo
    }

    void Update()
    {
        //Al hacer click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            // Si la línea ya está completa pasa a la siguiente
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                // Si no, completa instantáneamente la línea actual
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

    // Escribe letra a letra
    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        
        }
    }

    // Pasa a la siguiente línea o cierra el diálogo
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
            gameObject.SetActive(false); // Oculta el panel de diálogo
        }
    }

}
