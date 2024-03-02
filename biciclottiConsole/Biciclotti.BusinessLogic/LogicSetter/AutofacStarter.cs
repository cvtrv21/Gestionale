using Autofac;

namespace Biciclotti.BusinessLogic
{
    public static class AutofacStarter
    {
        static IContainer Container { get; set; } = null!;

        public static void AutofacInit()
        {
            try
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<LogicFactoriesCreator>();

                builder.RegisterType<LogicFactory>()
                    .AsSelf()
                    .As<ILogicFactory>()
                    .PropertiesAutowired()
                    .SingleInstance()
                    .OnActivating(ag => ag.Instance.Initialize());

                builder.RegisterAssemblyTypes(typeof(LogicFactory).Assembly)
                    .Where(t => t.Name.EndsWith("Logic"))
                    .AsImplementedInterfaces();

                Container = builder.Build();

                Container.Resolve<LogicFactoriesCreator>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
