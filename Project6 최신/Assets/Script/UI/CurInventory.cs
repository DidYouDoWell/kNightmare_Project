using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurInventory : BaseObject
{
	//bool equipedSlot1 = false;
	//bool IsInit = false;
	//ItemInstance equipItemInstance;
	//List<ItemInstance> itemList;
	Dictionary<eSlotType, ItemInstance> equipDic;

	//public ItemInstance ITEM_INSTANCE
	//{
	//	get { return itemInstance; }
	//	set { itemInstance = value; }
	//}

	eSelectItem selItem = eSelectItem.NONE;

	//해당 버튼들 삭제.
	//UITexture rewardItemTex = null;
	//UIButton ExchangeBtn = null;
	//UIButton PassBtn = null;

	UILabel curItem1Label = null;
	UILabel curItem2Label = null;

	UITexture curItem1Tex = null;
	UITexture curItem2Tex = null;

	UIToggle slot1 = null;
	UIToggle slot2 = null;

	//UILabel rewardItemLabel = null;
	UILabel slot1Label = null;
	UILabel slot2Label = null;

	UISprite borderOfCurItem1Tex = null;
	UISprite borderOfCurItem2Tex = null;

	//bool ItemSwitch = true;

	private void Awake()
	{


		
		curItem1Label = FindInChild("ItemDescription1").GetComponent<UILabel>();
		curItem2Label = FindInChild("ItemDescription2").GetComponent<UILabel>();

		curItem1Tex = FindInChild("CurrentItem1").GetComponentInChildren<UITexture>();
		curItem2Tex = FindInChild("CurrentItem2").GetComponentInChildren<UITexture>();

		
		borderOfCurItem1Tex = FindInChild("CurrentItem1").GetComponentInChildren<UISprite>();
		borderOfCurItem2Tex = FindInChild("CurrentItem2").GetComponentInChildren<UISprite>();

		//itemdescription을 건드려서 켜있는지 꺼있는지 확인할 것  -> 이걸로 아이템을 어디에 장착할 것인지 확인.

		slot1 = this.transform.Find("Panel").Find("CurrentItem1").GetComponent<UIToggle>();
		slot2 = this.transform.Find("Panel").Find("CurrentItem2").GetComponent<UIToggle>();

		slot1Label = FindInChild("CurrentItem1").GetComponentInChildren<UILabel>();
		slot2Label = FindInChild("CurrentItem2").GetComponentInChildren<UILabel>();
	}



	public void Init()
	{
		//if (IsInit == true)
		//	return;

		equipDic = ItemManager.Instance.DIC_EQUIP;

		ShowEquipItem();

	}
	
	public void ShowEquipItem()
	{


		string desc;

		ItemInstance tempItemIns = null;


		if (equipDic.Count >= 1)
		{
			//equipDic에서 아이템들을 받아옴
			equipDic.TryGetValue(eSlotType.SLOT_1, out tempItemIns);

			curItem1Tex.mainTexture = Resources.Load<Texture>("Textures/" + tempItemIns.ITEM_INFO.ITEM_IMAGE);



			desc = tempItemIns.ITEM_INFO.NAME + "\n\n"
				+ "HP = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.MAX_HP) + "\n"
				+ "Attack = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.ATTACK) + "\n"
				+ "Defence = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.DEFENCE);


			curItem1Label.text = desc;



			slot1Label.alpha = 0f;

			//selItem = eSelectItem.SELECT1;
		}
		else
		{
			curItem1Label.text = "1번 슬롯의 장착 아이템이 존재하지 않습니다.";
		}

		if (equipDic.Count == 2)
		{
			equipDic.TryGetValue(eSlotType.SLOT_2, out tempItemIns);

			curItem2Tex.mainTexture = Resources.Load<Texture>("Textures/" + tempItemIns.ITEM_INFO.ITEM_IMAGE);



			desc = tempItemIns.ITEM_INFO.NAME + "\n\n"
				+ "HP = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.MAX_HP) + "\n"
				+ "Attack = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.ATTACK) + "\n"
				+ "Defence = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.DEFENCE);


			curItem2Label.text = desc;

			slot2Label.alpha = 0f;

			//selItem = eSelectItem.SELECT2;
		}
		else
		{
			curItem2Label.text = "2번 슬롯의 장착 아이템이 존재하지 않습니다.";
		}


	}

	void Start()
	{
		EventDelegate.Add(slot1.onChange, () =>
		{
			if (slot1.value == true)
			{
				borderOfCurItem1Tex.alpha = 1f;
				borderOfCurItem2Tex.alpha = 0f;



				selItem = eSelectItem.SELECT1;

			}

		}
		);

		EventDelegate.Add(slot2.onChange, () =>
		{
			if (slot2.value == true)
			{
				borderOfCurItem2Tex.alpha = 1f;
				borderOfCurItem1Tex.alpha = 0f;


				selItem = eSelectItem.SELECT2;
			}

		}
		);


	}
}
