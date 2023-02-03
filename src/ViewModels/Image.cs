using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class Image
    {
        public string Path { get; set; }

        public Image(string path)
        {
            Path = path;
        }
    }
}
