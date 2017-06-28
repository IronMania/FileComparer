using System.Security.Cryptography;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FileComparer.Sha256
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<HashAlgorithm>().Instance(SHA256.Create()).Named("sha256"));
        }
    }
}