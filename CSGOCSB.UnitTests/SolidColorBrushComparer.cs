using System.Collections.Generic;
using System.Windows.Media;

namespace CSGOCSB.UnitTests
{
    public class SolidColorBrushComparer : IEqualityComparer<SolidColorBrush>
    {
        public bool Equals(SolidColorBrush firstBrush, SolidColorBrush secondBrush)
            => firstBrush.Color == secondBrush.Color;

        public int GetHashCode(SolidColorBrush brush)
            => new { C = brush.Color, O = brush.Opacity }.GetHashCode();
    }
}