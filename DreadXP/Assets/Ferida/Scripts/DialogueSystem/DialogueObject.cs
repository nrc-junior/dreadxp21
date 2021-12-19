using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Sprite[] pictures;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Response[] responses;
    [Header("NavMesh Configs")]
    [SerializeField] private bool timeToMove;
    [SerializeField] private Vector3 placeToMove;
    [SerializeField] private GameObject agent;

    [Header("Arts")]
    [SerializeField] private Sprite picture;
    [SerializeField] private Sprite background;

    [SerializeField] private Vector2 responseBoxSize;
    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

    public Vector3 PlaceToMove => placeToMove;
    public bool TimeToMove => timeToMove;
    public GameObject Agent => agent;
    public Sprite Picture => picture;
    public Sprite[] Pictures => pictures;
    public Sprite[] Backgrounds => backgrounds;
    public Vector2 ResponseBoxSize => responseBoxSize;
    public Sprite Background => background;
}
