using HoneyBunny.Models;
using Ninject.Modules;

namespace HoneyBunny.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<Repository>().WithConstructorArgument("context", new ApplicationDbContext());
        }
    }
}