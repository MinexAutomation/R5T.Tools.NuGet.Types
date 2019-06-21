using System;
using System.IO;
using System.Linq;
using System.Xml;

using R5T.NetStandard.IO.Paths;
using R5T.NetStandard.IO.Paths.Extensions;
using R5T.Tools.NuGet.IO;
using R5T.Tools.NuGet.IO.Extensions;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.Tools.NuGet.IO
{
    public static class Utilities
    {
        public static NupkgFileName GetNupkgFileName(PackageID packageID, Version version)
        {
            var packageFileSystemName = Utilities.GetPackageNupkgFileSystemName(packageID);
            var versionFileSystemName = Utilities.GetVersionFileSystemName(version);
            var nupkgFileNameWithoutExtension = Utilities.GetNupkgFileNameWithoutExtension(packageFileSystemName, versionFileSystemName);

            var nupkgFileName = PathUtilities.GetFileName(nupkgFileNameWithoutExtension, FileExtensions.Nupkg).AsNupkgFileName();
            return nupkgFileName;
        }

        public static NuspecFileName GetNuspecFileName(PackageID packageID)
        {
            var packageFileSystemName = Utilities.GetPackageNuspecFileSystemName(packageID);
            var nuspecFileNameWithoutExtension = Utilities.GetNuspecFileNameWithoutExtension(packageFileSystemName);
            var nuspecFileName = PathUtilities.GetFileName(nuspecFileNameWithoutExtension, NuspecFileExtension.Instance).AsNuspecFileName();
            return nuspecFileName;
        }

        public static PackageDirectoryName GetPackageDirectoryName(PackageID packageID)
        {
            var packageFileSystemName = Utilities.GetPackageNuspecFileSystemName(packageID);
            var packageDirectoryName = packageFileSystemName.Value.AsPackageDirectoryName();
            return packageDirectoryName;
        }

        public static FileNameWithoutExtension GetNuspecFileNameWithoutExtension(PackageFileSystemName packageFileSystemName)
        {
            var fileNameWithoutExtension = packageFileSystemName.Value.AsFileNameWithoutExtension();
            return fileNameWithoutExtension;
        }

        public static FileNameWithoutExtension GetNupkgFileNameWithoutExtension(PackageFileSystemName packageFileSystemName, VersionFileSystemName versionFileSystemName)
        {
            var nupkgFileNameWithoutExtension = PathUtilities.GetFileNameWithoutExtension(packageFileSystemName, versionFileSystemName);
            return nupkgFileNameWithoutExtension;
        }

        /// <summary>
        /// Value is the lower-case version of the package ID.
        /// </summary>
        public static PackageFileSystemName GetPackageNuspecFileSystemName(PackageID packageID)
        {
            var packageFileSystemName = packageID.Value.ToLowerInvariant().AsPackageFileSystemName();
            return packageFileSystemName;
        }

        /// <summary>
        /// Value is the same as the package ID.
        /// </summary>
        public static PackageFileSystemName GetPackageNupkgFileSystemName(PackageID packageID)
        {
            var packageFileSystemName = packageID.Value.AsPackageFileSystemName();
            return packageFileSystemName;
        }

        public static VersionDirectoryName GetVersionDirectoryName(Version version)
        {
            var versionFileSystemName = Utilities.GetVersionFileSystemName(version);
            var versionDirectoryName = versionFileSystemName.Value.AsVersionDirectoryName();
            return versionDirectoryName;
        }

        public static VersionFileSystemName GetVersionFileSystemName(Version version)
        {
            var versionFileSystemName = version.ToString().AsVersionFileSystemName();
            return versionFileSystemName;
        }

        /// <summary>
        /// Gets the path of the <see cref="FileExtensions.Nuspec"/> file in the directory.
        /// </summary>
        public static NuspecFilePath GetNuspecFilePath(DirectoryPath versionDirectoryPath)
        {
            var searchPattern = SearchPattern.AllFilesWithFileExtension(FileExtensions.Nuspec);
            var nuspecFilePaths = versionDirectoryPath.EnumerateFiles(searchPattern).ToArray();

            if (nuspecFilePaths.Length < 1)
            {
                throw new IOException($"No {FileExtensions.Nuspec} file found in version directory: {versionDirectoryPath}");
            }

            if (nuspecFilePaths.Length > 1)
            {
                throw new IOException($"More than one {FileExtensions.Nuspec} file found in version directory: {versionDirectoryPath}");
            }

            var nuspecFilePath = nuspecFilePaths.First().AsNuspecFilePath();
            return nuspecFilePath;
        }

        public static PackageSpecification GetPackageSpecification(NuspecFilePath nuspecFilePath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(nuspecFilePath.Value);

            var packageElement = xmlDoc.SelectSingleNode("//*[local-name()='package']");
            var namespaceAttribute = packageElement.Attributes["xmlns"];
            var defaultNamespaceValue = namespaceAttribute.Value; // Example: "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd", but annoyingly can be different between Visual Studio's NuGet and the latest NuGet executable.

            // Create a namespace manager.
            var xmlnsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlnsManager.AddNamespace("default", defaultNamespaceValue);

            var metadataElement = xmlDoc.SelectSingleNode("//default:package/default:metadata", xmlnsManager);

            var IDElementXPath = "//default:package/default:metadata/default:id";
            var IDElement = xmlDoc.SelectSingleNode(IDElementXPath, xmlnsManager);
            var ID = IDElement.InnerText;

            var versionElementXPath = "//default:package/default:metadata/default:version";
            var versionElement = xmlDoc.SelectSingleNode(versionElementXPath, xmlnsManager);
            var versionString = versionElement.InnerText;
            var version = Version.Parse(versionString);

            var specification = new PackageSpecification
            {
                ID = ID.AsPackageID(),
                Version = version,
            };
            return specification;
        }
    }
}
