using DsK.ITSM.MinimalAPI.Data;
using DsK.ITSM.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DsK.ITSM.MinimalAPI.Services;

public class KeyValueService
{
    private readonly AppDbContext _db;

    public KeyValueService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<KeyValueEntity>> GetAllAsync() =>
        await _db.KeyValues.ToListAsync();

    public async Task<KeyValueEntity?> GetByKeyAsync(string key) =>
        await _db.KeyValues.FirstOrDefaultAsync(kv => kv.Key == key);

    public async Task<KeyValueEntity> SetAsync(string key, string value)
    {
        var existing = await _db.KeyValues.FirstOrDefaultAsync(kv => kv.Key == key && kv.Value == value);
        if (existing == null)
        {
            existing = new KeyValueEntity { Key = key, Value = value };
            _db.KeyValues.Add(existing);
            await _db.SaveChangesAsync();
        }
        return existing;
    }

    public async Task<bool> DeleteAsync(string key)
    {
        var item = await _db.KeyValues.FirstOrDefaultAsync(kv => kv.Key == key);
        if (item == null) return false;
        _db.KeyValues.Remove(item);
        await _db.SaveChangesAsync();
        return true;
    }
}
