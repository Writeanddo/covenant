using UnityEngine;
using System.Collections;

namespace RE
{
    public class Demon : MonoBehaviour
    {
        private Animator _animator;
        private int _failTolerance;
        private float _eachLevelNumber;
        private float _levelNumber;
        private int _levelMain = 1;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetFailTolerance(int total)
        {
            _failTolerance = total;
            _eachLevelNumber = Helpers.DivideNumberByLevel(_failTolerance, 3);
        }

        public void SetStatus()
        {
            _levelNumber++;
            if(_levelNumber >= _eachLevelNumber)
            {
                _eachLevelNumber += _eachLevelNumber;
                _levelMain++;
                _animator.SetInteger("level", _levelMain);
            }
            if(_levelMain >= _failTolerance)
            {
                Debug.Log("It,s over.");
            }
        }
    }
}
