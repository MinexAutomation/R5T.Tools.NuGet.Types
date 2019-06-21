using System;


namespace R5T.Tools.NuGet.Extensions
{
    public static class StringExtensions
    {
        public static PackageID AsPackageID(this string value)
        {
            var packageID = new PackageID(value);
            return packageID;
        }
    }
}
