﻿using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class Validador
    {
        /// <summary>
        /// Valida todos los datos referidos al usuario, invocando cada método.
        /// Si un método validador devuelve false, ese atributo se escribe en un mensaje de error.
        /// Si ese mensaje supera los 19 caracteres significará que se concibieron validaciones falsas
        /// y por lo tanto lanzará una excepción detallando cada dato no válido.
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasenia"></param>
        /// <exception cref="Exception"></exception>
        public static void ValidarDatosUsuario(object rol, string nombre, string apellido,
                                                 string dni, string nombreUsuario, string contrasenia)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Datos no válidos:");

            if (!ValidarItemEnumerado(rol, typeof(ERol)))
                sb.AppendLine("- Rol");

            if (!ValidarStringConLetraSinDigitoYRango(nombre, 2, 24))
                sb.AppendLine("- Nombre");

            if (!ValidarStringConLetraSinDigitoYRango(apellido, 2, 24))
                sb.AppendLine("- Apellido");

            if (!ValidarStringNumericoRango(dni, 7, 9))
                sb.AppendLine("- DNI");

            if (!ValidarStringConLetraDigitoYRango(nombreUsuario, 4, 17))
                sb.AppendLine("- Nombre de Usuario");

            if (!ValidarStringConLetraDigitoYRango(contrasenia, 7, 17))
                sb.AppendLine("- Contraseña");

            if (sb.Length > 19)
                throw new Exception(sb.ToString());
        }

        /// <summary>
        /// Valida todos los datos referidos al producto, invocando cada método.
        /// Si un método validador devuelve false, ese atributo se escribe en un mensaje de error.
        /// Si ese mensaje supera los 19 caracteres significará que se concibieron validaciones falsas
        /// y por lo tanto lanzará una excepción detallando cada dato no válido.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="nombre"></param>
        /// <param name="marca"></param>
        /// <param name="precio"></param>
        /// <param name="stock"></param>
        /// <param name="descripcion"></param>
        /// <exception cref="Exception"></exception>
        public static void ValidarDatosProducto(object tipo, string nombre, string marca, float precio,
                                            float stock, string descripcion)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Datos no válidos:");

            if (!ValidarItemEnumerado(tipo, typeof(ETipo)))
                sb.AppendLine("- Tipo");

            if (!ValidarStringConLetraSinDigitoYRango(nombre, 2, 24))
                sb.AppendLine("- Nombre");

            if (!ValidarStringConLetraSinDigitoYRango(marca, 2, 24))
                sb.AppendLine("- Marca");

            if (!ValidarStringRango(descripcion, 4, 121))
                sb.AppendLine("- Descripción");

            if (!ValidarFloatRango((float)stock, 0, 2001))
                sb.AppendLine("- Stock");

            if (!ValidarFloatRango((float)precio, 0, 5001))
                sb.AppendLine("- Precio");

            if (sb.Length > 19)
                throw new Exception(sb.ToString());
        }

        public static bool ValidarStringConLetraSinDigitoYRango(string cadena, int minimo, int maximo)
        {
            if (!ValidarStringRango(cadena, minimo, maximo))
                return false;

            if (!ValidarStringConLetraGuionYEspacio(cadena))
                return false;

            if (!ValidarStringSinEspacioOGuionSeguido(cadena))
                return false;

            return true;
        }

        public static bool ValidarStringConLetraDigitoYRango(string cadena, int minimo, int maximo)
        {
            if (!ValidarStringRango(cadena, minimo, maximo))
                return false;

            if (!ValidarStringLetraODigito(cadena))
                return false;

            return true;
        }

        public static bool ValidarStringRango(string cadena, int minimo, int maximo)
        {
            return (!string.IsNullOrEmpty(cadena)) && cadena.Length > minimo && cadena.Length < maximo;
        }

        public static bool ValidarFloatRango(float cantidad, float minimo, float maximo)
        {
            return cantidad > minimo && cantidad < maximo;
        }

        public static bool ValidarStringNumericoRango(string cadena, int minimo, int maximo)
        {
            if (!ValidarStringRango(cadena, minimo, maximo))
                return false;

            foreach (char caracter in cadena)
            {
                if (!char.IsNumber(caracter))
                    return false;
            }

            return true;
        }

        public static bool ValidarItemEnumerado(object itemSeleccionado, Type tipoEnumerado)
        {
            if (itemSeleccionado is null)
                return false;

            if (!Enum.IsDefined(tipoEnumerado, itemSeleccionado))
                return false;

            return true;
        }

        public static bool ValidarStringConLetraGuionYEspacio(string cadena)
        {
            foreach (char caracter in cadena) 
            {
                if (!char.IsLetter(caracter) && caracter != ' ' && caracter != '-')
                    return false;
            }

            return true;
        }

        public static bool ValidarStringSinEspacioOGuionSeguido(string cadena)
        {
            for (int i = 0; i < cadena.Length - 1; i++)
            {
                if ((cadena[i] == ' ' && cadena[i + 1] == ' ') || (cadena[i] == '-' && cadena[i + 1] == '-'))
                    return false;
            }

            return true;
        }

        public static bool ValidarStringLetraODigito(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!char.IsLetterOrDigit(caracter))
                    return false;
            }

            return true;
        }
    }
}
