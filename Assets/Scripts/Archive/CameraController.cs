using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float currentPosYOffset;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        //Room Camera movement
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX,transform.position.y,transform.position.z), ref velocity, speed);

        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y+currentPosYOffset, transform.position.z);
        //x position flips (move more to left when facing left, vice versa)
        lookAhead = Mathf.Lerp(lookAhead ,(aheadDistance * player.localScale.x),Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
