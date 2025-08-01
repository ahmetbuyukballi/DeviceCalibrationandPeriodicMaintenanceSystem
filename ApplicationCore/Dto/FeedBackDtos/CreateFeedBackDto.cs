using Domain.Entites;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.FeedBackDtos
{
    public class CreateFeedBackDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? UserId { get; set; }
        public Guid? DeviceId { get; set; }

    }
}
