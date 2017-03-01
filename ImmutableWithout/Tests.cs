using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableWithout
{
    [TestFixture]
    public class Tests
    {
        class MyImmutableClass
        {
            public int MyProperty { get; }

            public MyImmutableClass(int myProperty)
            {
                MyProperty = myProperty;
            }
        }

        [Test]
        public void FixtureCreate_Should_Succeed()
        {
            var fixture = new Fixture();
            var instance = fixture.Create<MyImmutableClass>();
            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.MyProperty, Is.Not.EqualTo(0));
        }

        [Test]
        public void FixtureCreate_Should_Succeed_When_WithouInCreate()
        {
            var fixture = new Fixture();
            var instance = fixture.Build<MyImmutableClass>()
                .Without(i => i.MyProperty)
                .Create();
            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.MyProperty, Is.EqualTo(0));
        }

        [Test]
        public void FixtureCreate_Should_Succeed_When_WithouInCustomize()
        {
            var fixture = new Fixture();
            fixture.Customize<MyImmutableClass>(composer => composer
                .Without(i => i.MyProperty));
            var instance = fixture.Create<MyImmutableClass>();
            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.MyProperty, Is.EqualTo(0));
        }
    }
}
