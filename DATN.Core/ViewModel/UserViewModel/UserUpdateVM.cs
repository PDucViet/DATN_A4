﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModels.UserViewModel
{
    public class UserUpdateVM
    {
        public Guid Id { get; set; }
        public bool isActive { get; set; }
    }
}