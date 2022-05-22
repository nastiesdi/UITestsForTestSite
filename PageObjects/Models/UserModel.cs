namespace PageObjects.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }

        public override string ToString()
        {
            return $"First Name {this.FirstName}, " +
                $"Second Name = {this.LastName}, " +
                $"Email = {this.Email}," +
                $" age = {this.Age} " +
                $"Salary = {this.Salary}," +
                $" Department = {this.Department} \b";
        }

        public bool Equals(UserModel other)
        {
            return other != null && (other.FirstName == FirstName
                             && other.LastName == LastName
                             && other.Email == Email
                             && other.Age == Age
                             && other.Salary == Salary
                             && other.Department == Department);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserModel);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}