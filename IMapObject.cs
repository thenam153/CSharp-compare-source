using System;

// Token: 0x0200009C RID: 156
public interface IMapObject
{
	// Token: 0x060006D8 RID: 1752
	int getX();

	// Token: 0x060006D9 RID: 1753
	int getY();

	// Token: 0x060006DA RID: 1754
	int getW();

	// Token: 0x060006DB RID: 1755
	int getH();

	// Token: 0x060006DC RID: 1756
	void stopMoving();

	// Token: 0x060006DD RID: 1757
	bool isInvisible();
}
