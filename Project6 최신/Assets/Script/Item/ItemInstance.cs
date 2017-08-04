using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
	int ItemID = -1;
	int ItemNO = -1;
	// 실제 착용 위치
	eSlotType EquipSlotType = eSlotType.SLOT_NONE;
	ItemInfo Info = null;

	public int ITEM_ID { get { return ItemID; } }
	public int ITEM_NO { get { return ItemNO; } }
	public ItemInfo ITEM_INFO { get { return Info; } }

	public eSlotType SLOT_TYPE
	{
		get { return EquipSlotType; }
		set { EquipSlotType = value; }
	}

	public ItemInstance(int id, eSlotType type, ItemInfo _info)
	{
		ItemID = id;
		EquipSlotType = type;

		ItemNO = int.Parse(_info.KEY);

		StageInfo stageinfo = null;
		StageManager.Instance.DIC_STAGEINFO.TryGetValue(GameManager.Instance.SelectStage, out stageinfo);

		
		Info = new ItemInfo();

		Info.ITEM_IMAGE = _info.ITEM_IMAGE;
		Info.KEY = _info.KEY;
		Info.NAME = _info.NAME;
		

		Info.STATUS.SetData(eStatusData.MAX_HP, _info.STATUS.GetStatusData(eStatusData.MAX_HP) * (stageinfo.STATUS_SCALE));
		Info.STATUS.SetData(eStatusData.ATTACK, _info.STATUS.GetStatusData(eStatusData.ATTACK) * (stageinfo.STATUS_SCALE));
		Info.STATUS.SetData(eStatusData.DEFENCE, _info.STATUS.GetStatusData(eStatusData.DEFENCE) * (stageinfo.STATUS_SCALE));

	}

	

}
