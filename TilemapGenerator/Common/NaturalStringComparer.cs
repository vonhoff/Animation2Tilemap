using System.Globalization;

namespace TilemapGenerator.Common;

public class NaturalStringComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        switch (x)
        {
            case null when y == null:
            {
                return 0;
            }
            case null:
            {
                return -1;
            }
        }

        if (y == null)
        {
            return 1;
        }

        var lx = x.Length;
        var ly = y.Length;
        var mx = 0;
        var my = 0;

        while (mx < lx && my < ly)
        {
            if (char.IsDigit(x[mx]) && char.IsDigit(y[my]))
            {
                var vx = 0.0;
                var vy = 0.0;

                while (mx < lx && char.IsDigit(x[mx]))
                {
                    vx = vx * 10 + CharUnicodeInfo.GetNumericValue(x, mx);
                    mx++;
                }

                while (my < ly && char.IsDigit(y[my]))
                {
                    vy = vy * 10 + CharUnicodeInfo.GetNumericValue(y, my);
                    my++;
                }

                if (Math.Abs(vx - vy) > 0)
                {
                    return vx > vy ? 1 : -1;
                }
            }
            else
            {
                var cmp = string.Compare(x, mx, y, my, 1, StringComparison.InvariantCulture);
                if (cmp != 0)
                {
                    return cmp;
                }

                mx++;
                my++;
            }
        }

        return lx - ly;
    }
}