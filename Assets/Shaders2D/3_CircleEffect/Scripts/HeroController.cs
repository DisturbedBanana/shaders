using UnityEngine;

namespace ShadersExercises.CircleEffect
{
    [ExecuteAlways]
    public class HeroController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _heroAnimator;
        [SerializeField] private Transform _circleAnchor;
        [SerializeField] private float _moveSpeed = 5f;

        private Vector2 _orientDir = Vector2.down;
        private Vector2 _moveDir = Vector2.zero;

        private void FixedUpdate()
        {
            if (!Application.isPlaying) return;
            _moveDir = _GetInputMoveDir();
            _rigidbody.velocity = _moveDir * _moveSpeed;
            if (_moveDir.x != 0f || _moveDir.y != 0f) {
                _orientDir = _moveDir;
            }
        }

        private void Update()
        {
            if (_heroAnimator != null) {
                _heroAnimator.SetFloat("OrientX", _orientDir.x);
                _heroAnimator.SetFloat("OrientY", _orientDir.y);
                _heroAnimator.SetBool("IsMoving", _moveDir.sqrMagnitude > 0f);
            }

            if (_circleAnchor != null) {
                Shader.SetGlobalVector("_HeroPosition", _circleAnchor.position);
            }
        }

        private Vector2 _GetInputMoveDir()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                dir.x -= 1f;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                dir.x += 1f;
            }

            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                dir.y += 1f;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                dir.y -= 1f;
            }

            return dir.normalized;
        }
    }
}