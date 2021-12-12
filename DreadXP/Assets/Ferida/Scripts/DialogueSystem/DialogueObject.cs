using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private Sprite picture;
    [SerializeField] private Sprite background;

    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

    public Sprite Picture => picture;
    public Sprite Background => background;
}
