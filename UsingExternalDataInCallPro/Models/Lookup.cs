﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UsingExternalDataInCallPro.Models
{
    public class Lookup
    {
        public int ID { get; set; }
        public int CLEntryID { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Gereserveerd")]
        public bool Reserved { get; set; }

        public Lookup()
        {
        }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}