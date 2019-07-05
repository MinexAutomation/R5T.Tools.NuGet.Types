using System;
using System.IO;
using System.Linq;

using R5T.NetStandard.IO;
using R5T.NetStandard.IO.Paths;
using R5T.Tools.NuGet.IO;
using R5T.Tools.NuGet.IO.Extensions;

using PathUtilities = R5T.NetStandard.IO.Paths.UtilitiesExtra;
using PathUtilitiesExtra = R5T.NetStandard.IO.Paths.UtilitiesExtra;
using NuGetIoUtilities = R5T.Tools.NuGet.IO.Utilities;


namespace R5T.Tools.NuGet
{
    /// <summary>
    /// A NuGet repository in a local-directory.
    /// NuGet packages can be published to a local-directory, which makes that directory a package repository.
    /// </summary>
    public class LocalDirectoryNugetRepository
    {
        #region Static

        public static void ListAllPackagesAndVersions(LocalDirectoryNugetRepository repository, TextWriter writer)
        {
            foreach (var packageDirectoryPath in repository.DirectoryPath.EnumerateDirectories())
            {
                LocalDirectoryNugetRepository.ListAllPackageVersions(packageDirectoryPath, writer);
            }
        }

        public static void ListAllPackagesAndVersions(DirectoryPath localPackageRepositoryDirectoryPath, TextWriter writer)
        {
            var localDirectoryNugetRepository = new LocalDirectoryNugetRepository(localPackageRepositoryDirectoryPath);

            LocalDirectoryNugetRepository.ListAllPackagesAndVersions(localDirectoryNugetRepository, writer);
        }

        public static void ListAllPackageVersions(DirectoryPath packageDirectoryPath, TextWriter writer)
        {
            foreach (var versionDirectoryPath in packageDirectoryPath.EnumerateDirectories())
            {
                LocalDirectoryNugetRepository.ListPackageVersionInformation(versionDirectoryPath, writer);
            }
        }

        public static void ListPackageVersionInformation(DirectoryPath versionDirectoryPath, TextWriter writer)
        {
            var nuspecSearchPattern = SearchPattern.AllFilesWithFileExtension(NuspecFileExtension.Instance);

            var nuspecFilePath = versionDirectoryPath.EnumerateFiles(nuspecSearchPattern).First().AsNuspecFilePath(); // TODO: check whether any entries, ensure only one entry.

            LocalDirectoryNugetRepository.ListPackageVersionInformationNuspec(nuspecFilePath, writer);
        }

        public static void ListPackageVersionInformationNuspec(NuspecFilePath nuspecFilePath, TextWriter writer)
        {
            var packageSpecification = NuGetIoUtilities.GetPackageSpecification(nuspecFilePath);

            var versionForDisplay = packageSpecification.Version.ToStringDisplay();

            writer.WriteLine($"{packageSpecification.ID} {versionForDisplay}");
        }

        public static void ListAllPackageVersions(LocalDirectoryNugetRepository repository, PackageID packageID, TextWriter writer)
        {
            var packageDirectoryName = NuGetIoUtilities.GetPackageDirectoryName(packageID);

            var packageDirectoryPath = PathUtilitiesExtra.Combine(repository.DirectoryPath, packageDirectoryName).Value.AsPackageDirectoryPath();

            LocalDirectoryNugetRepository.ListAllPackageVersions(packageDirectoryPath, writer);
        }

        public static void ListAllPackageVersions(DirectoryPath localPackageRepositoryDirectoryPath, PackageID packageID, TextWriter writer)
        {
            var localDirectoryNugetRepository = new LocalDirectoryNugetRepository(localPackageRepositoryDirectoryPath);

            LocalDirectoryNugetRepository.ListAllPackageVersions(localDirectoryNugetRepository, packageID, writer);
        }

