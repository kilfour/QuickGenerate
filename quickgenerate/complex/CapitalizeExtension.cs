namespace QuickGenerate.Complex
{
    public static class CapitalizeExtension
    {
        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}