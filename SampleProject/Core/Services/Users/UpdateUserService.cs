using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public void Update(User user, string name, string email, int age, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            user.SetEmail(email);
            user.SetName(name);
            user.SetAge(age);
            user.SetType(type);
            if (annualSalary.HasValue)
            {
                user.SetMonthlySalary(annualSalary.Value / 12);
            }
            user.SetTags(tags);
        }
    }
}