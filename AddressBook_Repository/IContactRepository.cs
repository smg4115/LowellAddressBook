namespace AddressBook.Repository;

public interface IContactRepository
{
    void Add(Contact contact);
    Contact GetByID(int id);
    IEnumerable<Contact> GetAll();
    IEnumerable<Contact> GetByName(string name);
    void Update(Contact contact);
    bool Delete(int id);
}