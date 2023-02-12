namespace TilemapGenerator.Common
{
    public class NaturalStringComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            var length = x.Length;
            var length2 = y.Length;
            var i = 0;
            var j = 0;
            while (i < length && j < length2)
            {
                var c = x[i];
                var c2 = y[j];
                if (c is >= '0' and <= '9' && c2 is >= '0' and <= '9')
                {
                    var num = 0;
                    var num2 = 0;
                    for (; i < length; i++)
                    {
                        if (c is < '0' or > '9')
                        {
                            break;
                        }

                        num = num * 10 + c - 48;
                    }

                    for (; j < length2; j++)
                    {
                        if (c2 is < '0' or > '9')
                        {
                            break;
                        }

                        num2 = num2 * 10 + c2 - 48;
                    }

                    if (num != num2)
                    {
                        return num > num2 ? 1 : -1;
                    }
                }

                if (c != c2)
                {
                    return c > c2 ? 1 : -1;
                }

                i++;
                j++;
            }

            return length - i - (length2 - j);
        }
    }
}