using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public LayerMask ClickNavTargets;

    private NavMeshAgent agent;
    private Camera camera;
    private Vector2 facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        facingDirection = new Vector2(transform.forward.x, transform.forward.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            // Set facing direction to current movement direction
            facingDirection = new Vector2(agent.velocity.x, agent.velocity.z);
        }

        // Rotate towards facing direction
        transform.rotation = Quaternion.LookRotation(new Vector3(facingDirection.x, 0, facingDirection.y));

        if (Input.GetButtonDown("Fire1")) {
            // Get mouse position in world space
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, ClickNavTargets)) {
                // Move to mouse position
                agent.destination = hit.point;
            }
        }
    }
}
