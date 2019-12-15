using Gestamp.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        // Lo primero de todo es inyectar nuestro datacontext
        // para los datos mediante el constructo de la clase
        private readonly DBContext _context;
        public AuthRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            //if (!VerifyPasswordHash(password, user.PasswordHast, user.PasswordSalt))
            //    return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHast, byte[] passwordSalt)
        {
            // Ahora es igual que el metodo CreatePasswordHash solo que tenemos que comparar
            // si concuerdan las passwordSalts byte por byte, si hay alguno diferente es que 
            // no es la misma:
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHast[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHast = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
