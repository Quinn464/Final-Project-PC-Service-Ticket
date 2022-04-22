﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTicket.Data
{
    // .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
    //| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
    //| |   ______     | || |     ______   | || |    _______   | || |  _________   | || |  _______     | || | ____ ____    | || |     _____    | || |     ______   | || |  _________   | |
    //| |  |_ __   \   | || |   .' ___  |  | || |   /  ___  |  | || | |_   ___  |  | || | |_   __ \    | || ||_  _| |_  _| | || |    |_   _|   | || |   .' ___  |  | || | |_ ___  |  | |
    //| |    | |__) |  | || |  / .'   \_|  | || |  |  (__ \_|  | || |   | |_  \_|  | || |   | |__) |   | || |  \ \   / /   | || |      | |     | || |  / .'   \_|  | || |   | |_  \_|  | |
    //| |    |  ___/   | || |  | |         | || |   '.___`-.   | || |   |  _|  _   | || |   |  __ /    | || |   \ \ / /    | || |      | |     | || |  | |         | || |   |  _|  _   | |
    //| |   _| |_      | || |  \ `.___.'\  | || |  |`\____) |  | || |  _| |___/ |  | || |  _| |  \ \_  | || |    \ ' /     | || |     _| |_    | || |  \ `.___.'\  | || |  _| |___/ |  | |
    //| |  |_____|     | || |   `._____.'  | || |  |_______.'  | || | |_________|  | || | |____| |___| | || |     \_/      | || |    |_____|   | || |   `._____.'  | || | |_________|  | |
    //| |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | |
    //| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
    // '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 

    
    public enum CustomerStatus
    {
        Active = 0,
        Deactivated = 1
    };

    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public CustomerStatus Status { get; set; }

        
        public virtual List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}