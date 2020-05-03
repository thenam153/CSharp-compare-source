using System;

// Token: 0x02000097 RID: 151
public class CreateCharScr : mScreen, IActionListener
{
	// Token: 0x0600060F RID: 1551 RVA: 0x0004AC84 File Offset: 0x00048E84
	public CreateCharScr()
	{
		try
		{
			if (!GameCanvas.lowGraphic)
			{
				CreateCharScr.loadMapFromResource(new sbyte[]
				{
					39,
					40,
					41
				});
			}
			this.loadMapTableFromResource(new sbyte[]
			{
				39,
				40,
				41
			});
		}
		catch (Exception ex)
		{
			Cout.LogError("Tao char loi " + ex.ToString());
		}
		if (GameCanvas.w <= 200)
		{
			GameScr.setPopupSize(128, 100);
			GameScr.popupX = (GameCanvas.w - 128) / 2;
			GameScr.popupY = 10;
			this.cy += 15;
			this.dy -= 15;
		}
		CreateCharScr.indexGender = 1;
		CreateCharScr.tAddName = new TField();
		CreateCharScr.tAddName.width = GameCanvas.loginScr.tfUser.width;
		if (GameCanvas.w < 200)
		{
			CreateCharScr.tAddName.width = 60;
		}
		CreateCharScr.tAddName.height = mScreen.ITEM_HEIGHT + 2;
		if (GameCanvas.w < 200)
		{
			CreateCharScr.tAddName.x = GameScr.popupX + 45;
			CreateCharScr.tAddName.y = GameScr.popupY + 12;
		}
		else
		{
			CreateCharScr.tAddName.x = GameCanvas.w / 2 - CreateCharScr.tAddName.width / 2;
			CreateCharScr.tAddName.y = 35;
		}
		if (!GameCanvas.isTouch)
		{
			CreateCharScr.tAddName.isFocus = true;
		}
		CreateCharScr.tAddName.setIputType(TField.INPUT_TYPE_ANY);
		CreateCharScr.tAddName.showSubTextField = false;
		CreateCharScr.tAddName.strInfo = mResources.char_name;
		if (CreateCharScr.tAddName.getText().Equals("@"))
		{
			CreateCharScr.tAddName.setText(GameCanvas.loginScr.tfUser.getText().Substring(0, GameCanvas.loginScr.tfUser.getText().IndexOf("@")));
		}
		CreateCharScr.tAddName.name = mResources.char_name;
		CreateCharScr.indexGender = 1;
		CreateCharScr.indexHair = 0;
		this.center = new Command(mResources.NEWCHAR, this, 8000, null);
		this.left = new Command(mResources.BACK, this, 8001, null);
		if (!GameCanvas.isTouch)
		{
			this.right = CreateCharScr.tAddName.cmdClear;
		}
		this.yBegin = CreateCharScr.tAddName.y;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0000638D File Offset: 0x0000458D
	public static CreateCharScr gI()
	{
		if (CreateCharScr.instance == null)
		{
			CreateCharScr.instance = new CreateCharScr();
		}
		return CreateCharScr.instance;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00003584 File Offset: 0x00001784
	public static void init()
	{
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0004AFDC File Offset: 0x000491DC
	public static void loadMapFromResource(sbyte[] mapID)
	{
		Res.outz("newwwwwwwwww =============");
		for (int i = 0; i < mapID.Length; i++)
		{
			DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID[i]);
			MapTemplate.tmw[i] = (int)((ushort)dataInputStream.read());
			MapTemplate.tmh[i] = (int)((ushort)dataInputStream.read());
			Cout.LogError(string.Concat(new object[]
			{
				"Thong TIn : ",
				MapTemplate.tmw[i],
				"::",
				MapTemplate.tmh[i]
			}));
			MapTemplate.maps[i] = new int[dataInputStream.available()];
			Cout.LogError("lent= " + MapTemplate.maps[i].Length);
			for (int j = 0; j < MapTemplate.tmw[i] * MapTemplate.tmh[i]; j++)
			{
				MapTemplate.maps[i][j] = dataInputStream.read();
			}
			MapTemplate.types[i] = new int[MapTemplate.maps[i].Length];
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0004B0F0 File Offset: 0x000492F0
	public void loadMapTableFromResource(sbyte[] mapID)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		DataInputStream dataInputStream = null;
		try
		{
			for (int i = 0; i < mapID.Length; i++)
			{
				dataInputStream = MyStream.readFile("/mymap/mapTable" + mapID[i]);
				Cout.LogError("mapTable : " + mapID[i]);
				short num = dataInputStream.readShort();
				MapTemplate.vCurrItem[i] = new MyVector();
				Res.outz("nItem= " + num);
				for (int j = 0; j < (int)num; j++)
				{
					short id = dataInputStream.readShort();
					short num2 = dataInputStream.readShort();
					short num3 = dataInputStream.readShort();
					if (TileMap.getBIById((int)id) != null)
					{
						BgItem bibyId = TileMap.getBIById((int)id);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)id;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)num2 * (int)TileMap.size;
						bgItem.y = (int)num3 * (int)TileMap.size;
						bgItem.layer = bibyId.layer;
						MapTemplate.vCurrItem[i].addElement(bgItem);
						if (!BgItem.imgNew.containsKey(bgItem.idImage + string.Empty))
						{
							try
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image == null)
								{
									BgItem.imgNew.put(bgItem.idImage + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								else
								{
									BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
								}
							}
							catch (Exception ex)
							{
								Image image2 = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image2 == null)
								{
									image2 = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage + string.Empty, image2);
							}
							BgItem.vKeysLast.addElement(bgItem.idImage + string.Empty);
						}
						if (!BgItem.isExistKeyNews(bgItem.idImage + string.Empty))
						{
							BgItem.vKeysNew.addElement(bgItem.idImage + string.Empty);
						}
						bgItem.changeColor();
					}
					else
					{
						Res.outz("item null");
					}
				}
			}
		}
		catch (Exception ex2)
		{
			Cout.println("LOI TAI loadMapTableFromResource" + ex2.ToString());
		}
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0004B424 File Offset: 0x00049624
	public override void switchToMe()
	{
		LoginScr.isContinueToLogin = false;
		GameCanvas.menu.showMenu = false;
		GameCanvas.endDlg();
		base.switchToMe();
		CreateCharScr.indexGender = Res.random(0, 3);
		CreateCharScr.indexHair = Res.random(0, 3);
		this.doChangeMap();
		global::Char.isLoadingMap = false;
		CreateCharScr.tAddName.setFocusWithKb(true);
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0004B47C File Offset: 0x0004967C
	public void doChangeMap()
	{
		TileMap.maps = new int[MapTemplate.maps[CreateCharScr.indexGender].Length];
		for (int i = 0; i < MapTemplate.maps[CreateCharScr.indexGender].Length; i++)
		{
			TileMap.maps[i] = MapTemplate.maps[CreateCharScr.indexGender][i];
		}
		TileMap.types = MapTemplate.types[CreateCharScr.indexGender];
		TileMap.pxh = MapTemplate.pxh[CreateCharScr.indexGender];
		TileMap.pxw = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tileID = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tmw = MapTemplate.tmw[CreateCharScr.indexGender];
		TileMap.tmh = MapTemplate.tmh[CreateCharScr.indexGender];
		TileMap.tileID = this.bgID[CreateCharScr.indexGender] + 1;
		TileMap.loadMainTile();
		TileMap.loadTileCreatChar();
		GameCanvas.loadBG(this.bgID[CreateCharScr.indexGender]);
		GameScr.loadCamera(false, this.cx, this.cy);
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x000063A8 File Offset: 0x000045A8
	public override void keyPress(int keyCode)
	{
		CreateCharScr.tAddName.keyPressed(keyCode);
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0004B574 File Offset: 0x00049774
	public override void update()
	{
		this.cp1++;
		if (this.cp1 > 30)
		{
			this.cp1 = 0;
		}
		if (this.cp1 % 15 < 5)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		CreateCharScr.tAddName.update();
		if (CreateCharScr.selected != 0)
		{
			CreateCharScr.tAddName.isFocus = false;
		}
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0004B5EC File Offset: 0x000497EC
	public override void updateKey()
	{
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			CreateCharScr.selected--;
			if (CreateCharScr.selected < 0)
			{
				CreateCharScr.selected = mResources.MENUNEWCHAR.Length - 1;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			CreateCharScr.selected++;
			if (CreateCharScr.selected >= mResources.MENUNEWCHAR.Length)
			{
				CreateCharScr.selected = 0;
			}
		}
		if (CreateCharScr.selected == 0)
		{
			if (!GameCanvas.isTouch)
			{
				this.right = CreateCharScr.tAddName.cmdClear;
			}
			CreateCharScr.tAddName.update();
		}
		if (CreateCharScr.selected == 1)
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				CreateCharScr.indexGender--;
				if (CreateCharScr.indexGender < 0)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				this.doChangeMap();
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				CreateCharScr.indexGender++;
				if (CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1)
				{
					CreateCharScr.indexGender = 0;
				}
				this.doChangeMap();
			}
			this.right = null;
		}
		if (CreateCharScr.selected == 2)
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				CreateCharScr.indexHair--;
				if (CreateCharScr.indexHair < 0)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				CreateCharScr.indexHair++;
				if (CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1)
				{
					CreateCharScr.indexHair = 0;
				}
			}
			this.right = null;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			int num = 110;
			int num2 = 60;
			int num3 = 78;
			if (GameCanvas.w > GameCanvas.h)
			{
				num = 100;
				num2 = 40;
			}
			if (GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, 15, num3 * 3, 80))
			{
				CreateCharScr.selected = 0;
				CreateCharScr.tAddName.isFocus = true;
			}
			if (GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30, num3 * 3, num2 + 5))
			{
				CreateCharScr.selected = 1;
				int num4 = CreateCharScr.indexGender;
				CreateCharScr.indexGender = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				if (CreateCharScr.indexGender < 0)
				{
					CreateCharScr.indexGender = 0;
				}
				if (CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				if (num4 != CreateCharScr.indexGender)
				{
					this.doChangeMap();
				}
			}
			if (GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30 + num2 + 5, num3 * 3, 65))
			{
				CreateCharScr.selected = 2;
				int num5 = CreateCharScr.indexHair;
				CreateCharScr.indexHair = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				if (CreateCharScr.indexHair < 0)
				{
					CreateCharScr.indexHair = 0;
				}
				if (CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
				if (num5 != CreateCharScr.selected)
				{
					this.doChangeMap();
				}
			}
		}
		if (!TouchScreenKeyboard.visible)
		{
			base.updateKey();
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0004B974 File Offset: 0x00049B74
	public override void paint(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		GameCanvas.paintBGGameScr(g);
		g.translate(-GameScr.cmx, -GameScr.cmy);
		if (!GameCanvas.lowGraphic)
		{
			for (int i = 0; i < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); i++)
			{
				BgItem bgItem = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(i);
				if (bgItem.idImage != -1 && (int)bgItem.layer == 1)
				{
					bgItem.paint(g);
				}
			}
		}
		TileMap.paintTilemap(g);
		int num = 30;
		if (GameCanvas.w == 128)
		{
			num = 20;
		}
		int num2 = CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair];
		int num3 = CreateCharScr.defaultLeg[CreateCharScr.indexGender];
		int num4 = CreateCharScr.defaultBody[CreateCharScr.indexGender];
		g.drawImage(TileMap.bong, this.cx, this.cy + this.dy, 3);
		Part part = GameScr.parts[num2];
		Part part2 = GameScr.parts[num3];
		Part part3 = GameScr.parts[num4];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy + this.dy, 0, 0);
		SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy + this.dy, 0, 0);
		SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy + this.dy, 0, 0);
		if (!GameCanvas.lowGraphic)
		{
			for (int j = 0; j < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); j++)
			{
				BgItem bgItem2 = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(j);
				if (bgItem2.idImage != -1 && (int)bgItem2.layer == 3)
				{
					bgItem2.paint(g);
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (GameCanvas.w < 200)
		{
			GameCanvas.paintz.paintFrame(GameScr.popupX, GameScr.popupY, GameScr.popupW, GameScr.popupH, g);
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy, 0, 0);
			for (int k = 0; k < mResources.MENUNEWCHAR.Length; k++)
			{
				if (CreateCharScr.selected == k)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, GameScr.popupX + 10 + ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, GameScr.popupX + GameScr.popupW - 10 - ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.MENUNEWCHAR[k], GameScr.popupX + 20, GameScr.popupY + 30 + k * num, 0);
			}
			mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[CreateCharScr.indexGender], GameScr.popupX + 70, GameScr.popupY + 30 + 1 * num, mFont.LEFT);
			mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][CreateCharScr.indexHair], GameScr.popupX + 55, GameScr.popupY + 30 + 2 * num, mFont.LEFT);
			CreateCharScr.tAddName.paint(g);
		}
		else
		{
			if (!Main.isPC)
			{
				if (mGraphics.addYWhenOpenKeyBoard != 0)
				{
					this.yButton = 110;
					this.disY = 60;
					if (GameCanvas.w > GameCanvas.h)
					{
						this.yButton = GameScr.popupY + 30 + 3 * num + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy - 15;
						this.disY = 35;
					}
				}
				else
				{
					this.yButton = 110;
					this.disY = 60;
					if (GameCanvas.w > GameCanvas.h)
					{
						this.yButton = 100;
						this.disY = 45;
					}
				}
				CreateCharScr.tAddName.y = this.yButton - CreateCharScr.tAddName.height - this.disY + 5;
			}
			else
			{
				this.yButton = 110;
				this.disY = 60;
				if (GameCanvas.w > GameCanvas.h)
				{
					this.yButton = 100;
					this.disY = 45;
				}
				CreateCharScr.tAddName.y = this.yBegin;
			}
			for (int l = 0; l < 3; l++)
			{
				int num5 = 78;
				if (l != CreateCharScr.indexGender)
				{
					g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
				}
				else
				{
					if (CreateCharScr.selected == 1)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num5 + l * num5, this.yButton - 20 + ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), StaticObj.VCENTER_HCENTER);
					}
					g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[l], GameCanvas.w / 2 - num5 + l * num5, this.yButton - 5, mFont.CENTER);
			}
			for (int m = 0; m < 3; m++)
			{
				int num6 = 78;
				if (m != CreateCharScr.indexHair)
				{
					g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
				}
				else
				{
					if (CreateCharScr.selected == 2)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 20 + ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), StaticObj.VCENTER_HCENTER);
					}
					g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][m], GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 5, mFont.CENTER);
			}
			CreateCharScr.tAddName.paint(g);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		mFont.tahoma_7b_white.drawString(g, mResources.server + " " + LoginScr.serverName, 5, 5, 0, mFont.tahoma_7b_dark);
		if (!TouchScreenKeyboard.visible)
		{
			base.paint(g);
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0004C340 File Offset: 0x0004A540
	public void perform(int idAction, object p)
	{
		if (idAction != 8000)
		{
			if (idAction != 8001)
			{
				if (idAction != 10019)
				{
					if (idAction == 10020)
					{
						GameCanvas.endDlg();
					}
				}
				else
				{
					Session_ME.gI().close();
					GameCanvas.endDlg();
					GameCanvas.serverScreen.switchToMe();
				}
			}
			else
			{
				if (GameCanvas.loginScr.isLogin2)
				{
					GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, this, 10019, null), new Command(mResources.NO, this, 10020, null));
					return;
				}
				if (Main.isWindowsPhone)
				{
					GameMidlet.isBackWindowsPhone = true;
				}
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
			}
		}
		else
		{
			if (CreateCharScr.tAddName.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.char_name_blank);
				return;
			}
			if (CreateCharScr.tAddName.getText().Length < 5)
			{
				GameCanvas.startOKDlg(mResources.char_name_short);
				return;
			}
			if (CreateCharScr.tAddName.getText().Length > 15)
			{
				GameCanvas.startOKDlg(mResources.char_name_long);
				return;
			}
			InfoDlg.showWait();
			Service.gI().createChar(CreateCharScr.tAddName.getText(), CreateCharScr.indexGender, CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair]);
		}
	}

	// Token: 0x04000AEA RID: 2794
	public static CreateCharScr instance;

	// Token: 0x04000AEB RID: 2795
	private PopUp p;

	// Token: 0x04000AEC RID: 2796
	public static bool isCreateChar = false;

	// Token: 0x04000AED RID: 2797
	public static TField tAddName;

	// Token: 0x04000AEE RID: 2798
	public static int indexGender;

	// Token: 0x04000AEF RID: 2799
	public static int indexHair;

	// Token: 0x04000AF0 RID: 2800
	public static int selected;

	// Token: 0x04000AF1 RID: 2801
	public static int[][] hairID = new int[][]
	{
		new int[]
		{
			64,
			30,
			31
		},
		new int[]
		{
			9,
			29,
			32
		},
		new int[]
		{
			6,
			27,
			28
		}
	};

	// Token: 0x04000AF2 RID: 2802
	public static int[] defaultLeg = new int[]
	{
		2,
		13,
		8
	};

	// Token: 0x04000AF3 RID: 2803
	public static int[] defaultBody = new int[]
	{
		1,
		12,
		7
	};

	// Token: 0x04000AF4 RID: 2804
	private int yButton;

	// Token: 0x04000AF5 RID: 2805
	private int disY;

	// Token: 0x04000AF6 RID: 2806
	private int[] bgID = new int[]
	{
		0,
		4,
		8
	};

	// Token: 0x04000AF7 RID: 2807
	public int yBegin;

	// Token: 0x04000AF8 RID: 2808
	private int curIndex;

	// Token: 0x04000AF9 RID: 2809
	private int cx = 168;

	// Token: 0x04000AFA RID: 2810
	private int cy = 350;

	// Token: 0x04000AFB RID: 2811
	private int dy = 45;

	// Token: 0x04000AFC RID: 2812
	private int cp1;

	// Token: 0x04000AFD RID: 2813
	private int cf;
}
