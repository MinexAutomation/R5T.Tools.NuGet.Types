using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.Tools.NuGet.IO
{
    public static class DirectoryPathExtensions
    {
        public static PackageDirectoryPath AsPackageDirectoryPath(this DirectoryPath directoryPath)
        {
            var packageDirectoryPath = new PackageDirectoryPath(directoryPath.Value);
            return packageDirectoryPath;
        }

        public static VersionDirectoryPath AsVersionDirectoryPath(this DirectoryPath directoryPath)
        {
            var versionDirectoryPath = new VersionDirectoryPath(directoryPath.Value);
            return versionDirectoryPath;
        }
    }   
}
