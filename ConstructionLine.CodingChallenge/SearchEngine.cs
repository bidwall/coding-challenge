using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly Dictionary<Guid, int> _colorCount;
        private readonly Dictionary<Guid, int> _sizeCount;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            _colorCount = Color.All.ToDictionary(x => x.Id, y => 0);
            _sizeCount = Size.All.ToDictionary(x => x.Id, y => 0);
        }


        public SearchResults Search(SearchOptions options)
        {
            ResetCounts();

            var selectedColours = options.Colors.ToDictionary(x => x.Id);
            var selectedSizes = options.Sizes.ToDictionary(x => x.Id);
            var isColorSelected = options.Colors.Any();
            var isSizeSelected = options.Sizes.Any();

            var matchingShirts = new List<Shirt>();

            foreach (var shirt in _shirts)
            {
                if ((!isColorSelected || selectedColours.ContainsKey(shirt.Color.Id))
                    && (!isSizeSelected || selectedSizes.ContainsKey(shirt.Size.Id)))
                {
                    matchingShirts.Add(shirt);
                    _colorCount[shirt.Color.Id]++;
                    _sizeCount[shirt.Size.Id]++;
                }
            }


            return CreateSearchResults(matchingShirts);
        }

        private SearchResults CreateSearchResults(List<Shirt> shirts)
        {
            var searchResults = new SearchResults
            {
                Shirts = shirts,
                ColorCounts = new List<ColorCount>(),
                SizeCounts = new List<SizeCount>()
            };

            Color.All.ForEach(x => searchResults.ColorCounts.Add(new ColorCount { Color = x, Count = _colorCount.TryGetValue(x.Id, out var value) ? value : 0 }));
            Size.All.ForEach(x => searchResults.SizeCounts.Add(new SizeCount { Size = x, Count = _sizeCount.TryGetValue(x.Id, out var value) ? value : 0 }));

            return searchResults;
        }

        private void ResetCounts()
        {
            foreach (var colorCountKey in _colorCount.Keys.ToList())
            {
                _colorCount[colorCountKey] = 0;
            }

            foreach (var sizeCountKey in _sizeCount.Keys.ToList())
            {
                _sizeCount[sizeCountKey] = 0;
            }
        }
    }
}