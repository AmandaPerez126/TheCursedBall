using System.Collections;
using UnityEngine;
using TMPro;

public class InstruccionesDialogo : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.07f;
    private int index;
    private bool dialogoCompletado = false;

    void Start()
    {
        if (GuardarProgreso.Instancia != null && GuardarProgreso.Instancia.TriggerFueActivado("InstruccionesDialogo"))
        {
            gameObject.SetActive(false);
            return;
        }

        dialogueText.text = string.Empty;
        StartDialogo();
    }

    void Update()
    {
        if (dialogoCompletado) return;

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
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            dialogoCompletado = true;
            if (GuardarProgreso.Instancia != null)
                GuardarProgreso.Instancia.RegistrarTrigger("InstruccionesDialogo");
            gameObject.SetActive(false);
        }
    }
}