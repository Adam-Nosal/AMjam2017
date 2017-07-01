using UnityEngine;

public class JumpCamera2D : MonoBehaviour {

    //public float dampTime = 0.15f;
   // private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Camera me;
    public Vector3 Offset;

    private void Start() {
        me = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate () 
    {
        if (target)
        {
            Vector3 point = me.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - me.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position+ delta;
            destination += Offset;
            transform.position = destination;//Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }

    public void SetNewTarget( GameObject oTarget)
    {
        this.target = oTarget.transform;
    }
}