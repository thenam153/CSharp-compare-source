using System;

// Token: 0x02000035 RID: 53
public class EffecMn
{
	// Token: 0x06000241 RID: 577 RVA: 0x0000490D File Offset: 0x00002B0D
	public static void addEff(Effect me)
	{
		EffecMn.vEff.addElement(me);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000491A File Offset: 0x00002B1A
	public static void removeEff(int id)
	{
		if (EffecMn.getEffById(id) != null)
		{
			EffecMn.vEff.removeElement(EffecMn.getEffById(id));
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00012644 File Offset: 0x00010844
	public static Effect getEffById(int id)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			Effect effect = (Effect)EffecMn.vEff.elementAt(i);
			if (effect.effId == id)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0001268C File Offset: 0x0001088C
	public static void paintBackGroundUnderLayer(mGraphics g, int x, int y, int layer)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == -layer)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paintUnderBackground(g, x, y);
			}
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x000126E8 File Offset: 0x000108E8
	public static void paintLayer1(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 1)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00012744 File Offset: 0x00010944
	public static void paintLayer2(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 2)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x000127A0 File Offset: 0x000109A0
	public static void paintLayer3(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 3)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x000127FC File Offset: 0x000109FC
	public static void paintLayer4(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 4)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00012858 File Offset: 0x00010A58
	public static void update()
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			((Effect)EffecMn.vEff.elementAt(i)).update();
		}
	}

	// Token: 0x04000270 RID: 624
	public static MyVector vEff = new MyVector();
}
