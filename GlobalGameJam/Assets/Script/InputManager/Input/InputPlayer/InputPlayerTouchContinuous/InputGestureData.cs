using UnityEngine;
using System.Collections;

public class InputGestureData 
{
	public static void GetGestureData(int iGestureIndex, out Vector3 vDirection, out int iAngleEpsilon)
	{
		if(iGestureIndex >= (int)AtomicGestureType.COUNT)
		{
			//Excede the array limit..
			Debug.LogError("Wrong iGestureIndex " + iGestureIndex);
			vDirection = Vector3.zero;
			iAngleEpsilon = 0;

			return;
		}

		vDirection = s_avAtomicGestureDirection[iGestureIndex];
		iAngleEpsilon = s_aiAngleTrashold[iGestureIndex];
	}

	public enum AtomicGestureType
	{
		UP = 0,
		DOWN,
		LEFT,
		RIGTH,
		COUNT
	};

	private static Vector3[] 	s_avAtomicGestureDirection = new Vector3[] { new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f), new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f)}; 
	private static int[] 		s_aiAngleTrashold = new int[] { 20, 20, 20, 20 };
}
