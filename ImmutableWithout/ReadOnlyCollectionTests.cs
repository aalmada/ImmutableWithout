using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using Ploeh.AutoFixture.Kernel;
using System.Linq;

namespace ImmutableWithout
{
    [TestClass]
    public class ReadOnlyCollectionTests
    {
        class Asset
        {
            public AssetMetadata Metadata { get; }

            public Asset(AssetMetadata metadata)
            {
                Metadata = metadata;
            }
        }

        class AssetMetadata
        {
            public IReadOnlyCollection<Asset> Assets { get; }

            public AssetMetadata(IReadOnlyCollection<Asset> assets)
            {
                Assets = assets;
            }
        }

        [TestMethod]
        public void FixtureCreateAssetMetadata_Should_Succeed()
        {
            var fixture = new Fixture();
            fixture.Inject(Enumerable.Empty<Asset>());
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IReadOnlyCollection<Asset>),
                    typeof(List<Asset>)));

            var metadata = fixture.Create<AssetMetadata>();
            Assert.IsNotNull(metadata);
            Assert.AreEqual(0, metadata.Assets.Count);
        }
    }
}
