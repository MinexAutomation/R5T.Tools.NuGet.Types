using System;

using R5T.NetStandard;


namespace R5T.Tools.NuGet
{
    public static class PackageSpecificationExtensions
    {
        public static bool HasVersion(this PackageSpecification package)
        {
            var hasVersion = !VersionHelper.IsNone(package.Version);
            return hasVersion;
        }
    }
}
