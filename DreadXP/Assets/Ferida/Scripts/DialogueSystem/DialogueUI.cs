using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private Image picture;
    [SerializeField] private Image background;
    public bool IsOpen {get; private set; }
    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {

        dialogueBox.SetActive(true);
        background.sprite = dialogueObject.Background;
        picture.sprite = dialogueObject.Picture;
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {

        for(int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            if (dialogueObject.Pictures.Length >= i+1 && dialogueObject.Pictures[i] != null){
                picture.sprite = dialogueObject.Pictures[i];
            }
            if (dialogueObject.Backgrounds.Length >= i+1 && dialogueObject.Backgrounds[i] != null){
                background.sprite = dialogueObject.Backgrounds[i];
            }
            
            print(i);
            IsOpen = true;
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if(i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses, dialogueObject);
        }
        else
        {
            CloseDialogueBox(dialogueObject);
        }
 
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);
        while(typewriterEffect.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }


    public void CloseDialogueBox(DialogueObject dialogueObject = null)
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        
        if (dialogueObject != null){
            print(dialogueObject.PlaceToMove);
        
        }
        
    }
}
