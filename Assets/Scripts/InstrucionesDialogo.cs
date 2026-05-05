using System.Collections;
using UnityEngine;
using TMPro;

public class InstruccionesDialogo : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.07f;
    private int index;
    private bool dialogoActivo = true;
    private static bool dialogoCompletado = false;

    void Start()
    {
        if (dialogoCompletado)
        {
            Destroy(gameObject);
            return;
        }

        dialogueText.text = "";
        StartCoroutine(WriteLine());
    }

    void Update()
    {
        if (!dialogoActivo) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                SiguienteLinea();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    IEnumerator WriteLine()
    {
        dialogueText.text = "";
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SiguienteLinea()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(WriteLine());
        }
        else
        {
            dialogoActivo = false;
            dialogueText.text = "";
            dialogoCompletado = true;
            Destroy(gameObject);
        }
    }
}