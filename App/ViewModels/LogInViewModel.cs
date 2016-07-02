using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class LogInViewModel
    {
        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="Por favor ingrese su correo.")]
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese su contraseña.")]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Administrador")]
        public bool IsAdmin { get; set; }
        #endregion

    }
}
