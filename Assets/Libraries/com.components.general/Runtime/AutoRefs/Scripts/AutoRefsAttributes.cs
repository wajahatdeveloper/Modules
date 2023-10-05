using System;

public enum AutoRefTargetType
{
	Undefined = 0,
	Self = 1,
	Parent = 2,
	Children = 4,
	Siblings = 8,
	Scene = 16,
	NamedGameObjects = 32,
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoRefAttribute : Attribute
{
	public AutoRefTargetType m_eTargetType;
	public string[] m_astrGameObjectNames = null;

	public AutoRefAttribute()
	{
		m_eTargetType = AutoRefTargetType.Self;
	}

	public AutoRefAttribute(AutoRefTargetType eSearchType, string[] astrNamedGameObjects = null)
	{
		m_eTargetType = eSearchType;
		m_astrGameObjectNames = astrNamedGameObjects;
	}
}