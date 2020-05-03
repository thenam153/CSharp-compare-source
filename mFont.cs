using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class mFont
{
	// Token: 0x06000174 RID: 372 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
	public mFont(string strFont, string pathImage, string pathData, int space)
	{
		try
		{
			this.strFont = strFont;
			this.space = space;
			this.pathImage = pathImage;
			DataInputStream dataInputStream = null;
			this.reloadImage();
			try
			{
				dataInputStream = MyStream.readFile(pathData);
				this.fImages = new int[(int)dataInputStream.readShort()][];
				for (int i = 0; i < this.fImages.Length; i++)
				{
					this.fImages[i] = new int[4];
					this.fImages[i][0] = (int)dataInputStream.readShort();
					this.fImages[i][1] = (int)dataInputStream.readShort();
					this.fImages[i][2] = (int)dataInputStream.readShort();
					this.fImages[i][3] = (int)dataInputStream.readShort();
					this.setHeight(this.fImages[i][3]);
				}
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex2)
				{
					ex2.StackTrace.ToString();
				}
			}
		}
		catch (Exception ex3)
		{
			ex3.StackTrace.ToString();
		}
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000D924 File Offset: 0x0000BB24
	public mFont(sbyte id)
	{
		string text = "chelthm";
		if (((int)id > 0 && (int)id < 10) || (int)id == 19)
		{
			this.yAdd = 1;
			text = "barmeneb";
		}
		else if ((int)id >= 10 && (int)id <= 18)
		{
			text = "chelthm";
			this.yAdd = 2;
		}
		else if ((int)id > 24)
		{
			text = "staccato";
		}
		this.id = id;
		text = string.Concat(new object[]
		{
			"FontSys/x",
			mGraphics.zoomLevel,
			"/",
			text
		});
		this.myFont = (Font)Resources.Load(text);
		if ((int)id < 25)
		{
			this.color1 = this.setColorFont(id);
			this.color2 = this.setColorFont(id);
		}
		else
		{
			this.color1 = this.bigColor((int)id);
			this.color2 = this.bigColor((int)id);
		}
		this.wO = this.getWidthExactOf("o");
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
	public static void init()
	{
		if (mGraphics.zoomLevel == 1)
		{
			mFont.tahoma_7b_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_red.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_blue.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_white.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellowSmall = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_dark = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_brown.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green2.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_focus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_focus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_unfocus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_unfocus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue1 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue1.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green2.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_yellow.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_grey = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_grey.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_red.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_white.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_8b = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_8b.png", "/myfont/tahoma_8b", -1);
			mFont.number_yellow = new mFont(" 0123456789+-", "/myfont/number_yellow.png", "/myfont/number", 0);
			mFont.number_red = new mFont(" 0123456789+-", "/myfont/number_red.png", "/myfont/number", 0);
			mFont.number_green = new mFont(" 0123456789+-", "/myfont/number_green.png", "/myfont/number", 0);
			mFont.number_gray = new mFont(" 0123456789+-", "/myfont/number_gray.png", "/myfont/number", 0);
			mFont.number_orange = new mFont(" 0123456789+-", "/myfont/number_orange.png", "/myfont/number", 0);
			mFont.bigNumber_red = mFont.number_red;
			mFont.bigNumber_While = mFont.tahoma_7b_white;
			mFont.bigNumber_yellow = mFont.number_yellow;
			mFont.bigNumber_green = mFont.number_green;
			mFont.bigNumber_orange = mFont.number_orange;
			mFont.bigNumber_blue = mFont.tahoma_7_blue1;
			mFont.nameFontRed = mFont.tahoma_7_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
		}
		else
		{
			mFont.gI = new mFont(0);
			mFont.tahoma_7b_red = new mFont(1);
			mFont.tahoma_7b_blue = new mFont(2);
			mFont.tahoma_7b_white = new mFont(3);
			mFont.tahoma_7b_yellow = new mFont(4);
			mFont.tahoma_7b_yellowSmall = new mFont(4);
			mFont.tahoma_7b_dark = new mFont(5);
			mFont.tahoma_7b_green2 = new mFont(6);
			mFont.tahoma_7b_green = new mFont(7);
			mFont.tahoma_7b_focus = new mFont(8);
			mFont.tahoma_7b_unfocus = new mFont(9);
			mFont.tahoma_7 = new mFont(10);
			mFont.tahoma_7_blue1 = new mFont(11);
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
			mFont.tahoma_7_green2 = new mFont(12);
			mFont.tahoma_7_yellow = new mFont(13);
			mFont.tahoma_7_grey = new mFont(14);
			mFont.tahoma_7_red = new mFont(15);
			mFont.tahoma_7_blue = new mFont(16);
			mFont.tahoma_7_green = new mFont(17);
			mFont.tahoma_7_white = new mFont(18);
			mFont.tahoma_8b = new mFont(19);
			mFont.number_yellow = new mFont(20);
			mFont.number_red = new mFont(21);
			mFont.number_green = new mFont(22);
			mFont.number_gray = new mFont(23);
			mFont.number_orange = new mFont(24);
			mFont.bigNumber_red = new mFont(25);
			mFont.bigNumber_yellow = new mFont(26);
			mFont.bigNumber_green = new mFont(27);
			mFont.bigNumber_While = new mFont(28);
			mFont.bigNumber_blue = new mFont(29);
			mFont.bigNumber_orange = new mFont(30);
			mFont.bigNumber_black = new mFont(31);
			mFont.nameFontRed = mFont.tahoma_7b_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.yAddFont = 1;
			if (mGraphics.zoomLevel == 1)
			{
				mFont.yAddFont = -3;
			}
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x000043FB File Offset: 0x000025FB
	public void setHeight(int height)
	{
		this.height = height;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000E014 File Offset: 0x0000C214
	public Color setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float b = (float)num / 256f;
		float g = (float)num2 / 256f;
		float r = (float)num3 / 256f;
		Color result = new Color(r, g, b);
		return result;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000E06C File Offset: 0x0000C26C
	public Color bigColor(int id)
	{
		Color[] array = new Color[]
		{
			Color.red,
			Color.yellow,
			Color.green,
			Color.white,
			this.setColor(40404),
			Color.red,
			Color.black
		};
		return array[id - 25];
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00004404 File Offset: 0x00002604
	public void setColorByID(int ID)
	{
		this.color1 = this.setColor(mFont.colorJava[ID]);
		this.color2 = this.setColor(mFont.colorJava[ID]);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000E10C File Offset: 0x0000C30C
	public void setTypePaint(mGraphics g, string st, int x, int y, int align, sbyte idFont)
	{
		sbyte b = this.id;
		if ((int)idFont > 0)
		{
			b = idFont;
		}
		x--;
		if ((int)this.id > 24)
		{
			Color[] array = new Color[]
			{
				this.setColor(6029312),
				this.setColor(7169025),
				this.setColor(7680),
				this.setColor(0),
				this.setColor(9264),
				this.setColor(6029312)
			};
			this.color1 = array[(int)this.id - 25];
			this.color2 = array[(int)this.id - 25];
			this._drawString(g, st, x + 1, y, align);
			this._drawString(g, st, x - 1, y, align);
			this._drawString(g, st, x, y - 1, align);
			this._drawString(g, st, x, y + 1, align);
			this._drawString(g, st, x + 1, y + 1, align);
			this._drawString(g, st, x + 1, y - 1, align);
			this._drawString(g, st, x - 1, y - 1, align);
			this._drawString(g, st, x - 1, y + 1, align);
			this.color1 = this.bigColor((int)this.id);
			this.color2 = this.bigColor((int)this.id);
		}
		else
		{
			this.setColorByID((int)b);
		}
		this._drawString(g, st, x, y - this.yAdd, align);
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000442C File Offset: 0x0000262C
	public Color setColorFont(sbyte id)
	{
		return this.setColor(mFont.colorJava[(int)id]);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000E2CC File Offset: 0x0000C4CC
	public void drawString(mGraphics g, string st, int x, int y, int align)
	{
		if (mGraphics.zoomLevel == 1)
		{
			int length = st.Length;
			int num;
			if (align == 0)
			{
				num = x;
			}
			else if (align == 1)
			{
				num = x - this.getWidth(st);
			}
			else
			{
				num = x - (this.getWidth(st) >> 1);
			}
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i] + string.Empty);
				if (num2 == -1)
				{
					num2 = 0;
				}
				if (num2 > -1)
				{
					int x2 = this.fImages[num2][0];
					int num3 = this.fImages[num2][1];
					int w = this.fImages[num2][2];
					int num4 = this.fImages[num2][3];
					if (num3 + num4 > this.imgFont.texture.height)
					{
						num3 -= this.imgFont.texture.height;
						x2 = this.imgFont.texture.width / 2;
					}
					g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
		}
		else
		{
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000443C File Offset: 0x0000263C
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
	{
		if (mGraphics.zoomLevel == 1)
		{
			this.drawString(g, st, x, y, align);
		}
		else
		{
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00004469 File Offset: 0x00002669
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
	{
		if (mGraphics.zoomLevel == 1)
		{
			this.drawString(g, st, x, y, align, font2);
		}
		else
		{
			this.drawStringBd(g, st, x, y, align, font2);
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000E414 File Offset: 0x0000C614
	public void drawStringBd(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		this.setTypePaint(g, st, x - 1, y - 1, align, 0);
		this.setTypePaint(g, st, x - 1, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y - 1, align, 0);
		this.setTypePaint(g, st, x + 1, y + 1, align, 0);
		this.setTypePaint(g, st, x, y - 1, align, 0);
		this.setTypePaint(g, st, x, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y, align, 0);
		this.setTypePaint(g, st, x - 1, y, align, 0);
		this.setTypePaint(g, st, x, y, align, 0);
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
	public void drawString(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		if (mGraphics.zoomLevel == 1)
		{
			int length = st.Length;
			int num;
			if (align == 0)
			{
				num = x;
			}
			else if (align == 1)
			{
				num = x - this.getWidth(st);
			}
			else
			{
				num = x - (this.getWidth(st) >> 1);
			}
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i]);
				if (num2 == -1)
				{
					num2 = 0;
				}
				if (num2 > -1)
				{
					int x2 = this.fImages[num2][0];
					int num3 = this.fImages[num2][1];
					int w = this.fImages[num2][2];
					int num4 = this.fImages[num2][3];
					if (num3 + num4 > this.imgFont.texture.height)
					{
						num3 -= this.imgFont.texture.height;
						x2 = this.imgFont.texture.width / 2;
					}
					if (!GameCanvas.lowGraphic && font != null)
					{
						g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num + 1, y, 20);
						g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num, y + 1, 20);
					}
					g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
		}
		else
		{
			this.setTypePaint(g, st, x, y + 1, align, font.id);
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000E654 File Offset: 0x0000C854
	public MyVector splitFontVector(string src, int lineWidth)
	{
		MyVector myVector = new MyVector();
		string text = string.Empty;
		for (int i = 0; i < src.Length; i++)
		{
			if (src[i] == '\n' || src[i] == '\b')
			{
				myVector.addElement(text);
				text = string.Empty;
			}
			else
			{
				text += src[i];
				if (this.getWidth(text) > lineWidth)
				{
					int j;
					for (j = text.Length - 1; j >= 0; j--)
					{
						if (text[j] == ' ')
						{
							break;
						}
					}
					if (j < 0)
					{
						j = text.Length - 1;
					}
					myVector.addElement(text.Substring(0, j));
					i = i - (text.Length - j) + 1;
					text = string.Empty;
				}
				if (i == src.Length - 1 && !text.Trim().Equals(string.Empty))
				{
					myVector.addElement(text);
				}
			}
		}
		return myVector;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000E75C File Offset: 0x0000C95C
	public string splitFirst(string str)
	{
		string text = string.Empty;
		bool flag = false;
		for (int i = 0; i < str.Length; i++)
		{
			if (!flag)
			{
				string text2 = str.Substring(i);
				if (this.compare(text2, " "))
				{
					text = text + str[i] + "-";
				}
				else
				{
					text += text2;
				}
				flag = true;
			}
			else if (str[i] == ' ')
			{
				flag = false;
			}
		}
		return text;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
	public string[] splitStrInLine(string src, int lineWidth)
	{
		ArrayList arrayList = this.splitStrInLineA(src, lineWidth);
		string[] array = new string[arrayList.Count];
		for (int i = 0; i < arrayList.Count; i++)
		{
			array[i] = (string)arrayList[i];
		}
		return array;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000E830 File Offset: 0x0000CA30
	public ArrayList splitStrInLineA(string src, int lineWidth)
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		int num2 = 0;
		int length = src.Length;
		if (length < 5)
		{
			arrayList.Add(src);
			return arrayList;
		}
		string text = string.Empty;
		try
		{
			for (;;)
			{
				while (this.getWidthNotExactOf(text) < lineWidth)
				{
					text += src[num2];
					num2++;
					if (src[num2] == '\n')
					{
						break;
					}
					if (num2 >= length - 1)
					{
						num2 = length - 1;
						break;
					}
				}
				if (num2 != length - 1 && src[num2 + 1] != ' ')
				{
					int num3 = num2;
					while (src[num2 + 1] != '\n')
					{
						if (src[num2 + 1] != ' ' || src[num2] == ' ')
						{
							if (num2 != num)
							{
								num2--;
								continue;
							}
						}
						IL_E3:
						if (num2 == num)
						{
							num2 = num3;
							goto IL_ED;
						}
						goto IL_ED;
					}
					goto IL_E3;
				}
				IL_ED:
				string text2 = src.Substring(num, num2 + 1 - num);
				if (text2[0] == '\n')
				{
					text2 = text2.Substring(1, text2.Length - 1);
				}
				if (text2[text2.Length - 1] == '\n')
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				arrayList.Add(text2);
				if (num2 == length - 1)
				{
					break;
				}
				num = num2 + 1;
				while (num != length - 1 && src[num] == ' ')
				{
					num++;
				}
				if (num == length - 1)
				{
					break;
				}
				num2 = num;
				text = string.Empty;
			}
		}
		catch (Exception ex)
		{
			Cout.LogWarning(string.Concat(new object[]
			{
				"EXCEPTION WHEN REAL SPLIT ",
				src,
				"\nend=",
				num2,
				"\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			arrayList.Add(src);
		}
		return arrayList;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000EA60 File Offset: 0x0000CC60
	public string[] splitFontArray(string src, int lineWidth)
	{
		MyVector myVector = this.splitFontVector(src, lineWidth);
		string[] array = new string[myVector.size()];
		for (int i = 0; i < myVector.size(); i++)
		{
			array[i] = (string)myVector.elementAt(i);
		}
		return array;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000EAAC File Offset: 0x0000CCAC
	public bool compare(string strSource, string str)
	{
		for (int i = 0; i < strSource.Length; i++)
		{
			if ((string.Empty + strSource[i]).Equals(str))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
	public int getWidth(string s)
	{
		if (mGraphics.zoomLevel == 1)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				int num2 = this.strFont.IndexOf(s[i]);
				if (num2 == -1)
				{
					num2 = 0;
				}
				num += this.fImages[num2][2] + this.space;
			}
			return num;
		}
		return this.getWidthExactOf(s);
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000EB60 File Offset: 0x0000CD60
	public int getWidthExactOf(string s)
	{
		int result;
		try
		{
			result = (int)new GUIStyle
			{
				font = this.myFont
			}.CalcSize(new GUIContent(s)).x / mGraphics.zoomLevel;
		}
		catch (Exception ex)
		{
			Cout.LogError(string.Concat(new string[]
			{
				"GET WIDTH OF ",
				s,
				" FAIL.\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			result = this.getWidthNotExactOf(s);
		}
		return result;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00004499 File Offset: 0x00002699
	public int getWidthNotExactOf(string s)
	{
		return s.Length * this.wO / mGraphics.zoomLevel;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000EC08 File Offset: 0x0000CE08
	public int getHeight()
	{
		if (mGraphics.zoomLevel == 1)
		{
			return this.height;
		}
		if (this.height > 0)
		{
			return this.height / mGraphics.zoomLevel;
		}
		GUIStyle guistyle = new GUIStyle();
		guistyle.font = this.myFont;
		try
		{
			this.height = (int)guistyle.CalcSize(new GUIContent("Adg")).y + 2;
		}
		catch (Exception ex)
		{
			Cout.LogError("FAIL GET HEIGHT " + ex.StackTrace);
			this.height = 20;
		}
		return this.height / mGraphics.zoomLevel;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
	public void _drawString(mGraphics g, string st, int x0, int y0, int align)
	{
		y0 += mFont.yAddFont;
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.font = this.myFont;
		float num = 0f;
		float num2 = 0f;
		switch (align)
		{
		case 0:
			num = (float)x0;
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperLeft;
			break;
		case 1:
			num = (float)(x0 - GameCanvas.w);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperRight;
			break;
		case 2:
		case 3:
			num = (float)(x0 - GameCanvas.w / 2);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperCenter;
			break;
		}
		guistyle.normal.textColor = this.color1;
		g.drawString(st, (int)num, (int)num2, guistyle);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000ED7C File Offset: 0x0000CF7C
	public static string[] splitStringSv(string _text, string _searchStr)
	{
		int num = 0;
		int startIndex = 0;
		int length = _searchStr.Length;
		int num2 = _text.IndexOf(_searchStr, startIndex);
		while (num2 != -1)
		{
			startIndex = num2 + length;
			num2 = _text.IndexOf(_searchStr, startIndex);
			num++;
		}
		string[] array = new string[num + 1];
		int num3 = _text.IndexOf(_searchStr);
		int num4 = 0;
		int num5 = 0;
		while (num3 != -1)
		{
			array[num5] = _text.Substring(num4, num3 - num4);
			num4 = num3 + length;
			num3 = _text.IndexOf(_searchStr, num4);
			num5++;
		}
		array[num5] = _text.Substring(num4, _text.Length - num4);
		return array;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000044AE File Offset: 0x000026AE
	public void reloadImage()
	{
		if (mGraphics.zoomLevel == 1)
		{
			this.imgFont = GameCanvas.loadImage(this.pathImage);
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00003584 File Offset: 0x00001784
	public void freeImage()
	{
	}

	// Token: 0x0400014E RID: 334
	public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

	// Token: 0x0400014F RID: 335
	public static int LEFT = 0;

	// Token: 0x04000150 RID: 336
	public static int RIGHT = 1;

	// Token: 0x04000151 RID: 337
	public static int CENTER = 2;

	// Token: 0x04000152 RID: 338
	public static int RED = 0;

	// Token: 0x04000153 RID: 339
	public static int YELLOW = 1;

	// Token: 0x04000154 RID: 340
	public static int GREEN = 2;

	// Token: 0x04000155 RID: 341
	public static int FATAL = 3;

	// Token: 0x04000156 RID: 342
	public static int MISS = 4;

	// Token: 0x04000157 RID: 343
	public static int ORANGE = 5;

	// Token: 0x04000158 RID: 344
	public static int ADDMONEY = 6;

	// Token: 0x04000159 RID: 345
	public static int MISS_ME = 7;

	// Token: 0x0400015A RID: 346
	public static int FATAL_ME = 8;

	// Token: 0x0400015B RID: 347
	public static int HP = 9;

	// Token: 0x0400015C RID: 348
	public static int MP = 10;

	// Token: 0x0400015D RID: 349
	private int space;

	// Token: 0x0400015E RID: 350
	private Image imgFont;

	// Token: 0x0400015F RID: 351
	private string strFont;

	// Token: 0x04000160 RID: 352
	private int[][] fImages;

	// Token: 0x04000161 RID: 353
	public static int yAddFont;

	// Token: 0x04000162 RID: 354
	public static int[] colorJava = new int[]
	{
		0,
		16711680,
		6520319,
		16777215,
		16755200,
		5449989,
		21285,
		52224,
		7386228,
		16771788,
		0,
		65535,
		21285,
		16776960,
		5592405,
		16742263,
		33023,
		8701737,
		15723503,
		7999781,
		16768815,
		14961237,
		4124899,
		4671303,
		16096312,
		16711680,
		16755200,
		52224,
		16777215,
		6520319,
		16096312
	};

	// Token: 0x04000163 RID: 355
	public static mFont gI;

	// Token: 0x04000164 RID: 356
	public static mFont tahoma_7b_red;

	// Token: 0x04000165 RID: 357
	public static mFont tahoma_7b_blue;

	// Token: 0x04000166 RID: 358
	public static mFont tahoma_7b_white;

	// Token: 0x04000167 RID: 359
	public static mFont tahoma_7b_yellow;

	// Token: 0x04000168 RID: 360
	public static mFont tahoma_7b_yellowSmall;

	// Token: 0x04000169 RID: 361
	public static mFont tahoma_7b_dark;

	// Token: 0x0400016A RID: 362
	public static mFont tahoma_7b_green2;

	// Token: 0x0400016B RID: 363
	public static mFont tahoma_7b_green;

	// Token: 0x0400016C RID: 364
	public static mFont tahoma_7b_focus;

	// Token: 0x0400016D RID: 365
	public static mFont tahoma_7b_unfocus;

	// Token: 0x0400016E RID: 366
	public static mFont tahoma_7;

	// Token: 0x0400016F RID: 367
	public static mFont tahoma_7_blue1;

	// Token: 0x04000170 RID: 368
	public static mFont tahoma_7_blue1Small;

	// Token: 0x04000171 RID: 369
	public static mFont tahoma_7_green2;

	// Token: 0x04000172 RID: 370
	public static mFont tahoma_7_yellow;

	// Token: 0x04000173 RID: 371
	public static mFont tahoma_7_grey;

	// Token: 0x04000174 RID: 372
	public static mFont tahoma_7_red;

	// Token: 0x04000175 RID: 373
	public static mFont tahoma_7_blue;

	// Token: 0x04000176 RID: 374
	public static mFont tahoma_7_green;

	// Token: 0x04000177 RID: 375
	public static mFont tahoma_7_white;

	// Token: 0x04000178 RID: 376
	public static mFont tahoma_8b;

	// Token: 0x04000179 RID: 377
	public static mFont number_yellow;

	// Token: 0x0400017A RID: 378
	public static mFont number_red;

	// Token: 0x0400017B RID: 379
	public static mFont number_green;

	// Token: 0x0400017C RID: 380
	public static mFont number_gray;

	// Token: 0x0400017D RID: 381
	public static mFont number_orange;

	// Token: 0x0400017E RID: 382
	public static mFont bigNumber_red;

	// Token: 0x0400017F RID: 383
	public static mFont bigNumber_While;

	// Token: 0x04000180 RID: 384
	public static mFont bigNumber_yellow;

	// Token: 0x04000181 RID: 385
	public static mFont bigNumber_green;

	// Token: 0x04000182 RID: 386
	public static mFont bigNumber_orange;

	// Token: 0x04000183 RID: 387
	public static mFont bigNumber_blue;

	// Token: 0x04000184 RID: 388
	public static mFont bigNumber_black;

	// Token: 0x04000185 RID: 389
	public static mFont nameFontRed;

	// Token: 0x04000186 RID: 390
	public static mFont nameFontYellow;

	// Token: 0x04000187 RID: 391
	public static mFont nameFontGreen;

	// Token: 0x04000188 RID: 392
	public static mFont tahoma_7_greySmall;

	// Token: 0x04000189 RID: 393
	public static mFont tahoma_7b_yellowSmall2;

	// Token: 0x0400018A RID: 394
	public static mFont tahoma_7b_green2Small;

	// Token: 0x0400018B RID: 395
	public static mFont tahoma_7_whiteSmall;

	// Token: 0x0400018C RID: 396
	public static mFont tahoma_7b_greenSmall;

	// Token: 0x0400018D RID: 397
	public Font myFont;

	// Token: 0x0400018E RID: 398
	private int height;

	// Token: 0x0400018F RID: 399
	private int wO;

	// Token: 0x04000190 RID: 400
	public Color color1 = Color.white;

	// Token: 0x04000191 RID: 401
	public Color color2 = Color.gray;

	// Token: 0x04000192 RID: 402
	public sbyte id;

	// Token: 0x04000193 RID: 403
	public int fstyle;

	// Token: 0x04000194 RID: 404
	public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

	// Token: 0x04000195 RID: 405
	public string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

	// Token: 0x04000196 RID: 406
	private int yAdd;

	// Token: 0x04000197 RID: 407
	private string pathImage;
}
