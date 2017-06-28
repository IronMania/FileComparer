using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FileComparer
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IHashFactory>().AsFactory());
            container.Register(Classes.FromThisAssembly()
                .Pick()
                .WithServiceAllInterfaces()
                .WithServiceSelf()
            );
        }
    }
}