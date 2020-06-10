using UnityEngine;

public class CharacterGrounding : MonoBehaviour
{
    [SerializeField] private Transform[] _positions;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    private Transform _groundedObject;
    private Vector3? _groundedObjectLastPosition;

    public bool IsGrounded { get; private set; }
    public Vector2 GroundedDirection { get; private set; }

    void Update()
    {
        foreach (var position in _positions)
        {
            CheckPointForGrounding(position);
            if (IsGrounded)
                break;
        }

        StickToMovingObjects();
    }

    private void StickToMovingObjects()
    {
        if (_groundedObject != null)
        {
            if (_groundedObjectLastPosition.HasValue && 
                _groundedObjectLastPosition.Value != _groundedObject.position)
            {
                Vector3 delta = _groundedObject.position - _groundedObjectLastPosition.Value;
                transform.position += delta;
            }

            _groundedObjectLastPosition = _groundedObject.position;
        }
        else
        {
            _groundedObjectLastPosition = null;
        }
    }

    private void CheckPointForGrounding(Transform point)
    {
        var raycastHit = Physics2D.Raycast(point.position, point.forward, _maxDistance, _layerMask);
        Debug.DrawRay(point.position, point.forward * _maxDistance, Color.red);
        if (raycastHit.collider != null)
        {
            if (_groundedObject != raycastHit.collider.transform)
            {
                _groundedObject = raycastHit.collider.transform;
                IsGrounded = true;
                _groundedObjectLastPosition = _groundedObject.position;
                GroundedDirection = point.forward;
            }
        }
        else
        {
            _groundedObject = null;
            IsGrounded = false;
        }
    }
}
