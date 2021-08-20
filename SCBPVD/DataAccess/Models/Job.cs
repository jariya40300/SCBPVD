using System;
using System.Collections.Generic;
using System.Text;

namespace SCBPVD.DataAccess.Models
{
  public  class Job
    {
      public int id {get;set;}
      public DateTime create_date {get;set;}
      public string create_by {get;set;}
      public int total_company {get;set;}
      public DateTime cycle {get;set;}
      public string code_cycle {get;set;}
    }
}
