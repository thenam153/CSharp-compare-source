using System;

// Token: 0x0200008B RID: 139
public class Waypoint : IActionListener
{
	// Token: 0x0600042E RID: 1070 RVA: 0x00023878 File Offset: 0x00021A78
	public Waypoint(short minX, short minY, short maxX, short maxY, bool isEnter, bool isOffline, string name)
	{
		this.minX = minX;
		this.minY = minY;
		this.maxX = maxX;
		this.maxY = maxY;
		name = Res.changeString(name);
		this.isEnter = isEnter;
		this.isOffline = isOffline;
		if ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && this.minX >= 0 && this.minX <= 24)
		{
			return;
		}
		if (((TileMap.mapID == 0 && global::Char.myCharz().cgender != 0) || (TileMap.mapID == 7 && global::Char.myCharz().cgender != 1) || (TileMap.mapID == 14 && global::Char.myCharz().cgender != 2)) && isOffline)
		{
			return;
		}
		if (!TileMap.isInAirMap() && TileMap.mapID != 47)
		{
			if (!isEnter && !isOffline)
			{
				this.popup = new PopUp(name, (int)minX, (int)(minY - 24));
				this.popup.command = new Command(null, this, 1, this);
				this.popup.isWayPoint = true;
				this.popup.isPaint = false;
				PopUp.addPopUp(this.popup);
			}
			else
			{
				if (TileMap.isTrainingMap())
				{
					this.popup = new PopUp(name, (int)minX, (int)(minY - 16));
				}
				else
				{
					int x = (int)(minX + (maxX - minX) / 2);
					this.popup = new PopUp(name, x, (int)(minY - ((minY == 0) ? -32 : 16)));
				}
				this.popup.command = new Command(null, this, 2, this);
				this.popup.isWayPoint = true;
				this.popup.isPaint = false;
				PopUp.addPopUp(this.popup);
			}
			TileMap.vGo.addElement(this);
			return;
		}
		if (minY > 150 && TileMap.isInAirMap())
		{
			return;
		}
		this.popup = new PopUp(name, (int)(minX + (maxX - minX) / 2), (int)(maxY - ((minX <= 100) ? 48 : 24)));
		this.popup.command = new Command(null, this, 1, this);
		this.popup.isWayPoint = true;
		this.popup.isPaint = false;
		PopUp.addPopUp(this.popup);
		TileMap.vGo.addElement(this);
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00023A9C File Offset: 0x00021C9C
	public void perform(int idAction, object p)
	{
		if (idAction != 1)
		{
			if (idAction == 2)
			{
				GameScr.gI().auto = 0;
				if (global::Char.myCharz().isInEnterOfflinePoint() != null)
				{
					Service.gI().charMove();
					InfoDlg.showWait();
					Service.gI().getMapOffline();
					global::Char.ischangingMap = true;
					return;
				}
				if (global::Char.myCharz().isInEnterOnlinePoint() != null)
				{
					Service.gI().charMove();
					Service.gI().requestChangeMap();
					global::Char.isLockKey = true;
					global::Char.ischangingMap = true;
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					InfoDlg.showWait();
					return;
				}
				int xEnd = (int)((this.minX + this.maxX) / 2);
				int yEnd = (int)this.maxY;
				global::Char.myCharz().currentMovePoint = new MovePoint(xEnd, yEnd);
				global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
				global::Char.myCharz().endMovePointCommand = new Command(null, this, 2, null);
				return;
			}
		}
		else
		{
			int xEnd2 = (int)((this.minX + this.maxX) / 2);
			int yEnd2 = (int)this.maxY;
			if (this.maxY > this.minY + 24)
			{
				yEnd2 = (int)((this.minY + this.maxY) / 2);
			}
			GameScr.gI().auto = 0;
			global::Char.myCharz().currentMovePoint = new MovePoint(xEnd2, yEnd2);
			global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
			Service.gI().charMove();
		}
	}

	// Token: 0x04000713 RID: 1811
	public short minX;

	// Token: 0x04000714 RID: 1812
	public short minY;

	// Token: 0x04000715 RID: 1813
	public short maxX;

	// Token: 0x04000716 RID: 1814
	public short maxY;

	// Token: 0x04000717 RID: 1815
	public bool isEnter;

	// Token: 0x04000718 RID: 1816
	public bool isOffline;

	// Token: 0x04000719 RID: 1817
	public PopUp popup;
}
