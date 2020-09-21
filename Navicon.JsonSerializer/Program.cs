using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.Serializing;
using Navicon.Serializer.DAL.DataSorce;

namespace Navicon.Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Logger.Log.Info("Начало работы приложения");

            Director director = new Director(new ContactFabric(), new ModelBuilder(), new CSVSerializer(), new FileManager());

            director.CreateAndFillFile();

            Logger.Logger.Log.Info("Конец работы приложения");
        }

        //  Contact contact1 = new Contact(
        //      firstName: "Maxim",
        //      secondName: "Bukanov",
        //      lastName: "...",
        //      gender: Gender.Male,
        //      address: new Address()
        //      {
        //          Sity = "Moscow",
        //          AddressType = AddressType.Actual,
        //          Country = "Russia",
        //          LifeLocation = "213"
        //      },
        //      birth: new DateTime(1998, 11, 22),
        //      itn: "123124141241",
        //      phoneNumber: "+7(925)561-84-06"
        //  );
        //
        //  Contact contact2 = (Contact)contact1.Clone();
        //
        //  var contacts = new List<Contact>() { contact1, contact2 };
        //
        //  ModelOperations.ModelValidetor.ValidateModel(contact1);

        //  var contactSerializer = new ContactSerializer(new FileManager(), new Serializer.JsonSerializer());
        //
        //  contactSerializer.Serialize(contact1);
        //
        //  Console.WriteLine(contactSerializer.Deserialize("Maxim_Bukanov"));'
    }
}
