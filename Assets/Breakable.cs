using UnityEngine;
public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject _replacement;
    [SerializeField] private float _breakForce = 2;
    [SerializeField] private float _collisionMultiplier = 100;
    [SerializeField] private bool _broken;

    public float offset;
    public float offsetRot;

    private void OnTriggerEnter(Collider collision)
    {
        print("collision");
            if (_broken) return;
            if (collision.CompareTag("Player"))
            {
            print("player");
                var replacement = Instantiate(_replacement, new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), transform.rotation);

                var rbs = replacement.GetComponentsInChildren<Rigidbody>();
                foreach (var rb in rbs)
                {
                    rb.AddExplosionForce(2 * _collisionMultiplier, transform.position, 2);
                }

                Destroy(gameObject);
            }
        
    }
}