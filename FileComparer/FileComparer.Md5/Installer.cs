using System.Security.Cryptography;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FileComparer.Md5
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<HashAlgorithm>().Instance(MD5.Create()).Named("md5").IsDefault());
        }
    }
}