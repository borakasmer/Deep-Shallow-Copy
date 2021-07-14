using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Deep_Shallow_Copy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            /*using (var console = new Product() { Name = "Ps5" })
            {
                Console.WriteLine($"Ürün Adı : ${console.Name}");
            }*/

            /*var console = new Product() { Name = "Ps5" };
            Console.WriteLine($"Ürün Adı : ${console.Name}");*/

            Customer ali = new Customer();

            ali.FirstName = "Ali";
            ali.LastName = "Ali Lastname";
            ali.Id = 1;
            ali.HomeAddress = new Address() { City = "İstanbul", Street = "Ali Street", Country = "Turkey" };
            //Customer veli = ali.ShallowCopy();
            Customer veli = ali.DeepCopy();

            veli.FirstName = "Veli Changed";
            veli.HomeAddress.City = "Angara";
            veli.HomeAddress.Street  = "Tunalı Hilmi ";

            Console.WriteLine($"City : {ali.HomeAddress.City}");
            Console.WriteLine($"Street : {ali.HomeAddress.Street}");
            Console.WriteLine($"Name : {ali.FirstName}");
            Console.WriteLine($"Surname : {ali.LastName}");
            Console.WriteLine($"ID: {ali.Id}");
        }
    }

    [Serializable]
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address HomeAddress { get; set; }
        public Customer ShallowCopy()
        {
            return (Customer)this.MemberwiseClone();
        }

        public Customer DeepCopy()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (Customer)formatter.Deserialize(ms);
            }
        }
    }
    [Serializable]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Product : IDisposable
    {
        private bool _disposed = false;

        ~Product() => Dispose(false);


        public int Id { get; set; }
        public string Name { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }
    }


}
