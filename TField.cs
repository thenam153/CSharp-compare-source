using System;
using System.Threading;

// Token: 0x0200001B RID: 27
public class TField : IActionListener
{
	// Token: 0x060000CA RID: 202 RVA: 0x00009964 File Offset: 0x00007B64
	public TField(mScreen parentScr)
	{
		this.text = string.Empty;
		this.parentScr = parentScr;
		this.init();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00009A04 File Offset: 0x00007C04
	public TField()
	{
		this.text = string.Empty;
		this.init();
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00009A9C File Offset: 0x00007C9C
	public TField(int x, int y, int w, int h)
	{
		this.text = string.Empty;
		this.init();
		this.x = x;
		this.y = y;
		this.width = w;
		this.height = h;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00009B54 File Offset: 0x00007D54
	public TField(string text, int maxLen, int inputType)
	{
		this.text = text;
		this.maxTextLenght = maxLen;
		this.inputType = inputType;
		this.init();
		this.isTfield = true;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00003DB4 File Offset: 0x00001FB4
	public static bool setNormal(char ch)
	{
		return (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00003584 File Offset: 0x00001784
	public void doChangeToTextBox()
	{
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00009FC0 File Offset: 0x000081C0
	public static void setVendorTypeMode(int mode)
	{
		if (mode == TField.MOTO)
		{
			TField.print[0] = "0";
			TField.print[10] = " *";
			TField.print[11] = "#";
			TField.changeModeKey = 35;
		}
		else if (mode == TField.NOKIA)
		{
			TField.print[0] = " 0";
			TField.print[10] = "*";
			TField.print[11] = "#";
			TField.changeModeKey = 35;
		}
		else if (mode == TField.ORTHER)
		{
			TField.print[0] = "0";
			TField.print[10] = "*";
			TField.print[11] = " #";
			TField.changeModeKey = 42;
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000A080 File Offset: 0x00008280
	public void init()
	{
		TField.CARET_HEIGHT = mScreen.ITEM_HEIGHT + 1;
		this.cmdClear = new Command(mResources.DELETE, this, 1000, null);
		if (Main.isPC)
		{
			TField.typeXpeed = 0;
		}
		if (TField.imgTf == null)
		{
			TField.imgTf = GameCanvas.loadImage("/mainImage/myTexture2dtf.png");
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00003DE9 File Offset: 0x00001FE9
	public void clearKeyWhenPutText(int keyCode)
	{
		if (keyCode != -8)
		{
			return;
		}
		if (this.timeDelayKyCode > 0)
		{
			return;
		}
		if (this.timeDelayKyCode <= 0)
		{
			this.timeDelayKyCode = 1;
		}
		this.clear();
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00003E1A File Offset: 0x0000201A
	public void clearAllText()
	{
		this.text = string.Empty;
		if (TField.kb != null)
		{
			TField.kb.text = string.Empty;
		}
		this.caretPos = 0;
		this.setOffset(0);
		this.setPasswordTest();
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0000A0DC File Offset: 0x000082DC
	public void clear()
	{
		if (this.caretPos > 0 && this.text.Length > 0)
		{
			this.text = this.text.Substring(0, this.caretPos - 1);
			this.caretPos--;
			this.setOffset(0);
			this.setPasswordTest();
			if (TField.kb != null)
			{
				TField.kb.text = this.text;
			}
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000A158 File Offset: 0x00008358
	public void clearAll()
	{
		if (this.caretPos > 0 && this.text.Length > 0)
		{
			this.text = this.text.Substring(0, this.text.Length - 1);
			this.caretPos--;
			this.setOffset();
			this.setPasswordTest();
			this.setFocusWithKb(true);
			if (TField.kb != null)
			{
				TField.kb.text = string.Empty;
			}
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000A1DC File Offset: 0x000083DC
	public void setOffset()
	{
		if (this.paintedText == null || mFont.tahoma_8b == null)
		{
			return;
		}
		if (this.inputType == TField.INPUT_TYPE_PASSWORD)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		if (this.offsetX < 0 && mFont.tahoma_8b.getWidth(this.paintedText) + this.offsetX < this.width - TField.TEXT_GAP_X - 13 - TField.typingModeAreaWidth)
		{
			this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText);
		}
		if (this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) <= 0)
		{
			this.offsetX = -mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
			this.offsetX += 40;
		}
		else if (this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) >= this.width - 12 - TField.typingModeAreaWidth)
		{
			this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) - 2 * TField.TEXT_GAP_X;
		}
		if (this.offsetX > 0)
		{
			this.offsetX = 0;
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000A374 File Offset: 0x00008574
	private void keyPressedAny(int keyCode)
	{
		string[] array;
		if (this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY)
		{
			array = TField.printA;
		}
		else
		{
			array = TField.print;
		}
		if (keyCode == TField.lastKey)
		{
			this.indexOfActiveChar = (this.indexOfActiveChar + 1) % array[keyCode - 48].Length;
			char c = array[keyCode - 48][this.indexOfActiveChar];
			if (TField.mode == 0)
			{
				c = char.ToLower(c);
			}
			else if (TField.mode == 1)
			{
				c = char.ToUpper(c);
			}
			else if (TField.mode == 2)
			{
				c = char.ToUpper(c);
			}
			else
			{
				c = array[keyCode - 48][array[keyCode - 48].Length - 1];
			}
			string str = this.text.Substring(0, this.caretPos - 1) + c;
			if (this.caretPos < this.text.Length)
			{
				str += this.text.Substring(this.caretPos, this.text.Length);
			}
			this.text = str;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.setPasswordTest();
		}
		else if (this.text.Length < this.maxTextLenght)
		{
			if (TField.mode == 1 && TField.lastKey != -1984)
			{
				TField.mode = 0;
			}
			this.indexOfActiveChar = 0;
			char c2 = array[keyCode - 48][this.indexOfActiveChar];
			if (TField.mode == 0)
			{
				c2 = char.ToLower(c2);
			}
			else if (TField.mode == 1)
			{
				c2 = char.ToUpper(c2);
			}
			else if (TField.mode == 2)
			{
				c2 = char.ToUpper(c2);
			}
			else
			{
				c2 = array[keyCode - 48][array[keyCode - 48].Length - 1];
			}
			string str2 = this.text.Substring(0, this.caretPos) + c2;
			if (this.caretPos < this.text.Length)
			{
				str2 += this.text.Substring(this.caretPos, this.text.Length);
			}
			this.text = str2;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.caretPos++;
			this.setPasswordTest();
			this.setOffset();
		}
		TField.lastKey = keyCode;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000A604 File Offset: 0x00008804
	private void keyPressedAscii(int keyCode)
	{
		if ((this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY) && (keyCode < 48 || keyCode > 57) && (keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122))
		{
			return;
		}
		if (this.text.Length < this.maxTextLenght)
		{
			string str = this.text.Substring(0, this.caretPos) + (char)keyCode;
			if (this.caretPos < this.text.Length)
			{
				str += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
			}
			this.text = str;
			this.caretPos++;
			this.setPasswordTest();
			this.setOffset(0);
		}
		if (TField.kb != null)
		{
			TField.kb.text = this.text;
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00003E54 File Offset: 0x00002054
	public static void setMode()
	{
		TField.mode++;
		if (TField.mode > 3)
		{
			TField.mode = 0;
		}
		TField.lastKey = TField.changeModeKey;
		TField.timeChangeMode = (long)(Environment.TickCount / 1000);
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000A714 File Offset: 0x00008914
	private void setDau()
	{
		this.timeDau = (long)(Environment.TickCount / 100);
		if (this.indexDau == -1)
		{
			for (int i = this.caretPos; i > 0; i--)
			{
				char c = this.text[i - 1];
				for (int j = 0; j < TField.printDau.Length; j++)
				{
					char c2 = TField.printDau[j];
					if (c == c2)
					{
						this.indexTemplate = j;
						this.indexCong = 0;
						this.indexDau = i - 1;
						return;
					}
				}
			}
			this.indexDau = -1;
		}
		else
		{
			this.indexCong++;
			if (this.indexCong >= 6)
			{
				this.indexCong = 0;
			}
			string str = this.text.Substring(0, this.indexDau);
			string str2 = this.text.Substring(this.indexDau + 1);
			string str3 = TField.printDau.Substring(this.indexTemplate + this.indexCong, 1);
			this.text = str + str3 + str2;
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000A82C File Offset: 0x00008A2C
	public bool keyPressed(int keyCode)
	{
		if (Main.isPC && keyCode == -8)
		{
			this.clearKeyWhenPutText(-8);
			return true;
		}
		if (keyCode == 8 || keyCode == -8 || keyCode == 204)
		{
			this.clear();
			return true;
		}
		if (TField.isQwerty && keyCode >= 32)
		{
			this.keyPressedAscii(keyCode);
			return false;
		}
		if (keyCode == TField.changeDau && this.inputType == TField.INPUT_TYPE_ANY)
		{
			this.setDau();
			return false;
		}
		if (keyCode == 42)
		{
			keyCode = 58;
		}
		if (keyCode == 35)
		{
			keyCode = 59;
		}
		if (keyCode >= 48 && keyCode <= 59)
		{
			if (this.inputType == TField.INPUT_TYPE_ANY || this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY)
			{
				this.keyPressedAny(keyCode);
			}
			else if (this.inputType == TField.INPUT_TYPE_NUMERIC)
			{
				this.keyPressedAscii(keyCode);
				this.keyInActiveState = 1;
			}
		}
		else
		{
			this.indexOfActiveChar = 0;
			TField.lastKey = -1984;
			if (keyCode == 14 && !this.lockArrow)
			{
				if (this.caretPos > 0)
				{
					this.caretPos--;
					this.setOffset(0);
					this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
					return false;
				}
			}
			else if (keyCode == 15 && !this.lockArrow)
			{
				if (this.caretPos < this.text.Length)
				{
					this.caretPos++;
					this.setOffset(0);
					this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
					return false;
				}
			}
			else
			{
				if (keyCode == 19)
				{
					this.clear();
					return false;
				}
				TField.lastKey = keyCode;
			}
		}
		return true;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000A9FC File Offset: 0x00008BFC
	public void setOffset(int index)
	{
		if (this.inputType == TField.INPUT_TYPE_PASSWORD)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		int num = mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
		if (index == -1)
		{
			if (num + this.offsetX < 15 && this.caretPos > 0 && this.caretPos < this.paintedText.Length)
			{
				this.offsetX += mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos, 1));
			}
		}
		else if (index == 1)
		{
			if (num + this.offsetX > this.width - 25 && this.caretPos < this.paintedText.Length && this.caretPos > 0)
			{
				this.offsetX -= mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos - 1, 1));
			}
		}
		else
		{
			this.offsetX = -(num - (this.width - 12));
		}
		if (this.offsetX > 0)
		{
			this.offsetX = 0;
		}
		else if (this.offsetX < 0)
		{
			int num2 = mFont.tahoma_8b.getWidth(this.paintedText) - (this.width - 12);
			if (this.offsetX < -num2)
			{
				this.offsetX = -num2;
			}
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000AB90 File Offset: 0x00008D90
	public void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text, string info)
	{
		g.setColor(0);
		if (iss)
		{
			g.drawRegion(TField.imgTf, 0, 81, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 135, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + w - 58, y, 0);
			for (int i = 0; i < (w - 58) / 29; i++)
			{
				g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + 29 + i * 29, y, 0);
			}
		}
		else
		{
			g.drawRegion(TField.imgTf, 0, 0, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 54, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + w - 58, y, 0);
			for (int j = 0; j < (w - 58) / 29; j++)
			{
				g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + 29 + j * 29, y, 0);
			}
		}
		g.setClip(x + 3, y + 1, w - 4, h);
		if (text != null && !text.Equals(string.Empty))
		{
			mFont.tahoma_8b.drawString(g, text, xText, yText, 0);
		}
		else if (info != null)
		{
			if (iss)
			{
				mFont.tahoma_7b_focus.drawString(g, info, xText, yText, 0);
			}
			else
			{
				mFont.tahoma_7b_unfocus.drawString(g, info, xText, yText, 0);
			}
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000AD3C File Offset: 0x00008F3C
	public void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool flag = this.isFocused();
		if (this.inputType == TField.INPUT_TYPE_PASSWORD)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		this.paintInputTf(g, flag, this.x, this.y - 1, this.width, this.height + 5, TField.TEXT_GAP_X + this.offsetX + this.x + 1, this.y + (this.height - mFont.tahoma_8b.getHeight()) / 2 + 2, this.paintedText, this.name);
		g.setClip(this.x + 3, this.y + 1, this.width - 4, this.height - 2);
		g.setColor(0);
		if (flag && this.isPaintMouse && this.isPaintCarret)
		{
			if (this.keyInActiveState == 0 && (this.showCaretCounter > 0 || this.counter / TField.CARET_SHOWING_TIME % 4 == 0))
			{
				g.setColor(7999781);
				g.fillRect(TField.TEXT_GAP_X + 1 + this.offsetX + this.x + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos) + "a") - TField.CARET_WIDTH - mFont.tahoma_8b.getWidth("a"), this.y + (this.height - TField.CARET_HEIGHT) / 2 + 5, TField.CARET_WIDTH, TField.CARET_HEIGHT);
			}
			GameCanvas.resetTrans(g);
			if (this.text != null && this.text.Length > 0 && GameCanvas.isTouch)
			{
				g.drawImage(GameCanvas.imgClear, this.x + this.width - 13, this.y + this.height / 2 + 3, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00003E8E File Offset: 0x0000208E
	private bool isFocused()
	{
		return this.isFocus;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000AF4C File Offset: 0x0000914C
	public string subString(string str, int index, int indexTo)
	{
		if (index >= 0 && indexTo > str.Length - 1)
		{
			return str.Substring(index);
		}
		if (index < 0 || index > str.Length - 1 || indexTo < 0 || indexTo > str.Length - 1)
		{
			return string.Empty;
		}
		string text = string.Empty;
		for (int i = index; i < indexTo; i++)
		{
			text += str[i];
		}
		return text;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x0000AFD4 File Offset: 0x000091D4
	private void setPasswordTest()
	{
		if (this.inputType == TField.INPUT_TYPE_PASSWORD)
		{
			this.passwordText = string.Empty;
			for (int i = 0; i < this.text.Length; i++)
			{
				this.passwordText += "*";
			}
			if (this.keyInActiveState > 0 && this.caretPos > 0)
			{
				this.passwordText = this.passwordText.Substring(0, this.caretPos - 1) + this.text[this.caretPos - 1] + this.passwordText.Substring(this.caretPos, this.passwordText.Length);
			}
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000B09C File Offset: 0x0000929C
	public void update()
	{
		this.isPaintCarret = true;
		if (Main.isPC)
		{
			if (this.timeDelayKyCode > 0)
			{
				this.timeDelayKyCode--;
			}
			if (this.timeDelayKyCode <= 0)
			{
				this.timeDelayKyCode = 0;
			}
		}
		if (TField.kb != null && TField.currentTField == this)
		{
			if (TField.kb.text.Length < 40 && this.isFocus)
			{
				this.setText(TField.kb.text);
			}
			if (TField.kb.done && this.cmdDoneAction != null)
			{
				this.cmdDoneAction.performAction();
			}
		}
		this.counter++;
		if (this.keyInActiveState > 0)
		{
			this.keyInActiveState--;
			if (this.keyInActiveState == 0)
			{
				this.indexOfActiveChar = 0;
				if (TField.mode == 1 && TField.lastKey != TField.changeModeKey && this.isFocus)
				{
					TField.mode = 0;
				}
				TField.lastKey = -1984;
				this.setPasswordTest();
			}
		}
		if (this.showCaretCounter > 0)
		{
			this.showCaretCounter--;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			this.setTextBox();
		}
		if (this.indexDau != -1 && (long)(Environment.TickCount / 100) - this.timeDau > 5L)
		{
			this.indexDau = -1;
		}
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000B21C File Offset: 0x0000941C
	public void setTextBox()
	{
		if (GameCanvas.isPointerHoldIn(this.x + this.width - 20, this.y, 40, this.height))
		{
			this.clearAllText();
			this.isFocus = true;
		}
		else if (GameCanvas.isPointerHoldIn(this.x, this.y, this.width - 20, this.height))
		{
			this.setFocusWithKb(true);
		}
		else
		{
			this.setFocus(false);
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x0000B29C File Offset: 0x0000949C
	public void setFocus(bool isFocus)
	{
		if (this.isFocus != isFocus)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		if (isFocus)
		{
			TField.currentTField = this;
			if (TField.kb != null)
			{
				TField.kb.text = TField.currentTField.text;
			}
		}
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x0000B314 File Offset: 0x00009514
	public void setFocusWithKb(bool isFocus)
	{
		if (this.isFocus != isFocus)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		if (isFocus)
		{
			TField.currentTField = this;
		}
		else if (TField.currentTField == this)
		{
			TField.currentTField = null;
		}
		if (Thread.CurrentThread.Name == Main.mainThreadName && TField.currentTField != null)
		{
			isFocus = true;
			TouchScreenKeyboard.hideInput = !TField.currentTField.showSubTextField;
			TouchScreenKeyboardType t = TouchScreenKeyboardType.ASCIICapable;
			if (this.inputType == TField.INPUT_TYPE_NUMERIC)
			{
				t = TouchScreenKeyboardType.NumberPad;
			}
			bool type = false;
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				type = true;
			}
			TField.kb = TouchScreenKeyboard.Open(TField.currentTField.text, t, false, false, type, false, TField.currentTField.name);
			if (TField.kb != null)
			{
				TField.kb.text = TField.currentTField.text;
			}
			Cout.LogWarning("SHOW KEYBOARD FOR " + TField.currentTField.text);
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00003E96 File Offset: 0x00002096
	public string getText()
	{
		return this.text;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00003E9E File Offset: 0x0000209E
	public void clearKb()
	{
		if (TField.kb != null)
		{
			TField.kb.text = string.Empty;
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000B43C File Offset: 0x0000963C
	public void setText(string text)
	{
		if (text == null)
		{
			return;
		}
		TField.lastKey = -1984;
		this.keyInActiveState = 0;
		this.indexOfActiveChar = 0;
		this.text = text;
		this.paintedText = text;
		if (text == string.Empty)
		{
			TouchScreenKeyboard.Clear();
		}
		this.setPasswordTest();
		this.caretPos = text.Length;
		this.setOffset();
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000B4A4 File Offset: 0x000096A4
	public void insertText(string text)
	{
		this.text = this.text.Substring(0, this.caretPos) + text + this.text.Substring(this.caretPos);
		this.setPasswordTest();
		this.caretPos += text.Length;
		this.setOffset();
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00003EB9 File Offset: 0x000020B9
	public int getMaxTextLenght()
	{
		return this.maxTextLenght;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00003EC1 File Offset: 0x000020C1
	public void setMaxTextLenght(int maxTextLenght)
	{
		this.maxTextLenght = maxTextLenght;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00003ECA File Offset: 0x000020CA
	public int getIputType()
	{
		return this.inputType;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00003ED2 File Offset: 0x000020D2
	public void setIputType(int iputType)
	{
		this.inputType = iputType;
		this.setMaxTextLenght(500);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0000B500 File Offset: 0x00009700
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			this.clear();
		}
	}

	// Token: 0x04000094 RID: 148
	public const sbyte KEY_LEFT = 14;

	// Token: 0x04000095 RID: 149
	public const sbyte KEY_RIGHT = 15;

	// Token: 0x04000096 RID: 150
	public const sbyte KEY_CLEAR = 19;

	// Token: 0x04000097 RID: 151
	public bool isFocus;

	// Token: 0x04000098 RID: 152
	public int x;

	// Token: 0x04000099 RID: 153
	public int y;

	// Token: 0x0400009A RID: 154
	public int width;

	// Token: 0x0400009B RID: 155
	public int height;

	// Token: 0x0400009C RID: 156
	public bool lockArrow;

	// Token: 0x0400009D RID: 157
	public bool justReturnFromTextBox;

	// Token: 0x0400009E RID: 158
	public bool paintFocus = true;

	// Token: 0x0400009F RID: 159
	public static int typeXpeed = 2;

	// Token: 0x040000A0 RID: 160
	private static readonly int[] MAX_TIME_TO_CONFIRM_KEY = new int[]
	{
		30,
		14,
		11,
		9,
		6,
		4,
		2
	};

	// Token: 0x040000A1 RID: 161
	private static int CARET_HEIGHT = 0;

	// Token: 0x040000A2 RID: 162
	private static readonly int CARET_WIDTH = 1;

	// Token: 0x040000A3 RID: 163
	private static readonly int CARET_SHOWING_TIME = 5;

	// Token: 0x040000A4 RID: 164
	private static readonly int TEXT_GAP_X = 4;

	// Token: 0x040000A5 RID: 165
	private static readonly int MAX_SHOW_CARET_COUNER = 10;

	// Token: 0x040000A6 RID: 166
	public static readonly int INPUT_TYPE_ANY = 0;

	// Token: 0x040000A7 RID: 167
	public static readonly int INPUT_TYPE_NUMERIC = 1;

	// Token: 0x040000A8 RID: 168
	public static readonly int INPUT_TYPE_PASSWORD = 2;

	// Token: 0x040000A9 RID: 169
	public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

	// Token: 0x040000AA RID: 170
	private static string[] print = new string[]
	{
		" 0",
		".,@?!_1\"/$-():*+<=>;%&~#%^&*{}[];'/1",
		"abc2áàảãạâấầẩẫậăắằẳẵặ2",
		"def3đéèẻẽẹêếềểễệ3",
		"ghi4íìỉĩị4",
		"jkl5",
		"mno6óòỏõọôốồổỗộơớờởỡợ6",
		"pqrs7",
		"tuv8úùủũụưứừửữự8",
		"wxyz9ýỳỷỹỵ9",
		"*",
		"#"
	};

	// Token: 0x040000AB RID: 171
	private static string[] printA = new string[]
	{
		"0",
		"1",
		"abc2",
		"def3",
		"ghi4",
		"jkl5",
		"mno6",
		"pqrs7",
		"tuv8",
		"wxyz9",
		"0",
		"0"
	};

	// Token: 0x040000AC RID: 172
	private static string[] printBB = new string[]
	{
		" 0",
		"er1",
		"ty2",
		"ui3",
		"df4",
		"gh5",
		"jk6",
		"cv7",
		"bn8",
		"m9",
		"0",
		"0",
		"qw!",
		"as?",
		"zx",
		"op.",
		"l,"
	};

	// Token: 0x040000AD RID: 173
	private string text = string.Empty;

	// Token: 0x040000AE RID: 174
	private string passwordText = string.Empty;

	// Token: 0x040000AF RID: 175
	private string paintedText = string.Empty;

	// Token: 0x040000B0 RID: 176
	private int caretPos;

	// Token: 0x040000B1 RID: 177
	private int counter;

	// Token: 0x040000B2 RID: 178
	private int maxTextLenght = 500;

	// Token: 0x040000B3 RID: 179
	private int offsetX;

	// Token: 0x040000B4 RID: 180
	private static int lastKey = -1984;

	// Token: 0x040000B5 RID: 181
	private int keyInActiveState;

	// Token: 0x040000B6 RID: 182
	private int indexOfActiveChar;

	// Token: 0x040000B7 RID: 183
	private int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

	// Token: 0x040000B8 RID: 184
	private int inputType = TField.INPUT_TYPE_ANY;

	// Token: 0x040000B9 RID: 185
	public static bool isQwerty = true;

	// Token: 0x040000BA RID: 186
	public static int typingModeAreaWidth;

	// Token: 0x040000BB RID: 187
	public static int mode = 0;

	// Token: 0x040000BC RID: 188
	public static long timeChangeMode;

	// Token: 0x040000BD RID: 189
	public static readonly string[] modeNotify = new string[]
	{
		"abc",
		"Abc",
		"ABC",
		"123"
	};

	// Token: 0x040000BE RID: 190
	public static readonly int NOKIA = 0;

	// Token: 0x040000BF RID: 191
	public static readonly int MOTO = 1;

	// Token: 0x040000C0 RID: 192
	public static readonly int ORTHER = 2;

	// Token: 0x040000C1 RID: 193
	public static readonly int BB = 3;

	// Token: 0x040000C2 RID: 194
	public static int changeModeKey = 11;

	// Token: 0x040000C3 RID: 195
	public static readonly sbyte abc = 0;

	// Token: 0x040000C4 RID: 196
	public static readonly sbyte Abc = 1;

	// Token: 0x040000C5 RID: 197
	public static readonly sbyte ABC = 2;

	// Token: 0x040000C6 RID: 198
	public static readonly sbyte number123 = 3;

	// Token: 0x040000C7 RID: 199
	public static TField currentTField;

	// Token: 0x040000C8 RID: 200
	public bool isTfield;

	// Token: 0x040000C9 RID: 201
	public bool isPaintMouse = true;

	// Token: 0x040000CA RID: 202
	public string name = string.Empty;

	// Token: 0x040000CB RID: 203
	public string title = string.Empty;

	// Token: 0x040000CC RID: 204
	public string strInfo;

	// Token: 0x040000CD RID: 205
	public Command cmdClear;

	// Token: 0x040000CE RID: 206
	public Command cmdDoneAction;

	// Token: 0x040000CF RID: 207
	private mScreen parentScr;

	// Token: 0x040000D0 RID: 208
	private int timeDelayKyCode;

	// Token: 0x040000D1 RID: 209
	private int holdCount;

	// Token: 0x040000D2 RID: 210
	public static int changeDau;

	// Token: 0x040000D3 RID: 211
	private int indexDau = -1;

	// Token: 0x040000D4 RID: 212
	private int indexTemplate;

	// Token: 0x040000D5 RID: 213
	private int indexCong;

	// Token: 0x040000D6 RID: 214
	private long timeDau;

	// Token: 0x040000D7 RID: 215
	private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

	// Token: 0x040000D8 RID: 216
	public static Image imgTf;

	// Token: 0x040000D9 RID: 217
	public int timePutKeyClearAll;

	// Token: 0x040000DA RID: 218
	public int timeClearFirt;

	// Token: 0x040000DB RID: 219
	public bool isPaintCarret;

	// Token: 0x040000DC RID: 220
	public bool showSubTextField = true;

	// Token: 0x040000DD RID: 221
	public static TouchScreenKeyboard kb;

	// Token: 0x040000DE RID: 222
	public static int[][] BBKEY = new int[][]
	{
		new int[]
		{
			32,
			48
		},
		new int[]
		{
			49,
			69
		},
		new int[]
		{
			50,
			84
		},
		new int[]
		{
			51,
			85
		},
		new int[]
		{
			52,
			68
		},
		new int[]
		{
			53,
			71
		},
		new int[]
		{
			54,
			74
		},
		new int[]
		{
			55,
			67
		},
		new int[]
		{
			56,
			66
		},
		new int[]
		{
			57,
			77
		},
		new int[]
		{
			42,
			128
		},
		new int[]
		{
			35,
			137
		},
		new int[]
		{
			33,
			113
		},
		new int[]
		{
			63,
			97
		},
		new int[]
		{
			64,
			121,
			122
		},
		new int[]
		{
			46,
			111
		},
		new int[]
		{
			44,
			108
		}
	};
}
