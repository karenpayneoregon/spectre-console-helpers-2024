using System.Data;
using System.Data.SQLite;
using Dapper;
using SamplesApplication.Handlers;
using SamplesApplication.Models;

namespace SamplesApplication.Classes;
internal class DapperOperations
{
    private IDbConnection _cn;
    private static string _name = "SpectreConsole.db";
    private static string ConnectionString()
        => $"Data Source={_name}";

    public DapperOperations()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        _cn = new SQLiteConnection(ConnectionString());
    }
    /// <summary>
    /// Retrieves a list of all persons from the database.
    /// </summary>
    /// <returns>A list of <see cref="Person"/> objects representing all persons in the database.</returns>
    public List<Person> List()
        => _cn.Query<Person>("SELECT Id, FirstName, LastName, BirthDate FROM Person").ToList();

    /// <summary>
    /// Inserts a new person into the database.
    /// </summary>
    /// <param name="person">The <see cref="Person"/> object representing the person to be inserted.</param>
    public Person Insert(Person person)
    {
        var result = _cn.ExecuteScalar(
            """
                INSERT INTO Person (FirstName, LastName, BirthDate) VALUES (@FirstName, @LastName, @BirthDate)
                Returning RowId
                """, person);

        person.Id = Convert.ToInt32(result);
        return person;
    }
}
