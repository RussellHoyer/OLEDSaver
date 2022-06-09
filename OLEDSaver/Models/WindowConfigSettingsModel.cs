using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLEDSaver.Models
{
    public class WindowConfigSettingsModel
    {
        public double OriginalWidth { get; set; } = 800;
        public double OriginalHeight { get; set; } = 450;

        public double CurrentWidth { get; set; }
        public double CurrentHeight { get; set; }


    }
}
