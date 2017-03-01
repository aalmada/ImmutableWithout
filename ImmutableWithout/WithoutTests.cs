using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace ImmutableWithout
{
    [TestClass]
    public class WithoutTests
    {
        class MyImmutableClass
        {
            public int MyProperty { get; }

            public MyImmutableClass(int myProperty)
            {
                MyProperty = myProperty;
            }
        }

        [TestMethod]
        public void FixtureCreate_Should_Succeed()
        {
            var fixture = new Fixture();
            var instance = fixture.Create<MyImmutableClass>();
            Assert.IsNotNull(instance);
            Assert.AreNotEqual(0, instance.MyProperty);
        }

        [TestMethod]
        public void FixtureCreate_Should_Succeed_When_WithouInCreate()
        {
            var fixture = new Fixture();
            var instance = fixture.Build<MyImmutableClass>()
                .Without(i => i.MyProperty)
                .Create();
            Assert.IsNotNull(instance);
            Assert.AreEqual(0, instance.MyProperty);
        }

        [TestMethod]
        public void FixtureCreate_Should_Succeed_When_WithouInCustomize()
        {
            var fixture = new Fixture();
            fixture.Customize<MyImmutableClass>(composer => composer
                .Without(i => i.MyProperty));
            var instance = fixture.Create<MyImmutableClass>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(0, instance.MyProperty);
        }
    }
}
