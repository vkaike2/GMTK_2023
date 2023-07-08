using UnityEngine;

public static class WeaponUtils
{
    public static Vector2 GetMouseNormalizedDirection(Transform transform)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        return direction.normalized;
    }

    public static Vector2 GetNormalizedDirection(Transform transform, Vector3 position)
    {
        Vector2 direction = position - transform.position;
        return direction.normalized;
    }
}
