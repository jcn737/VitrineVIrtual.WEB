using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VitrineVirtual.Model
{
    /// <summary>
    ///     Modelo de endereço, todos os dados setados nessa classe
    ///     serão submetido as validações como pode ver.
    /// </summary>
    /// <see cref="http://volkoinen.github.com/Correios.Net" />
    /// <see cref="https://github.com/volkoinen/Correios.Net" />
    public class CEP
    {
        private string _city;
        private string _district;
        private string _state;
        private string _street;
        private string _zip;

        [Key]
        public int ID_CEP { get; set; }

        /// <summary>
        ///     A validação para o CEP permite apenas strings de
        ///     oito nove digitos com ou sem máscara, apenas seguindo
        ///     os seguintes padroes: 99999999 ou 99999-999.
        /// </summary>
        /// <see cref="http://volkoinen.github.com/Correios.Net" />
        /// <see cref="https://github.com/volkoinen/Correios.Net" />
        public String Zip
        {
            get { return _zip; }
            set
            {
                _zip = value;
            }
        }

        /// <summary>
        ///     A validação do endereço verifica apenas se o mesmo
        ///     tem um número de caracteres maior do que 500.
        /// </summary>
        /// <see cref="http://volkoinen.github.com/Correios.Net" />
        /// <see cref="https://github.com/volkoinen/Correios.Net" />
        public String Street
        {
            get { return _street; }
            set
            {
                _street = value;
            }
        }

        /// <summary>
        ///     A validação do distrito verifica apenas se o valor informado
        ///     tem um tamanho de no máximo 500 caracteres.
        /// </summary>
        /// <see cref="http://volkoinen.github.com/Correios.Net" />
        /// <see cref="https://github.com/volkoinen/Correios.Net" />
        public String District
        {
            get { return _district; }
            set
            {
                _district = value;
            }
        }

        /// <summary>
        ///     A validação da ciade verifica apenas se o valor informado
        ///     tem um tamanho de no máximo 500 caracteres.
        /// </summary>
        /// <see cref="http://volkoinen.github.com/Correios.Net" />
        /// <see cref="https://github.com/volkoinen/Correios.Net" />
        public String City
        {
            get { return _city; }
            set
            {
                _city = value;
            }
        }

        /// <summary>
        ///     Verifica se o UF informado é
        /// </summary>
        /// <see cref="http://volkoinen.github.com/Correios.Net" />
        /// <see cref="https://github.com/volkoinen/Correios.Net" />
        public String State
        {
            get { return _state; }
            set
            {
                bool validState = false;

                string[] states =
                {
                    "AC",
                    "AL",
                    "AM",
                    "AP",
                    "BA",
                    "CE",
                    "DF",
                    "ES",
                    "GO",
                    "MA",
                    "MG",
                    "MS",
                    "MT",
                    "PA",
                    "PB",
                    "PE",
                    "PI",
                    "PR",
                    "RJ",
                    "RN",
                    "RO",
                    "RR",
                    "RS",
                    "SC",
                    "SE",
                    "SP",
                    "TO"
                };

                foreach (string state in states)
                {
                    if (value.ToUpper() == state)
                    {
                        validState = true;
                        _state = value.ToUpper();
                    }
                }
            }
        }

        /// <summary>
        ///     True quando for um cep único para toda a cidade.
        /// </summary>
        public Boolean UniqueZip { get; set; }
    }
}