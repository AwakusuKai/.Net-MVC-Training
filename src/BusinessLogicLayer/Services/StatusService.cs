using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class StatusService : IStatusService
    {
        IRepository<Status> StatusRepository { get; set; }
        public StatusService(IRepository<Status> statusRepository)
        {
            StatusRepository = statusRepository;
        }

        public IEnumerable<StatusDTO> GetStatuses()
        {

            List<StatusDTO> statusDTOs = new List<StatusDTO>();
            foreach (Status status in StatusRepository.GetAll())
            {
                statusDTOs.Add(Mapper.Convert<Status, StatusDTO>(status));
            }
            return statusDTOs;
        }
    }
}
