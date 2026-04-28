using System;
using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class CallLogBLL
    {
        private CallLogDAL _dal = new CallLogDAL();

        public List<CallLog> GetAllLogs()
        {
            return _dal.GetAllLogs();
        }

        public List<CallLog> GetLogsByDate(DateTime date)
        {
            return _dal.GetLogsByDate(date);
        }
    }
}