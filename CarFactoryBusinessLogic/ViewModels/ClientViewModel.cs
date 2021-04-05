﻿using System.ComponentModel;
using System.Runtime.Serialization;
namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("ФИО клиента")]
        public string ClientFIO { get; set; }
        [DataMember]
        [DisplayName("Email")]
        public string Email { get; set; }
        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}