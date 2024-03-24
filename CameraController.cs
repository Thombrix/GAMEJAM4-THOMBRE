using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] players; // Assign in the editor or find dynamically
    public float minFOV = 60f; // Minimum field of view
    public float maxFOV = 90f; // Maximum field of view
    public float maxDistance = 50f; // Maximum distance to trigger the max FOV

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        AdjustCameraFOV();
    }

    void AdjustCameraFOV()
    {
        float maxPlayerDistance = FindMaxDistanceBetweenPlayers();
        // Map the distance to the FOV range
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, maxPlayerDistance / maxDistance);
        cam.fieldOfView = Mathf.Clamp(targetFOV, minFOV, maxFOV);
    }

    float FindMaxDistanceBetweenPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Goober");

        if (players.Length < 2) return 0f;

        var bounds = new Bounds(players[0].transform.position, Vector3.zero);
        foreach (var player in players)
        {
            bounds.Encapsulate(player.transform.position);
        }

        // For a more sophisticated approach, consider the size of the level and players' positions
        // This calculation assumes players are the main factor in determining zoom
        return bounds.size.magnitude;
    }
}
