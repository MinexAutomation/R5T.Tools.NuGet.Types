using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.Tools.NuGet.IO
{
    public static class DirectoryNameExtensions
    {
        public static PackageDirectoryName AsPackageDirectoryName(this DirectoryName directoryName)
        {
            var packageDirectoryName = new PackageDirectoryName(directoryName.Value);
            return packageDirectoryName;
        }

        public static VersionDirectoryName AsVersionDirectoryName(this DirectoryName directoryName)
        {
            var versionDirectoryName = new VersionDirectoryName(directoryName.Value);
            return versionDirectoryName;
        }
    }
}
