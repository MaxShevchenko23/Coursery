using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coursery.Infrastucture.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(CourseryDbContext context)
    {
        _context = context;
    }

    public CourseryDbContext _context { get; set; }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = _context.Users;
        return await users.ToListAsync();
    }

    public async Task<User> AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity != null)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    
    public void Dispose()
    { 
        _context.Dispose();
    }

    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        return user;
    }
}