using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public interface ICoroutineRunner
    {
        public void AbortCoroutine(IEnumerator routine);
        public Coroutine RunCoroutine(IEnumerator routine);
    }
}