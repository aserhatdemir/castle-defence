using UnityEngine;

public class GuidedMissile : Projectile

{
    public Rigidbody2D rigidBody;
    public float angleChangingSpeed;
    public float movementSpeed;
      
    void FixedUpdate()
    {
        if (target == null) return;
        Vector2 direction = (Vector2)target.transform.position - rigidBody.position;
        direction.Normalize ();
        float rotateAmount = Vector3.Cross (direction, transform.up).z;
        rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
        rigidBody.velocity = transform.up * movementSpeed;
    }
}