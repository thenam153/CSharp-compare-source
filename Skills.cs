using System;

// Token: 0x0200007A RID: 122
public class Skills
{
	// Token: 0x060003D0 RID: 976 RVA: 0x00005569 File Offset: 0x00003769
	public static void add(Skill skill)
	{
		Skills.skills.put(skill.skillId, skill);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00005581 File Offset: 0x00003781
	public static Skill get(short skillId)
	{
		return (Skill)Skills.skills.get(skillId);
	}

	// Token: 0x040006A1 RID: 1697
	public static MyHashTable skills = new MyHashTable();
}
