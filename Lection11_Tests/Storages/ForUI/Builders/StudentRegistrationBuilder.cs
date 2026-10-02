using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Storages.ForUI.Models;
using Lection11_Tests.Enums;

namespace Lection11_Tests.Storages.ForUI.Builders
{
    public class StudentRegistrationBuilder
    {
        private StudentRegistrationFormModel student = new StudentRegistrationFormModel();

        public StudentRegistrationBuilder WithFirstName(string firstName)
        {
            student.FirstName = firstName;
            return this;
        }

        public StudentRegistrationBuilder WithLastName(string lastName)
        {
            student.LastName = lastName;
            return this;
        }

        public StudentRegistrationBuilder WithGender(GenderType gender)
        {
            student.Gender = gender;
            return this;
        }

        public StudentRegistrationBuilder WithEmail(string email)
        {
            student.Email = email;
            return this;
        }

        public StudentRegistrationBuilder WithMobile(string number)
        {
            student.MobileNumber = number;
            return this;
        }

        public StudentRegistrationBuilder WithDateOfBirth(DateTime date)
        {
            student.DateOfBirth = date;
            return this;
        }

        public StudentRegistrationBuilder WithSubjects(params string[] subjects)
        {
            student.Subjects = subjects.ToList();
            return this;
        }

        public StudentRegistrationBuilder WithHobbies(params HobbyType[] hobbies)
        {
            student.Hobbies = hobbies.ToList();
            return this;
        }

        public StudentRegistrationBuilder WithPicture(string path)
        {
            student.PicturePath = path;
            return this;
        }

        public StudentRegistrationBuilder WithAddress(string address)
        {
            student.CurrentAddress = address;
            return this;
        }
        public StudentRegistrationBuilder WithLocation(string state, string city)
        {
            student.State = state;
            student.City = city;
            return this;
        }

        public StudentRegistrationFormModel Build()
        {
            return student;
        }
    }
}
