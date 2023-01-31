using UnityEngine;

public class MortarBullet : BulletController
{
    private Rigidbody rb;
    private bool fired = false;
    override public void Update()
    {
        if (fired == false)
        {
            Verticall(currentTargetPosition);
            fired = true;
        }
    }

    // todo
    public void Verticall(Vector3 target)
    {
        rb = GetComponent<Rigidbody>();
        var direction = target - transform.position;
        var rotvec = Quaternion.AngleAxis(22.5f, Vector3.right) * direction;
        rb.AddForce(rotvec * 1f, ForceMode.Impulse);
    }

    public float CalculateForce()
    {
        // to do
        return 10f;
    }

    public static float CalculateMaxRange(float muzzleVelocity)
    {
        return (muzzleVelocity * muzzleVelocity) / -Physics.gravity.y;
    }

    public static float GetTimeOfFlight(float vel, float angle, float height)
    {

        return (2.0f * vel * Mathf.Sin(angle)) / -Physics.gravity.y;
    }
}