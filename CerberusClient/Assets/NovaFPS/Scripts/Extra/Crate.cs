using UnityEngine;

/// <summary>
/// Inheriting from destructible, lets you destroy crates
/// </summary>
public class Crate : Destructible {

    [Tooltip("Instantiate your destroyed object once it is destroyed."), SerializeField]
    private GameObject destroyedObject;

    /// <summary>
    /// Override the method from Destructible.cs
    /// Here we are also instantiating some effect on destructed.
    /// </summary>
    public override void Die() {
        Instantiate(destroyedObject, transform.position, Quaternion.identity);
        // base method
        base.Die();
    }
}