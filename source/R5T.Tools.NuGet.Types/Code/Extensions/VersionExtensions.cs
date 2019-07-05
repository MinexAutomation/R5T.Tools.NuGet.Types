using System;


namespace R5T.Tools.NuGet
{
    public static class VersionExtensions
    {
        public static string ToStringDisplay(this Version version)
        {
            var output = version.ToString();
            return output;
        }
    }
}
