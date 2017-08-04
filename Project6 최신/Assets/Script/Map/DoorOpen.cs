using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    public GameObject Door;
    public Animator Ani = null;
    public eDoorType m_IsDoorState;

    private void Start()
    {
        Ani = Door.GetComponent<Animator>();
        m_IsDoorState = eDoorType.DoorIdle;
        Ani.SetInteger("State", (int)m_IsDoorState);
    }
    // Update is called once per frame


    void Update()
    { 
        if(GameManager.Instance.GAME_START == true)
        {
            LoveISOpenDoor();
            Invoke("LoveISCloseDoor", 2.0f);
        }
        Ani.SetInteger("State", (int)m_IsDoorState);
    }
    //0번
    void LoveISCloseStay()
    {
        m_IsDoorState = eDoorType.DoorIdle;
    }
    //1번
    void LoveISOpenDoor()
    {
        m_IsDoorState = eDoorType.DoorOpen;
    }
    //2번
    void LoveISCloseDoor()
    {
        m_IsDoorState = eDoorType.DoorClose;
        GameManager.Instance.GAME_START = false;
    }
    //3번
    void LoveISOpenStay()
    {
        m_IsDoorState = eDoorType.DoorStay;
    }
}
