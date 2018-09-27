using UnityEngine;
using System.Collections;

public class VectorUtils 
{
	public static float Angle(Vector3 v3OldDir, Vector3 v3Up, Vector3 v3NewDir)
	{		 
		Vector3 v3Right = Vector3.Cross(v3Up, v3OldDir);

		float fAngle = Vector3.Dot(v3OldDir, v3NewDir);
		fAngle = Mathf.Acos(fAngle) * Mathf.Rad2Deg;

		float fSign = (Vector3.Dot(v3NewDir, v3Right) > 0.0f) ? 1.0f: -1.0f;

		return (fSign * fAngle);	
	}

	public static bool IsAngleWithinThreshold(Vector3 v3Dir1, Vector3 v3Up, Vector3 v3Dir2, int iAngleThreshold)
	{
		float fAngle = VectorUtils.Angle(v3Dir1, v3Up, v3Dir2);
		
		return (Mathf.Abs(fAngle) <= iAngleThreshold);
	}
}
