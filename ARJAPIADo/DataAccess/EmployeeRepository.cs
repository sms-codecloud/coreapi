using ARJAPIADo.Models;
using System.Data.SqlClient;

namespace ARJAPIADo.DataAccess
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Employees", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNo = reader.GetString(3),
                            Email = reader.GetString(4),
                            Address = reader.GetString(5)
                        });
                    }
                }
            }
            return employees;
        }

        public Employee? GetEmployeeById(int id)
        {
            Employee? employee = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Employees WHERE EmployeeId = @EmployeeId", connection);
                command.Parameters.AddWithValue("@EmployeeId", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNo = reader.GetString(3),
                            Email = reader.GetString(4),
                            Address = reader.GetString(5)
                        };
                    }
                }
            }
            return employee;
        }

        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Employees (Name, Quantity, Price) VALUES (@FirstName,@LastName,@PhoneNo, @Email, @Address)", connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@LastName", employee.PhoneNo);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Address", employee.Address);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, PhoneNo = @PhoneNo, Email = @Email, Address = @Address WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Address", employee.Address);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Employees WHERE EmployeeId = @EmployeeId", connection);
                command.Parameters.AddWithValue("@EmployeeId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
