using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayModeTests
    {
        private GameObject pNewObject;
        [SetUp]
        public void Setup()
        {
            Object pPrefab = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GameBoard"));
            pNewObject = (GameObject)Object.Instantiate(pPrefab, Vector3.zero, Quaternion.identity);
           // Debug.Assert(south._north == null && north._south == null, "Redefined neighbors!");
          // Debug.Assert(west.east == null && east.west == null, "Redefined neighbors!");
        }
        [TearDown]
        public void Teardown()
        {
            Object.Destroy(pNewObject.gameObject);
        }
        [UnityTest]
        public IEnumerator TestStartBoardSize()
        {
            pNewObject.GetComponent<GameBoard>().Initialize(new Vector2Int(11, 11));
            // Assert
            Assert.AreEqual(new Vector3(11,11,1), pNewObject.transform.GetChild(0).localScale);
            yield return null;
        }
    }
}
