using UnityEngine;

public static class CameraExtensions
{
	public static bool WorldRectInView(this Camera camera, Rect worldRect, float depth)
	{
		return worldRect.Intersects(camera.GetRect(depth));
	}

	public static bool WorldRectInView(this Camera camera, Rect worldRect)
	{
		return camera.WorldRectInView(worldRect, 0f);
	}

	public static bool PointInView(this Camera camera, Vector3 point)
	{
		return camera.GetRect(0f).Contains(point);
	}

	public static Rect GetRect(this Camera camera, float depth)
	{
		var z = depth - camera.transform.position.z;
		Vector2 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, z));
		Vector2 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, z));
		return Rect.MinMaxRect(vector.x, vector.y, vector2.x, vector2.y);
	}

	public static void ShowLayer(this Camera cam, int layerIndex) => cam.cullingMask |= (1 << layerIndex);

	public static void ShowLayer(this Camera cam, string layerName) => ShowLayer(cam, LayerMask.NameToLayer(layerName));

	public static void HideLayer(this Camera cam, int layerIndex) => cam.cullingMask &= ~(1 << layerIndex);

	public static void HideLayer(this Camera cam, string layerName) => HideLayer(cam, LayerMask.NameToLayer(layerName));

	public static void ToggleLayerVisibility(this Camera cam, int layerIndex) => cam.cullingMask ^= (1 << layerIndex);

	public static void ToggleLayerVisibility(this Camera cam, string layerName) => ToggleLayerVisibility(cam, LayerMask.NameToLayer(layerName));

	public static bool IsLayerShown(this Camera cam, int layerIndex) => (cam.cullingMask & (1 << layerIndex)) > 0;

	public static bool IsLayerShown(this Camera cam, string layerName) => IsLayerShown(cam, LayerMask.NameToLayer(layerName));

	public static void SetLayerVisibility(this Camera cam, int layerIndex, bool show)
	{
		if (show) cam.ShowLayer(layerIndex);
		else cam.HideLayer(layerIndex);
	}

	public static void SetLayerVisibility(this Camera cam, string layerName, bool show) => cam.SetLayerVisibility(LayerMask.NameToLayer(layerName), show);

	/// <summary>
	/// Gets a value indicating whether the specified <paramref name="worldPosition"/> is inside the frustum of the <paramref name="camera"/>.
	/// </summary>
	public static bool CanSee(this Camera camera, Vector3 worldPosition)
	{
		var viewportPosition = camera.WorldToViewportPoint(worldPosition);
		return viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
			   viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
			   viewportPosition.z > 0;
	}
}