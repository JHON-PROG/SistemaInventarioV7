using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV7.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }

        public async Task Agregar(T entidad)
        {
            await DbSet.AddAsync(entidad); // insert into table
        }

        public async Task<T> Obtener(int id)
        {
            return await DbSet.FindAsync(id); // select * from
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string incluirPropiedades = null, bool isTraking = true)
        {
           IQueryable<T> query = DbSet;

            if (filtro !=null)
            {
                query = query.Where(filtro); //select * from whewre ....(filtro)
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //select * from inner join (categoria,marca)
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query); // order by
            }

            if (!isTraking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();   
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTraking = true)
        {
            IQueryable<T> query = DbSet;

            if (filtro != null)
            {
                query = query.Where(filtro); //select * from whewre ....(filtro)
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); //select * from inner join (categoria,marca)
                }
            }

            if (!isTraking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }


        public void Remover(T entidad)
        {
           DbSet.Remove(entidad); // delete
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            DbSet.RemoveRange(entidad);
        }
    }
}
