using System;

// Token: 0x02000074 RID: 116
public class Skill
{
	// Token: 0x060003C3 RID: 963 RVA: 0x0001DD84 File Offset: 0x0001BF84
	public string strTimeReplay()
	{
		if (this.coolDown % 1000 == 0)
		{
			return this.coolDown / 1000 + string.Empty;
		}
		int num = this.coolDown % 1000;
		return this.coolDown / 1000 + "." + ((num % 100 != 0) ? (num / 10) : (num / 100));
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0001DE04 File Offset: 0x0001C004
	public void paint(int x, int y, mGraphics g)
	{
		SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
		long num = mSystem.currentTimeMillis();
		long num2 = num - this.lastTimeUseThisSkill;
		if (num2 < (long)this.coolDown)
		{
			g.setColor(2721889, 0.7f);
			if (this.paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2)
			{
				g.setColor(876862);
			}
			int num3 = (int)(num2 * 20L / (long)this.coolDown);
			g.fillRect(x - 10, y - 10 + num3, 20, 20 - num3);
		}
		else
		{
			this.paintCanNotUseSkill = false;
		}
	}

	// Token: 0x0400066A RID: 1642
	public const sbyte ATT_STAND = 0;

	// Token: 0x0400066B RID: 1643
	public const sbyte ATT_FLY = 1;

	// Token: 0x0400066C RID: 1644
	public const sbyte SKILL_AUTO_USE = 0;

	// Token: 0x0400066D RID: 1645
	public const sbyte SKILL_CLICK_USE_ATTACK = 1;

	// Token: 0x0400066E RID: 1646
	public const sbyte SKILL_CLICK_USE_BUFF = 2;

	// Token: 0x0400066F RID: 1647
	public const sbyte SKILL_CLICK_NPC = 3;

	// Token: 0x04000670 RID: 1648
	public const sbyte SKILL_CLICK_LIVE = 4;

	// Token: 0x04000671 RID: 1649
	public SkillTemplate template;

	// Token: 0x04000672 RID: 1650
	public short skillId;

	// Token: 0x04000673 RID: 1651
	public int point;

	// Token: 0x04000674 RID: 1652
	public long powRequire;

	// Token: 0x04000675 RID: 1653
	public int coolDown;

	// Token: 0x04000676 RID: 1654
	public long lastTimeUseThisSkill;

	// Token: 0x04000677 RID: 1655
	public int dx;

	// Token: 0x04000678 RID: 1656
	public int dy;

	// Token: 0x04000679 RID: 1657
	public int maxFight;

	// Token: 0x0400067A RID: 1658
	public int manaUse;

	// Token: 0x0400067B RID: 1659
	public SkillOption[] options;

	// Token: 0x0400067C RID: 1660
	public bool paintCanNotUseSkill;

	// Token: 0x0400067D RID: 1661
	public short damage;

	// Token: 0x0400067E RID: 1662
	public string moreInfo;

	// Token: 0x0400067F RID: 1663
	public short price;
}
