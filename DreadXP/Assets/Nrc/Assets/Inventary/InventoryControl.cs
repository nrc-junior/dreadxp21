using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour {
    
    [System.Serializable]
    public class Item {
    
        public int id;
        public Sprite sprite;
        public KeyCode key;
        public int usesLeft = -1;

        public Item(int id, Sprite sprite, KeyCode key, int usesLeft = -1) {
            this.id = id;
            this.sprite = sprite;
            this.key = key;
            this.usesLeft = usesLeft;
        }
    
        public delegate void UseAction();
        public static event UseAction on_use;

        public void UseItem() =>
            on_use?.Invoke();
    }
    public class InvUIManager {
        public struct ItemUI {
            public GameObject gameobject;
            public Image bg;
            public Image icon;
            
            public ItemUI(GameObject obj, Image bg, Image icon) {
                gameobject = obj;
                this.bg = bg;
                this.icon = icon;
            }

            public void apply(Item i) {
                icon.sprite = i.sprite;
            }
        }
        
        public ItemUI selected;
        public ItemUI left;
        public ItemUI right;

        public void enable_all(bool b) {
            selected.gameobject.SetActive(b);
            left.gameobject.SetActive(b);
            right.gameobject.SetActive(b);
        } 
    }
    
    public Item[] items;
    private InvUIManager ui;
    private Animator selected_animator;
    private Animator inventory_animator;
    
    private RectTransform inventoryUI;
    private RectTransform canvasRect;

    public GameObject player; //NRC Todo : Colocar "player" como um elemento estatico da cena. 
    [HideInInspector] public Camera cam; // NRC TODO: caso mude a camera, talvez seja necessario alterar essa variavel tbm.
    
    private float scrollCD = 0;
    private float fade_time = 0;
    private bool faded = false;
    
    //--------------------//
    
    int idx;
    List<Item> itemList = new List<Item>();
    public List<int> player_itemsID = new List<int>();
    private Item selected;

    private void Awake() {
        cam = Camera.main;
        canvasRect = transform.parent.GetComponent<RectTransform>();
        inventoryUI = GetComponent<RectTransform>();
        inventory_animator = GetComponent<Animator>();
        ui = new InvUIManager();
        
        for (int i = 0; i < transform.childCount; i++) {
           Transform child =  transform.GetChild(i);
           Image bg =  child.GetChild(0).GetComponent<Image>();
           Image icon =  child.GetChild(1).GetComponent<Image>();
           
           switch (child.name) {
               case "Selected": 
                   ui.selected =  new InvUIManager.ItemUI(child.gameObject, bg, icon);
                   selected_animator = child.GetComponent<Animator>();
                   break;
               case "Right": 
                   ui.right =  new InvUIManager.ItemUI(child.gameObject, bg, icon); 
                   break;
               case "Left":
                   ui.left = new InvUIManager.ItemUI(child.gameObject, bg, icon); 
                   break;
           }
        }
        
    }

    private void Start() {
        UpdateInventory();
        
        if (player_itemsID.Count == 0) {
            ui.enable_all(false);
        }else {
            ui.enable_all(true);
            UpdateInvUI();
        }
    }

    void UpdateInventory() {
        foreach (Item item in items) {
            foreach (int id in player_itemsID) {
                if (item.id == id) {
                    itemList.Add(item);
                }
            }
        }
    }

    void UpdateInvUI() {
        
        selected = itemList[idx];

        ui.selected.apply(selected);
        SoundManager.PlaySound(SoundManager.Sound.inventory_change);
        selected_animator.Play("SelectChange");
        ui.left.apply(itemList[idx - 1 < 0 ? itemList.Count - 1 : idx - 1]);
        ui.right.apply(itemList[idx + 1 < itemList.Count ? idx + 1 : 0]);
    }


    //todo: se o player pressionar "Item.key", invoca o evento do item.
    void Update() {
        int s = Mathf.RoundToInt(Input.mouseScrollDelta.y);
        if (!faded) {
            inventoryUI.localPosition = GetPlayerPositionOnScreen();
        }
        
        if (s == 0 || Time.time < scrollCD) {
            if (faded || !(Time.time > fade_time)) return;
            SoundManager.PlaySound(SoundManager.Sound.inventory_hide);
            inventory_animator.Play("DisableAll");
            faded = true;
            return; 
        }
        if (faded) {
            inventory_animator.Play("Idle");
            faded = false;
        }

        
        fade_time = Time.time + 1.8f;
        
        faded = false;
        scrollCD = Time.time + 0.1f;
        idx += s;
        
        
        if (idx >= itemList.Count) {
            idx = 0;
        }else if (idx < 0) {
            idx = itemList.Count - 1;
        }
        print(s);
        UpdateInvUI();
    }

    public void AddItem(int id) {
        player_itemsID.Add(id);
        UpdateInventory();
        UpdateInvUI();
    }
    
    public void RemoveItem(int id) {
        int index = player_itemsID.IndexOf(id);
        if(index == -1) return;
        player_itemsID.Remove(index);
        UpdateInventory();
        UpdateInvUI();
    }

    Vector2 GetPlayerPositionOnScreen() {
        Vector3 position = player.transform.position;
        Vector3 pos = new Vector3(position.x, position.y + 2, position.z);
        Vector2 screenPoint = cam.WorldToScreenPoint(pos);
        Vector2 canvasPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPoint);
        return canvasPoint;
    }
}
