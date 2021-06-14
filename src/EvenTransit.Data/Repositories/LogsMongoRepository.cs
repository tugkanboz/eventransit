using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EvenTransit.Core.Abstractions.Data;
using EvenTransit.Core.Dto;
using EvenTransit.Core.Entities;
using EvenTransit.Core.Enums;
using MongoDB.Driver;

namespace EvenTransit.Data.Repositories
{
    public class LogsMongoRepository : BaseMongoRepository<Logs>, ILogsRepository
    {
        private readonly IMapper _mapper;

        public LogsMongoRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task InsertLogAsync(LogsDto model)
        {
            var data = _mapper.Map<Logs>(model);
            data.CreatedOn = DateTime.UtcNow;

            await Collection.InsertOneAsync(data);
        }

        public async Task<LogFilterDto> GetLogsAsync(Expression<Func<Logs, bool>> predicate, int page)
        {
            const int perPage = 100;

            var count = await Collection.Find(predicate).CountDocumentsAsync();
            var totalPages = (int) Math.Ceiling((double) count / perPage);
            var result = await Collection.Find(predicate)
                .Sort(Builders<Logs>.Sort.Ascending(x => x.CreatedOn))
                .Skip((page - 1) * perPage)
                .Limit(perPage)
                .ToListAsync();

            return new LogFilterDto
            {
                Items = result,
                TotalPages = totalPages
            };
        }

        public async Task<LogsDto> GetByIdAsync(string id)
        {
            var log = await Collection.Find(x => x._id == id).FirstOrDefaultAsync();
            return _mapper.Map<LogsDto>(log);
        }

        public async Task<long> GetLogsCount(DateTime startDate, DateTime endDate, LogType type)
        {
            return await Collection.CountDocumentsAsync(x =>
                x.CreatedOn >= startDate && x.CreatedOn <= endDate && x.LogType == type);
        }
    }
}