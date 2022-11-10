using System.Collections;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer.Samples
{
	/// <summary>
	/// This script will invoke slice method of IBzSliceableNoRepeat interface if knife slices this GameObject.
	/// The script must be attached to a GameObject that have rigidbody on it and
	/// IBzSliceable implementation in one of its parent.
	/// </summary>
	[DisallowMultipleComponent]
	public class KnifeSliceableAsync : MonoBehaviour
	{
		[SerializeField] private GameObject bloodForce;
		
		IBzSliceableNoRepeat _sliceableAsync;

		void Start()
		{
			_sliceableAsync = GetComponentInParent<IBzSliceableNoRepeat>();
		}

		void OnTriggerEnter(Collider other)
		{
			Rigidbody tempRigidbody = gameObject.GetComponent<Rigidbody>();

			var knife = other.gameObject.GetComponent<BzKnife>();
			if (knife == null)
				return;
			tempRigidbody.useGravity = true;
			tempRigidbody.AddForce(new Vector3(-100,250,10) *5);

			StartCoroutine(Slice(knife));
		}

		private IEnumerator Slice(BzKnife knife)
		{
			// The call from OnTriggerEnter, so some object positions are wrong.
			// We have to wait for next frame to work with correct values
			yield return null;

			Vector3 point = GetCollisionPoint(knife);
			Vector3 normal = Vector3.Cross(knife.MoveDirection, knife.BladeDirection);
			Plane plane = new Plane(normal, point);

            GameObject _blood = Instantiate(bloodForce, point, Quaternion.identity);

            if (_sliceableAsync != null)
			{
				_sliceableAsync.Slice(plane, knife.SliceID, null);
			}

			yield return new WaitForSeconds(1f);
            Destroy(_blood);
        }

		private Vector3 GetCollisionPoint(BzKnife knife)
		{
			Vector3 distToObject = transform.position - knife.Origin;
			Vector3 proj = Vector3.Project(distToObject, knife.BladeDirection);

			Vector3 collisionPoint = knife.Origin + proj;
			return collisionPoint;
		}
	}
}