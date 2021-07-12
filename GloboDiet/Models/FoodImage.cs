using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class FoodImage : _ModelBase
    {
        public Uri IconPath { get; set; }

        public static List<FoodImage> GetSeedsFromLegacy() => new List<FoodImage>()
        {
            new FoodImage() {Id=1, Code="apple_deliciuos", IconPath = new Uri("/images/apple1.png", UriKind.Relative)},
            new FoodImage() {Id=2, Code="apple_red", IconPath = new Uri("/images/apple2.png", UriKind.Relative)},
            new FoodImage() {Id=3, Code="hotmeal", IconPath = new Uri("/images/fhir.jpg", UriKind.Relative)}
        };
    }
}
