using UnityEngine;

public class CameraWallGenerator : MonoBehaviour
{
    [Header("ï«Prefab")]
    [SerializeField] private GameObject wallPrefab;

    [Header("ï«ÇÃå˙Ç≥")]
    [SerializeField] private float thickness = 1f;

    private void Start()
    {
        CreateWalls();
    }

    private void CreateWalls()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        Vector2 center = cam.transform.position;

        // è„
        CreateWall(
            new Vector2(center.x, center.y + height / 2f + thickness / 2f),
            new Vector2(width + thickness * 2f, thickness)
        );

        // â∫
        CreateWall(
            new Vector2(center.x, center.y - height / 2f - thickness / 2f),
            new Vector2(width + thickness * 2f, thickness)
        );

        // ç∂
        CreateWall(
            new Vector2(center.x - width / 2f - thickness / 2f, center.y),
            new Vector2(thickness, height)
        );

        // âE
        CreateWall(
            new Vector2(center.x + width / 2f + thickness / 2f, center.y),
            new Vector2(thickness, height)
        );
    }

    private void CreateWall(Vector2 pos, Vector2 scale)
    {
        GameObject wall = Instantiate(wallPrefab, pos, Quaternion.identity);

        wall.transform.localScale = scale;
    }
}