using FiapStore.Entities;
using FiapStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapStore.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : Entidade
    {

        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            /*
            _context.Usuario.Add(entidade);
            _context.SaveChanges();
            */


            /*
            _context.Entry(entidade).State = EntityState.Added;
            _context.Add(entidade);
            _context.SaveChanges();
            */

            _dbSet.Add(entidade);
            _context.SaveChanges();

        }

        public void Deletar(int id)
        {
            _dbSet.Remove(ObterPorId(id));
            _context.SaveChanges();

        }

        public T ObterPorId(int id)
        {
            return _dbSet.FirstOrDefault(t => t.Id == id);
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}
