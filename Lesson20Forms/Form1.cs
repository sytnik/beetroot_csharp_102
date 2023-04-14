using System.Reflection;
using Lesson20Lib;

namespace Lesson20Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // use from reference
            // var persons =
            //     new PersonCollectionMock(30).Persons;
            // reflection
            // 1. load the assembly
            var path = "Lesson20Lib.dll";
            Assembly assembly = Assembly.LoadFrom(path);
            // 2. get target type
            Type personsType =
                assembly.GetType("Lesson20Lib.PersonCollectionMock");
            // 3. create an instance of the class
            var persons = Activator
                .CreateInstance(personsType, 100);
            var persons2 = Convert.ChangeType(persons, personsType);
            var directCast = (PersonCollectionMock)persons;

        }
    }
}