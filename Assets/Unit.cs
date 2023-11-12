using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
	public Transform target;
	float speed = 5;
	Vector3[] path;
	int targetIndex;

	public void MoveTo(Vector3 targetPos)
	{
		PathManager.RequestPath(transform.position,targetPos, OnPathFound);
	}
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			if (gameObject.activeSelf) 
			{
				StopCoroutine("FollowPath");
				StartCoroutine("FollowPath");
        	}
		}
	}

	IEnumerator FollowPath() {
		if (path == null || path.Length == 0) {
            yield break; // No path to follow, exit the coroutine
        }
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;

		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.cyan;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}