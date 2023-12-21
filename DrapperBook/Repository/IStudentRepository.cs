using DrapperBook.Models;

namespace DrapperBook.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
        Task<int> AddStudent(Student student);
        Task<int> UpdateStudent(Student student);

        Task<int> DeleteStudent(int id);
    }
}
