using System.Numerics;
using Baracuda.Monitoring;
using Quartzified.EditorAttributes;
using UnityEngine;

public class Tester : SingletonBehaviour<Tester>
{
	[Tag]
	public string tag;

	[Layer]
	public int layer;

	[Range(-100,100)]
	public float slider;

	[Monitor]
	private float val;

	/*private IEnumerator Start()
	{
		/*Fader.Instance.OnFadeFromBlackComplete.AddListener(() => Debug.Log("OnFadeFromBlackComplete"));
		Fader.Instance.OnFadeToBlackComplete.AddListener(() => Debug.Log("OnFadeToBlackComplete"));

		Fader.Instance.FadeFromBlack();
		yield return new WaitForSeconds(2.0f);
		Fader.Instance.FadeToBlack();

		ApiHelper.Get("https://www.google.com", result => { Debug.Log(result); }, error => { Debug.Log(error); },"");#1#
	}*/

	private BigInteger num = BigInteger.Zero;

	private void Update()
	{
		val += slider;
		/*num += 100000000;
		Debug.Log($"{num.ToStringAbbreviated("0")}");*/
	}
}