        public static PackageDirectoryPath GetPackageDirectoryPath(LocalDirectoryNugetRepository repository, PackageID packageID)
        {
            var packageDirectoryName = NuGetIoUtilities.GetPackageDirectoryName(packageID);

            var packageDirectoryPath = PathUtilitiesExtra.Combine(repository.DirectoryPath, packageDirectoryName).Value.AsPackageDirectoryPath();
            return packageDirectoryPath;
        }

        public static PackageDirectoryPath GetPackageDirectoryPath(DirectoryPath localPackageRepositoryDirectoryPath, PackageID packageID)
        {
            var localDirectoryNugetRepository = new LocalDirectoryNugetRepository(localPackageRepositoryDirectoryPath);

            var output = LocalDirectoryNugetRepository.GetPackageDirectoryPath(localDirectoryNugetRepository, packageID);
            return output;
        }

        public static VersionDirectoryPath GetPackageVersionDirectoryPath(LocalDirectoryNugetRepository repository, PackageID packageID, Version packageVersion)
        {
            var packageDirectoryPath = LocalDirectoryNugetRepository.GetPackageDirectoryPath(repository, packageID);

            var versionDirectoryName = NuGetIoUtilities.GetVersionDirectoryName(packageVersion);

            var packageVersionDirectoryPath = PathUtilitiesExtra.Combine(packageDirectoryPath, versionDirectoryName).Value.AsVersionDirectoryPath();
            return packageVersionDirectoryPath;
        }

        public static VersionDirectoryPath GetPackageVersionDirectoryPath(DirectoryPath localPackageRepositoryDirectoryPath, PackageID packageID, Version packageVersion)
        {
            var localDirectoryNugetRepository = new LocalDirectoryNugetRepository(localPackageRepositoryDirectoryPath);

            var output = LocalDirectoryNugetRepository.GetPackageVersionDirectoryPath(localDirectoryNugetRepository, packageID, packageVersion);
            return output;
        }

        public static bool PackageExists(LocalDirectoryNugetRepository repository, PackageID packageID, Version packageVersion)
        {
            // Does the version directory of the package directory exist?
            var packageVersionDirectoryPath = LocalDirectoryNugetRepository.GetPackageVersionDirectoryPath(repository, packageID, packageVersion);
            if (!packageVersionDirectoryPath.Exists())
            {
                return false;
            }

            var nuspecFilePath = NuGetIoUtilities.GetNuspecFilePath(packageVersionDirectoryPath);

            // Does the nuspec file in the correct directory actually state the specified packageID and version number?
            var packageSpecification = NuGetIoUtilities.GetPackageSpecification(nuspecFilePath);

            if (packageSpecification.ID != packageID)
            {
                return false;
            }

            if (packageSpecification.Version != packageVersion)
            {
                return false;
            }

            return true;
        }

        public static bool PackageExists(DirectoryPath localPackageRepositoryDirectoryPath, PackageID packageID, Version packageVersion)
        {
            var localDirectoryNugetRepository = new LocalDirectoryNugetRepository(localPackageRepositoryDirectoryPath);

            var output = LocalDirectoryNugetRepository.PackageExists(localDirectoryNugetRepository, packageID, packageVersion);
            return output;
        }

        public static void DeletePackage(LocalDirectoryNugetRepository repository, PackageID packageID, Version packageVersion)
        {
            var packageVersionDirectoryPath = LocalDirectoryNugetRepository.GetPackageVersionDirectoryPath(repository, packageID, packageVersion);
            if (packageVersionDirectoryPath.Exists())
            {
                packageVersionDirectoryPath.Delete();
            }
        }

        public static void DeletePackage(DirectoryPath localPackageRepositoryDirectoryPath, PackageID packageID, Version packageVersion)
        {
            var localDirectoryNugetRepository = new LocalDirectoryNugetRepository(localPackageRepositoryDirectoryPath);

            LocalDirectoryNugetRepository.DeletePackage(localDirectoryNugetRepository, packageID, packageVersion);
        }

        #endregion


        public DirectoryPath DirectoryPath { get; }


        public LocalDirectoryNugetRepository(DirectoryPath directoryPath)
        {
            this.DirectoryPath = directoryPath;
        }
    }
}
