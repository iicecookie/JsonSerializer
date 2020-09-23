using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.Serializing;
using System.Reflection;
using Navicon.Serializer.DAL.Interfaces;
using log4net;
using Ninject;

namespace Navicon.Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Get<ILog>().Info("Начало работы приложения");

            Director director = new Director( kernel.Get<IDataSorce>(),
                                              kernel.Get<ModelBuilder>(),
                                              kernel.Get<ISerializer>("Excel"),
                                              kernel.Get<IFileManager>(),
                                              kernel.Get<ILog>());

            // var director = kernel.Get<Director>();

            director.CreateAndFillFile();

            kernel.Get<ILog>().Info("Конец работы приложения");
        }
    }
}
