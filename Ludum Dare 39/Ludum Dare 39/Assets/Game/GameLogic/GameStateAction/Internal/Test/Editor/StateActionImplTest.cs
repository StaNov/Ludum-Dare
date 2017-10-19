namespace GameOfLife.GameLogic.GameStateAction.Internal
{
    using UnityEngine.TestTools;
    using NUnit.Framework;
    using System.Collections;

    // TODO
    public class StateActionImplTest
    {
        
        [Test]
        public void StateActionImplTestSimplePasses()
        {
            // Use the Assert class to test conditions.
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator StateActionImplTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}
