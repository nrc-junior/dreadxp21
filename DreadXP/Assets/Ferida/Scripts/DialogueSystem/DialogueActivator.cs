
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("entrei");
        if(other.CompareTag("Player") && other.TryGetComponent(out Moving player))
        {
            player.Interactable = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out Moving player))
        {
            if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }  
    }

    public void Interact( Moving player)
    {
        if (TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject == dialogueObject)
        {
            player.DialogueUI.AddResponseEvents(responseEvents.Events);
        }
        player.DialogueUI.ShowDialogue(dialogueObject);
        //print(this.dialogueObject.PlaceToMove);
    }
}
