using UnityEngine ;
using UnityEngine.UI ;

public class ToastDemo : MonoBehaviour {
   [TextArea (5, 20)] public string text ;

    [ContextMenu(nameof(ShowMessage1))]
   public void ShowMessage1 () {
      Toast.Show (text) ;
   }

    [ContextMenu(nameof(ShowMessage2))]
	public void ShowMessage2 () {
      Toast.Show ("Hello GameDev, How are you?", 3f, ToastColor.Green) ;
   }

    [ContextMenu(nameof(ShowMessage3))]
	public void ShowMessage3 () {
      Toast.Show ("This is another toast message, just ignore it :D", 4f, new Color (1f, .4f, 0f)) ;
   }

   [ContextMenu(nameof(ShowMessageTopLeft))]
	public void ShowMessageTopLeft () {
      Toast.Show ("Top Left Toast", 3f, ToastColor.Magenta, ToastPosition.TopLeft) ;
   }

   [ContextMenu(nameof(ShowMessageMiddle))]
	public void ShowMessageMiddle () {
      Toast.Show ("<b>Middle</b> Toast", 3f, ToastColor.Blue, ToastPosition.MiddleCenter) ;
   }

    [ContextMenu(nameof(DismissToast))]
	public void DismissToast () {
      Toast.Dismiss () ;
   }
}
