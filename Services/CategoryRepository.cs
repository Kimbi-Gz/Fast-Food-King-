using FastFoodKing.Data;
using FastFoodKing.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace FastFoodKing.Services
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private IElasticClient _elasticClient;
        public CategoryRepository(FastFoodKingContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Category>> All()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CategoryRepository));
                return new List<Category>();
            }
        }

        public override async void Upsert(Category entity)
        {
            try
            {
                var existingEntity = await _dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingEntity == null)
                {
                    await _elasticClient.IndexDocumentAsync(entity);
                    Add(entity);
                }


                existingEntity.Id = entity.Id;
                existingEntity.Title = entity.Title;
              

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(CategoryRepository));
                throw new Exception();
            }
        }
        public override async void Delete(int id)
        {
            try
            {
                var exist = await _dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) throw new Exception();

                _dbSet.Remove(exist);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CategoryRepository));
                throw new Exception();
            }
        }


    }
}
