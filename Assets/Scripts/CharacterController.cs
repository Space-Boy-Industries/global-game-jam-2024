using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public float RotateSpeed = 1000f;
    public LayerMask ClickNavTargets;

    private NavMeshAgent agent;
    private Camera camera;
    private Vector2 facingDirectionTarget;
    private Animator animator;
    private Interactable targetInteractable;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        facingDirectionTarget = new Vector2(transform.forward.x, transform.forward.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete
            && agent.remainingDistance <= agent.stoppingDistance
            && targetInteractable != null
            && Vector3.Distance(targetInteractable.transform.position, transform.position) <= 3f
        )
        {
            StartCoroutine(InteractWithTarget());
        }

        if (agent.velocity.magnitude > 0.01f)
        {
            // Set facing direction to current movement direction
            facingDirectionTarget = new Vector2(agent.velocity.x, agent.velocity.z).normalized;
        }

        // Set animator parameters
        animator.SetFloat("WalkSpeed", agent.velocity.magnitude / agent.speed);

        // Rotate towards facing direction
        var targetRotation = Quaternion.LookRotation(new Vector3(facingDirectionTarget.x, 0, facingDirectionTarget.y));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);

        var blockedByIntro = GlobalStateSystem.Instance.GlobalState.ContainsKey("in_intro") 
                      && GlobalStateSystem.Instance.GlobalState["in_intro"].ToString().ToLower() == "true";
        if (Input.GetButtonDown("Fire1") && DialogueSystem.Instance.isOpen == false && !blockedByIntro)
        {
            // Get mouse position in world space
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000, ClickNavTargets))
            {
                // Move to mouse position
                StartCoroutine(SetNavTarget(hit.point));

                if (hit.collider.gameObject.tag == "Interactable")
                {
                    Debug.Log("Interactable clicked on");
                    // Set target interactable
                    var interactable = hit.collider.gameObject.GetComponentInChildren<Interactable>();
                    targetInteractable = interactable;
                }
                else {
                    // Something else clicked on
                    targetInteractable = null;
                }
            }
        }
    }

    private bool FacingTargetWithin(float angle) {
        var targetRotation = Quaternion.LookRotation(new Vector3(facingDirectionTarget.x, 0, facingDirectionTarget.y));
        return Quaternion.Angle(transform.rotation, targetRotation) < angle;
    }

    private bool IsTurning() {
        return !FacingTargetWithin(0.1f);
    }

    IEnumerator SetNavTarget(Vector3 target)
    {
        agent.SetDestination(target);
        yield return new WaitUntil(() => !agent.pathPending);
        if (agent.path.corners.Length > 1) {
            // Turn towards the first node in the path before moving
            agent.isStopped = true;
            var towardsNextPoint = agent.path.corners[1] - transform.position;
            if (towardsNextPoint.magnitude > 0.01f)
            {
                facingDirectionTarget = new Vector2(towardsNextPoint.x, towardsNextPoint.z).normalized;
            }

            yield return new WaitUntil(() => FacingTargetWithin(90f));
            agent.isStopped = false;
        }
    }

    IEnumerator InteractWithTarget()
    {
        var towardsInteractable = targetInteractable.transform.position - transform.position;
        if (towardsInteractable.magnitude > 0.01f)
        {
            facingDirectionTarget = new Vector2(towardsInteractable.x, towardsInteractable.z).normalized;
        }

        yield return new WaitUntil(() => !IsTurning());
        if (targetInteractable) {
            targetInteractable.Interact();
            targetInteractable = null;
        }
    }
}
