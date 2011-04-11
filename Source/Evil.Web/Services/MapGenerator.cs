using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Evil.Common;
using Evil.Web.Models;

namespace Evil.Web.Services
{
    public class MapGenerator : IMapGenerator
    {
        private const int _defaultZoom = 13;
        private readonly Random _random;

        public MapGenerator()
        {
            _random = new Random();
        }

        #region IMapGenerator Members

        public GoogleMap CreateStartingMap(Area startingCity)
        {
            return new GoogleMap
                       {
                           StartingPosition =
                               CreateCenter(startingCity.LowerRight.Latitude, startingCity.UpperLeft.Latitude,
                                            startingCity.UpperLeft.Longitude, startingCity.LowerRight.Longitude),
                           Zoom = _defaultZoom,
                           Locations = CreateRandomLocations(5, startingCity.UpperLeft, startingCity.LowerRight)
                       };
        }

        public GoogleMap GenerateTargetMap(IEnumerable<Target> targets)
        {
            return new GoogleMap
                       {
                           StartingPosition = CreateStartingPosition(targets),
                           Locations = CreateLocationsFromTargets(targets),
                           Zoom = _defaultZoom
                       };
        }

        private static Position CreateStartingPosition(IEnumerable<Target> targets)
        {
            var latitudeMax = double.MinValue;
            var latitudeMin = double.MaxValue;
            var longitudeMax = double.MinValue;
            var longitudeMin = double.MaxValue;
            foreach (var target in targets)
            {
                latitudeMin = Math.Min(latitudeMin, target.Position.Latitude);
                latitudeMax = Math.Max(latitudeMax, target.Position.Latitude);
                longitudeMin = Math.Min(longitudeMin, target.Position.Longitude);
                longitudeMax = Math.Max(longitudeMax, target.Position.Longitude);
           }
            return CreateCenter(latitudeMin, latitudeMax, longitudeMin, longitudeMax);
        }

        private static Position CreateCenter(double latitudeMin, double latitudeMax, double longitudeMin, double longitudeMax)
        {
            return new Position((latitudeMin + latitudeMax)/2,
                                (longitudeMin + longitudeMax) / 2);
        }

        private static IEnumerable<GoogleLocation> CreateLocationsFromTargets(IEnumerable<Target> targets)
        {
            var locations = new Collection<GoogleLocation>();
            foreach (var target in targets)
            {
                locations.Add(new GoogleLocation {Name = target.Name, Position = target.Position});
            }
            return locations;
        }

        #endregion

        private IEnumerable<GoogleLocation> CreateRandomLocations(int count, Position upperLeft, Position lowerRight)
        {
            var locations = new Collection<GoogleLocation>();
            for (int i = 0; i < count; i++)
            {
                var location = new GoogleLocation {Name = ("Location " + i)};
                double latitude = GenerateRandomDouble(upperLeft.Latitude, lowerRight.Latitude);
                double longitude = GenerateRandomDouble(upperLeft.Longitude, lowerRight.Longitude);
                location.Position = new Position(latitude, longitude);
                locations.Add(location);
            }
            return locations;
        }

        private double GenerateRandomDouble(double minimum, double maximum)
        {
            double range = maximum - minimum;
            return minimum + (range*_random.NextDouble());
        }
    }
}