using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
	[SerializeField]
	protected float interval = 1f;

	/**** 弾幕パターンを派生クラスで実装してください ****/
	protected abstract IEnumerator shot();
}
