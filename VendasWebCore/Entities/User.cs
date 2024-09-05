using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.Entities
{
    public class User
    {
        public int UserId {  get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public List<Pedido>? Pedidos { get; private set; }
        public List<Produto>? Produtos { get; private set; }

        public User(int id, string fullName, string email, bool active, string role)
        {
            UserId = id;
            FullName = fullName;
            Email = email;
            Active = active;
            Role = role;
        }

        public User(string fullName, string email, string password, string role)
        {
            FullName = fullName;
            Email = email;
            CreatedAt = DateTime.Now;
            Active = true;
            Password = password;
            Role = role;
        }

        public void UpdateUsuario(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.FullName))
            {
                FullName = user.FullName;
            }
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                Email = user.Email;
            }            
            if (!string.IsNullOrWhiteSpace(user.Role))
            {
                Role = user.Role;
            }
            Active = user.Active;
        }

        public void UpdatePassword(string password)
        {            
            Password = password;
        }
    }
}
