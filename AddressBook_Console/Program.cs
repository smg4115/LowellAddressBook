using AddressBook.Console;
using AddressBook.Repository;

class Program
{
    static void Main(string[] args)
    {
        var ui = new ProgramUI(new ContactRepository());
        ui.Run();
    }
}