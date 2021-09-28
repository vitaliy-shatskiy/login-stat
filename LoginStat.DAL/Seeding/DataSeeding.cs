using System;
using System.Collections.Generic;
using Bogus;
using Bogus.Extensions;
using LoginStat.DAL.Entities;

namespace LoginStat.DAL.Seeding
{
    public static class DataSeeding
    {
        public static (ICollection<User> users, ICollection<UserLoginAttempt> userLoginAttempts)
            GenerateAllData(int count)
        {
            var users = GenerateRandomUsers(count);
            var userLoginAttempts = GenerateRandomUserLoginAttempts(users, count * 2);
            return (users, userLoginAttempts);
        }

        private static ICollection<User> GenerateRandomUsers(int count)
        {
            var uniqueNumber = 0;
            var usersFake = new Faker<User>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.Name, faker => faker.Name.FirstName().ClampLength(2, 30, ' '))
                .RuleFor(p => p.Surname, faker => faker.Name.LastName().ClampLength(2, 30, ' '))
                .RuleFor(p => p.Email,
                    (faker, u) => faker.Internet.Email(
                            u.Name.ClampLength(1, 2),
                            u.Surname.ClampLength(1, 2),
                            "mail.ua",
                            (uniqueNumber++).ToString())
                        .ToLower()
                        .ClampLength(5, 25));
            return usersFake.Generate(count);
        }

        private static ICollection<UserLoginAttempt> GenerateRandomUserLoginAttempts(ICollection<User> users, int count)
        {
            var userLogins = new Faker<UserLoginAttempt>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.UserId, faker => faker.PickRandom(users).Id)
                .RuleFor(p => p.IsSuccess, faker => faker.Random.Bool())
                .RuleFor(p => p.AttemptTime, faker => faker.Date.Between(faker.Date.Past(), DateTime.Now));
            return userLogins.Generate(count);
        }
    }
}