﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using MB.Data.Models;
using System.Collections.Generic;

namespace MB.Data.DTO
{
    public class UserRoleDTO
    {
        public UserRoleDTO()
        {
            this.Permission = new List<int>();
        }

        public string Name { get; set; }
        public int Id { get; set; }

        public List<int> Permission { get; set; }
    }
}
