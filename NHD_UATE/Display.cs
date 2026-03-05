using System;
using System.Collections.Generic;
using System.Text;

namespace NHD_UATE
{

    public class Display
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Rev { get; set; }
        public string Interface { get; set; }
        public string Logic { get; set; }

        public Display(string _Name, string _Desc, string _Rev, string _Int, string _logic, string _path)
        {
            Name = _Name;
            Path = _path;
            Description = _Desc;
            Rev = _Rev;
            Interface = _Int;
            Logic = _logic;
        }

    }
}
