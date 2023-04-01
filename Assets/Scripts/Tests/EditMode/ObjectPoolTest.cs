using Assets.Scripts.Utils;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Tests.EditMode
{
    public class ObjectPoolTest
    {
        [SetUp]
        public void Setup()
        {
            ObjectPool.Clear();
        }

        // A Test behaves as an ordinary method
        [Test]
        public void Get_WhenPoolEmpty_ShouldReturnNull()
        {
            // Arrange
            int key = 1;

            // Act
            var result = ObjectPool.Get(key);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Get_WhenPoolNotEmpty_ShouldReturnObject()
        {
            // Arrange
            int key = 1;
            var obj = new GameObject();
            ObjectPool.Add(key, obj);

            // Act
            var result = ObjectPool.Get(key);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(obj, result);
        }

        [Test]
        public void Get_WhenPoolNotEmpty_ShouldRemoveObjectFromPool()
        {
            // Arrange
            int key = 1;
            var obj = new GameObject();
            ObjectPool.Add(key, obj);

            // Act
            var result = ObjectPool.Get(key);

            // Assert
            Assert.IsNull(ObjectPool.Get(key));
        }

        [Test]
        public void Add_WhenPoolEmpty_ShouldAddObject()
        {
            // Arrange
            int key = 1;
            var obj = new GameObject();

            // Act
            ObjectPool.Add(key, obj);

            // Assert
            Assert.AreEqual(obj, ObjectPool.Get(key));
        }
    }
}
