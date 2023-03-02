using Bogus;

static class StudentHelpers // with extension print method
{
    static readonly Faker<Student> _faker;

    static StudentHelpers()
    {
        _faker = new Faker<Student>()
            .RuleFor(s => s.Id, f => f.IndexGlobal + 1)
            .RuleFor(s => s.FirstName, f => f.Name.FirstName())
            .RuleFor(s => s.LastName, f => f.Name.LastName())
            .RuleFor(s => s.Mark, f => f.Random.Float(1, 12))
            .RuleFor(s => s.Group, f => f.Company.CompanyName())
            .RuleFor(s => s.BirthDate, f => f.Date.Between(new DateTime(1997, 1, 1), new DateTime(2007, 1, 1)))
            .RuleFor(s => s.Email, f => f.Internet.Email());
    }


    public static List<Student> Generate(int count)
        => _faker.Generate(count);



    public static void Print(this Student student)
    {
        Console.WriteLine(@$"
                    Id: {student.Id}
                    FistName: {student.FirstName}
                    LastName: {student.LastName}
                    Mark: {student.Mark.ToString("N2")}
                    Mark: {student.Group}
                    BirthDate: {student.BirthDate.ToShortDateString()}
                    Email: {student.Email}");
    }
}