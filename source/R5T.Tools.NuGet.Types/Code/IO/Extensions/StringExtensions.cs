using System;


namespace R5T.Tools.NuGet.IO.Extensions
{
    public static class StringExtensions
    {
        public static NupkgFileName AsNupkgFileName(this string fileName)
        {
            var nupkgFileName = new NupkgFileName(fileName);
            return nupkgFileName;
        }

        public static NupkgFilePath AsNupkgFilePath(this string filePath)
        {
            var nupkgFilePath = new NupkgFilePath(filePath);
            return nupkgFilePath;
        }

        public static NuspecFileName AsNuspecFileName(this string fileName)
        {
            var nuspecFileName = new NuspecFileName(fileName);
            return nuspecFileName;
        }

        public static NuspecFilePath AsNuspecFilePath(this string filePath)
        {
            var nuspecFilePath = new NuspecFilePath(filePath);
            return nuspecFilePath;
        }

        public static PackageDirectoryName AsPackageDirectoryName(this string value)
        {
            var packageDirectoryName = new PackageDirectoryName(value);
            return packageDirectoryName;
        }

        public static PackageDirectoryPath AsPackageDirectoryPath(this string value)
        {
            var packageDirectoryPath = new PackageDirectoryPath(value);
            return packageDirectoryPath;
        }

        public static PackageFileSystemName AsPackageFileSystemName(this string value)
        {
            var packageFileSystemName = new PackageFileSystemName(value);
            return packageFileSystemName;
        }

        public static PackageID AsPackageID(this string value)
        {
            var packageID = new PackageID(value);
            return packageID;
        }

        public static VersionDirectoryName AsVersionDirectoryName(this string value)
        {
            var versionDirectoryName = new VersionDirectoryName(value);
            return versionDirectoryName;
        }

        public static VersionDirectoryPath AsVersionDirectoryPath(this string value)
        {
            var versionDirectoryPath = new VersionDirectoryPath(value);
            return versionDirectoryPath;
        }

        public static VersionFileSystemName AsVersionFileSystemName(this string value)
        {
            var versionFileSystemName = new VersionFileSystemName(value);
            return versionFileSystemName;
        }
    }
}
