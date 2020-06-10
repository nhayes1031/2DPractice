using UnityEngine;
using UnityEngine.Serialization;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] 
    [FormerlySerializedAs("sawBladeSprite")]
    private Transform sprite;
    [SerializeField] private float speed;

    private float _positionPercent;
    private int _direction = 1;

    private void Update()
    {
        float distance = Vector3.Distance(start.position, end.position);
        float speedForDistance = speed / distance;
        _positionPercent += Time.deltaTime * _direction * speedForDistance;
  
        sprite.position = Vector3.Lerp(start.position, end.position, _positionPercent);

        if (_positionPercent >= 1 && _direction == 1)
            _direction = -1;
        else if (_positionPercent <= 0 && _direction == -1)
            _direction = 1;
    }
}
