using AddressBook.Repository;

namespace AddressBook.Console
{
    public class ProgramUI
    {
        private IContactRepository _repo;

        public ProgramUI(IContactRepository repo)
        {
            _repo = repo;
            SeedData();
        }

        public void Run()
        {
            // Initial listing of all contacts at start
            ListContacts();
            bool running = true;
            while (running)
            {
                running = ShowMainMenu();
            }
        }

        private bool ShowMainMenu()
        {
            System.Console.WriteLine("\nMain Menu:");
            System.Console.WriteLine("1. Add Contact");
            System.Console.WriteLine("2. Edit Contact");
            System.Console.WriteLine("3. Delete Contact");
            System.Console.WriteLine("4. Exit");
            string option = System.Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ShowEditMenu();
                    break;
                case "3":
                    DeleteContact();
                    break;
                case "4":
                    return false;
                default:
                    System.Console.WriteLine("Invalid option.");
                    break;
            }
            ListContacts();  // List all contacts after each action or invalid option
            return true;
        }

        private void ShowEditMenu()
        {
            System.Console.WriteLine("\nEnter the ID of the contact you want to edit:");
            int id;
            if (!int.TryParse(System.Console.ReadLine(), out id))
            {
                System.Console.WriteLine("Invalid ID format.");
                return;
            }

            EditContact(id);
        }

        private void AddContact()
        {
            System.Console.WriteLine("Enter Name:");
            string name = System.Console.ReadLine();
            System.Console.WriteLine("Enter Address:");
            string address = System.Console.ReadLine();
            System.Console.WriteLine("Enter Email:");
            string email = System.Console.ReadLine();
            System.Console.WriteLine("Enter Phone Number:");
            string phoneNumber = System.Console.ReadLine();

            var contact = new Contact { Name = name, Address = address, Email = email, PhoneNumber = phoneNumber };
            _repo.Add(contact);
            System.Console.WriteLine("Contact added.");
        }

        private void ListContacts()
        {
            var contacts = _repo.GetAll();
            if (contacts.Any())
            {
                System.Console.WriteLine("\n------------------------------- Lowell Logistics Driver List -------------------------------\n");
                foreach (var contact in contacts)
                {
                    System.Console.WriteLine($"ID: {contact.ID}, Name: {contact.Name}, Address: {contact.Address}, Email: {contact.Email}, Phone: {contact.PhoneNumber}");
                }
            }
            else
            {
                System.Console.WriteLine("No contacts found.");
            }
        }

        private void EditContact(int id)
        {
            var contact = _repo.GetByID(id);

            if (contact != null)
            {
                System.Console.WriteLine("Editing Contact - Enter new values (leave blank to keep current value):");

                System.Console.WriteLine("Enter new Name (leave empty to not change):");
                string name = System.Console.ReadLine();
                System.Console.WriteLine("Enter new Address (leave empty to not change):");
                string address = System.Console.ReadLine();
                System.Console.WriteLine("Enter new Email (leave empty to not change):");
                string email = System.Console.ReadLine();
                System.Console.WriteLine("Enter new Phone Number (leave empty to not change):");
                string phoneNumber = System.Console.ReadLine();

                contact.Name = string.IsNullOrEmpty(name) ? contact.Name : name;
                contact.Address = string.IsNullOrEmpty(address) ? contact.Address : address;
                contact.Email = string.IsNullOrEmpty(email) ? contact.Email : email;
                contact.PhoneNumber = string.IsNullOrEmpty(phoneNumber) ? contact.PhoneNumber : phoneNumber;

                _repo.Update(contact);
                System.Console.WriteLine("Contact updated.");
            }
            else
            {
                System.Console.WriteLine("Contact not found.");
            }
        }

        private void DeleteContact()
        {
            System.Console.WriteLine("Enter ID of contact to delete:");
            int id;
            if (!int.TryParse(System.Console.ReadLine(), out id))
            {
                System.Console.WriteLine("Invalid ID format.");
                return;
            }

            if (_repo.Delete(id))
            {
                System.Console.WriteLine("Contact deleted.");
            }
            else
            {
                System.Console.WriteLine("Contact not found.");
            }
        }

        private void SeedData()
        {
            _repo.Add(new Contact { Name = "Blake Bickle", Address = "123 Elm St", Email = "Blake.bickle@example.com", PhoneNumber = "555-1234" });
            _repo.Add(new Contact { Name = "Jean Triplehorn", Address = "456 Oak St", Email = "jtrip@example.com", PhoneNumber = "555-5678" });
            _repo.Add(new Contact { Name = "Timmy Tom", Address = "500 Haughville Rd", Email = "ragecje@example.com", PhoneNumber = "555-1234" });
            _repo.Add(new Contact { Name = "Jake Cucumberbun", Address = "4678 Steem St", Email = "homeless@example.com", PhoneNumber = "555-5678" });
        }
    }
}