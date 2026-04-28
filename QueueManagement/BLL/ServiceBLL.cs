using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class ServiceBLL
    {
        private ServiceDAL _dal = new ServiceDAL();

        public List<Service> GetAllServices()
        {
            return _dal.GetAllServices();
        }

        public List<Service> GetServicesByBranch(int branchId)
        {
            return _dal.GetServicesByBranch(branchId);
        }

        public void AddService(Service s)
        {
            if (string.IsNullOrWhiteSpace(s.ServiceName))
                throw new System.Exception("Service name cannot be empty");
            if (s.AvgDuration <= 0)
                throw new System.Exception("Average duration must be greater than 0");
            _dal.AddService(s);
        }

        public void UpdateService(Service s)
        {
            if (string.IsNullOrWhiteSpace(s.ServiceName))
                throw new System.Exception("Service name cannot be empty");
            if (s.AvgDuration <= 0)
                throw new System.Exception("Average duration must be greater than 0");
            _dal.UpdateService(s);
        }

        public void DeleteService(int serviceId)
        {
            _dal.DeleteService(serviceId);
        }
    }
}