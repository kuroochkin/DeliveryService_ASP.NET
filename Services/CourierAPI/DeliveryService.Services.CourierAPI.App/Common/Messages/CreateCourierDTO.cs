﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.CourierAPI.App.Common.Messages;

public class CreateCourierDTO
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string UserName { get; set; }
}
