using System;

// Token: 0x02000037 RID: 55
public abstract class Effect2
{
	// Token: 0x06000258 RID: 600 RVA: 0x00003584 File Offset: 0x00001784
	public virtual void update()
	{
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00003584 File Offset: 0x00001784
	public virtual void paint(mGraphics g)
	{
	}

	// Token: 0x040002AA RID: 682
	public static MyVector vEffect3 = new MyVector();

	// Token: 0x040002AB RID: 683
	public static MyVector vEffect2 = new MyVector();

	// Token: 0x040002AC RID: 684
	public static MyVector vRemoveEffect2 = new MyVector();

	// Token: 0x040002AD RID: 685
	public static MyVector vEffect2Outside = new MyVector();

	// Token: 0x040002AE RID: 686
	public static MyVector vAnimateEffect = new MyVector();

	// Token: 0x040002AF RID: 687
	public static MyVector vEffectFeet = new MyVector();
}
