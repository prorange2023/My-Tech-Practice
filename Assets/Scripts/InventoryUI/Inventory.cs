using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSlotCountChange(int val); // 대리자 정의
    public OnSlotCountChange onSlotCountChange; // 대리자 인스턴스화

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    void Start()
    {
        SlotCnt = 4;
    }

    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
            onChangeItem.Invoke();
            return true;
        }
        return false;
    }
    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
        onChangeItem.Invoke(); // 화면을 다시그려준다는게 무슨 소리여
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItems fielditems = collision.GetComponent<FieldItems>();
            if(AddItem(fielditems.GetItem()))
            {
                fielditems.DestroyItem();
            }
        }
    }
}
