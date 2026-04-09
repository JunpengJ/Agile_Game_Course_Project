using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       
    public Vector2 deadZoneSize = new Vector2(2f, 1.5f);
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 5f;
    public Tilemap tilemap;
    private Vector3 targetPos;
    private Vector3 cameraStartPos;
    private float autoMinX, autoMaxX, autoMinY, autoMaxY;
    void Start()
    {
        if (player == null) return ;
        Bounds bounds = tilemap.GetComponent<TilemapRenderer>().bounds;
        autoMinX = bounds.min.x;
        autoMaxX = bounds.max.x;
        autoMinY = bounds.min.y;
        autoMaxY = bounds.max.y;
        cameraStartPos = transform.position;
        targetPos = cameraStartPos;
    }

    void LateUpdate()
    {
        if (player == null) return ;
        Vector3 playerOffset = player.position + offset - targetPos;
        if (Mathf.Abs(playerOffset.x) > deadZoneSize.x / 2f || Mathf.Abs(playerOffset.y) > deadZoneSize.y / 2f)
        {
            Vector3 newTarget = targetPos;
            if (playerOffset.x > deadZoneSize.x / 2f)
                newTarget.x += playerOffset.x - deadZoneSize.x / 2f;
            else if (playerOffset.x < -deadZoneSize.x / 2f)
                newTarget.x += playerOffset.x + deadZoneSize.x / 2f;

            if (playerOffset.y > deadZoneSize.y / 2f)
                newTarget.y += playerOffset.y - deadZoneSize.y / 2f;
            else if (playerOffset.y < -deadZoneSize.y / 2f)
                newTarget.y += playerOffset.y + deadZoneSize.y / 2f;
            targetPos = newTarget;
        }

        if (tilemap != null)
        {
            float cameraHalfheight = Camera.main.orthographicSize;
            float cameraHalfwidth = cameraHalfheight * Camera.main.aspect;
            targetPos.x = Mathf.Clamp(targetPos.x, autoMinX + cameraHalfwidth, autoMaxX - cameraHalfwidth);
            targetPos.y = Mathf.Clamp(targetPos.y, autoMinY + cameraHalfheight, autoMaxY - cameraHalfheight);
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}