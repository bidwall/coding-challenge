using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;

        private SearchEngine _searchEngine;

        [OneTimeSetUp]
        public void Setup()
        {
            _shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            _searchEngine = new SearchEngine(_shirts);
        }

        [Test]
        public void Search_NoColorNoSize_ReturnsAllShirts()
        {
            var searchOptions = new SearchOptions();

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_OneColorNoSize_ReturnsShirtsForColorInAllSizes()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = _searchEngine.Search(searchOptions);
            
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_NoColorOneSize_ReturnsShirtsInAllColorsForSize()
        {
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small }
            };

            var results = _searchEngine.Search(searchOptions);
            
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_OneColorOneSize_ReturnsShirtInSelectedSizeAndColor()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_OneColorManySizes_ReturnsShirtsForSelectedColorAndSizes()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_ManyColorsOneSize_ReturnsShirtsForSelectedColorsAndSize()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black, Color.Blue },
                Sizes = new List<Size> { Size.Small }
            };
            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_ManyColorsManySizes_ReturnsShirtsForSelectedColorsAndSizes()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black, Color.Blue },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }
    }
}
