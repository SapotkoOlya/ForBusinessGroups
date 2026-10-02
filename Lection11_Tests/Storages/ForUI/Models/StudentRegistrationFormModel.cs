using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Enums;

namespace Lection11_Tests.Storages.ForUI.Models
{
    public class StudentRegistrationFormModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> Subjects { get; set; }
        public List<HobbyType> Hobbies { get; set; }
        public string PicturePath { get; set; }
        public string CurrentAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}
