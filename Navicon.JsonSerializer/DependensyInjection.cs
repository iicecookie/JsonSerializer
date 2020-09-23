using log4net;
using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.DataSorce;
using Navicon.Serializer.DAL.Interfaces;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.Logging;
using Navicon.Serializer.Serializing;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Navicon.Serializer
{
    public class DependensyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataSorce>().To<ContactFabric>();

            Bind<ModelBuilder>().ToSelf();

            Bind<Director>().ToSelf();

            Bind<ISerializer>().ToFactory();

            Bind<ISerializer>().To<ExcelSerializer>().Named("Excel");
            Bind<ISerializer>().To<JsonSerializer>().Named("Json");
            Bind<ISerializer>().To<CSVSerializer>().Named("Csv");

            Bind<IFileManager>().To<FileManager>();

            Bind<ILog>().ToMethod(context => Logger.GetLogger());
        }
    }
}
