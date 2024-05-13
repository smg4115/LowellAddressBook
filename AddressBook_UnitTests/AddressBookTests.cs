using AddressBook.Repository;

namespace AddressBook.UnitTests;

[TestClass]
public class ProgramUITests
{
    private ContactRepository _repository;
    private ProgramUI _programUI;

    [TestInitialize]
    public void Setup()
    {
        // Initialize with a real repository
        _repository = new ContactRepository();
        _programUI = new ProgramUI(_repository);
    }

    [TestMethod]
    public void AddContact_ShouldAddContactToRepository()
    {
        // Arrange
        string name = "Blake Bickle";
        string address = "123 Elm St";
        string email = "Blake.bickle@example.com";
        string phone = "555-1234";

        // Act
        _programUI.AddContact(name, address, email, phone);

        // Assert
        var contacts = _repository.GetAll();
        Assert.AreEqual(1, contacts.Count(), "There should be one contact in the repository.");
        var contact = contacts.First();
        Assert.AreEqual(name, contact.Name);
        Assert.AreEqual(address, contact.Address);
        Assert.AreEqual(email, contact.Email);
        Assert.AreEqual(phone, contact.PhoneNumber);
    }

    [TestMethod]
    public void DeleteContact_ShouldRemoveContactFromRepository()
    {
        // Arrange
        string name = "Blake Bickle";
        string address = "123 Elm St";
        string email = "Blake.bickle@example.com";
        string phone = "555-1234";
        _programUI.AddContact(name, address, email, phone);
        var contact = _repository.GetAll().First();

        // Act
        _programUI.DeleteContact(contact.ID);

        // Assert
        Assert.AreEqual(0, _repository.GetAll().Count(), "There should be no contacts in the repository after deletion.");
    }
}