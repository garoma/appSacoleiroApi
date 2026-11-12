using Microsoft.AspNetCore.Identity;

namespace appSacoleiro.API
{
    public class GerarHash
    {
        public static void Executar()
        {
            var hasher = new PasswordHasher<object>();

            string senha = "12345678";

            string hash = hasher.HashPassword(new object(), senha);

            Console.WriteLine("HASH GERADO:");
            Console.WriteLine(hash);


            //INSERT INTO public."AdminUsers" (
            //    "Id",
            //    "Nome",
            //    "Email",
            //    "PasswordHash",
            //    "CreatedAtUtc"
            //)
            //VALUES(
            //    gen_random_uuid(),
            //    'Administrador Joker Jeans',
            //    'admin@jokerjeans.local',
            //    'AQAAAAIAAYagAAAAEAFkHYt5EQhh4E2Pdo4cnMooqho8RTPrVOaXT0PCRfo5g8OwhbQA3pvbt7toucRkrA==',
            //    NOW()
            //);

        }
    }
}