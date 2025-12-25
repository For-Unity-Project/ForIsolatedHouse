using UnityEngine;
using Unity.Netcode;

public class Grab : NetworkBehaviour
{
    private bool hold;
    public bool canGrab;
    public Animator animator;
    public bool RightHand;

    FixedJoint joint;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (!canGrab) return;

        bool grabInput = RightHand
            ? Input.GetMouseButton(1)
            : Input.GetMouseButton(0);

        if (grabInput)
        {
            animator.SetBool(RightHand ? "GrabR" : "GrabL", true);
            hold = true;
        }
        else
        {
            Release();
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (hold == false) return;
        if (joint != null) return;

        Rigidbody rb = col.rigidbody;
        if (rb == null) return;

        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = rb;

        joint.breakForce = 2000f;
        joint.breakTorque = 400f;
    }

    void OnJointBreak(float breakForce)
    {
        Release();
    }

    void Release()
    {
        animator.SetBool(RightHand ? "GrabR" : "GrabL", false);
        hold = false;

        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
    }
}
