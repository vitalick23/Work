using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.DTO;
using UserStories.BLL.Entities;
using UserStories.BLL.Infrastructure;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;

namespace UserStories.BLL.Services
{
    public class StoriesServise : IStoriesSevises
    {
        IUnitOfWork Database { get; set; }

        public StoriesServise(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<OperationDetails> Create(Stories storiesDto)
        {
            try
            {
                var stories = new Stories()
                {
                    Id = storiesDto.Id,
                    IdUser = storiesDto.IdUser,
                    Story = storiesDto.Story,
                    TimePublicate = storiesDto.TimePublicate
                };
                Database.StoriesManager.Create(stories);
                await Database.SaveAsync();
                return new OperationDetails(true, "История добавленна");

            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }
        }
    }
}
