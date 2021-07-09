using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class FoodImage : _ModelBase
    {
        public Uri IconPath { get; set; }

        public List<FoodImage> GetSeedsFromMockup() => new List<FoodImage>()
        {
            new FoodImage() {Id=1, IconPath = new Uri("/images/apple1.png", UriKind.Relative)},
            new FoodImage() {Id=2, IconPath = new Uri("/images/apple2.png", UriKind.Relative)},
            new FoodImage() {Id=3, IconPath = new Uri("/images/fhir.png", UriKind.Relative)}
        };
    }
}
