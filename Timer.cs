using System;

// Token: 0x020000B5 RID: 181
public class Timer
{
	// Token: 0x060008D4 RID: 2260 RVA: 0x0000731D File Offset: 0x0000551D
	public static void setTimer(IActionListener actionListener, int action, long timeEllapse)
	{
		Timer.timeListener = actionListener;
		Timer.idAction = action;
		Timer.timeExecute = mSystem.currentTimeMillis() + timeEllapse;
		Timer.isON = true;
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0007FC68 File Offset: 0x0007DE68
	public static void update()
	{
		long num = mSystem.currentTimeMillis();
		if (Timer.isON && num > Timer.timeExecute)
		{
			Timer.isON = false;
			try
			{
				if (Timer.idAction > 0)
				{
					GameScr.gI().actionPerform(Timer.idAction, null);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x04001061 RID: 4193
	public static IActionListener timeListener;

	// Token: 0x04001062 RID: 4194
	public static int idAction;

	// Token: 0x04001063 RID: 4195
	public static long timeExecute;

	// Token: 0x04001064 RID: 4196
	public static bool isON;
}
