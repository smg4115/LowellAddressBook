[TestClass]
public class ContactRepositoryTests
{
    private ContactRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        // Instantiate the repository before each test
        _repository = new ContactRepository();
    }

    [TestMethod]
    public void Add_ShouldAddContactAndAssignUniqueId()
    {
        // Arrange
        var contact = new Contact { Name = "John Doe", Address = "123 Elm St", Email = "john@example.com", PhoneNumber = "555-1234" };

        // Act
        _repository.Add(contact);

        // Assert
        Assert.AreEqual(1, contact.ID, "The ID should be set to 1 for the first contact.");
        Assert.AreEqual(1, _repository.GetAll().Count(), "There should be one contact in the repository.");
        Assert.AreEqual(contact, _repository.GetByID(1), "The contact retrieved by ID should be the same as the one added.");
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllAddedContacts()
    {
        // Arrange
        var contact1 = new Contact { Name = "John Doe", Address = "123 Elm St", Email = "john@example.com", PhoneNumber = "555-1234" };
        var contact2 = new Contact { Name = "Jane Smith", Address = "456 Oak St", Email = "jane.smith@example.com", PhoneNumber = "555-5678" };
        
        // Act
        _repository.Add(contact1);
        _repository.Add(contact2);

        var contacts = _repository.GetAll().ToList();

        // Assert
        Assert.AreEqual(2, contacts.Count, "There should be two contacts in the repository.");
        Assert.IsTrue(contacts.Contains(contact1) && contacts.Contains(contact2), "Both contacts should be retrievable from the repository.");
    }
}
