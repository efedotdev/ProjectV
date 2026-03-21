using Business.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsolTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductManager p = new ProductManager(new EfProductDal());
            Product phone = new Product() { ProductId=9,ProductName = "Iphone 14", CategoryId = 1, IsActive = true, UnitPrice = 55000, UnitsInStock =3 , ProductCode = "ELK-004" };
            Console.WriteLine("Sistem başlatıldı.");
            Console.WriteLine(p.Update(phone).Message);
            
        }
    }
}
