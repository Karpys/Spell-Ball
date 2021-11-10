using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
	public enum Type
	{
		NORMAL,
		FIRE,
		WATER,
		GRASS,
		ELECTRICK,
	}

	public string name;
	public int degat;
	public Type type;
}

