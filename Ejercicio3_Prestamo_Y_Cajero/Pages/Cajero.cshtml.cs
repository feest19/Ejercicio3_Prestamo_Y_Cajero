using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ejercicio3_Prestamo_Y_Cajero.Pages
{
    public class CajeroModel : PageModel
    {
        public const int maxretiro = 10000;
        public const int minretiro = 2000;
        public int totaldisponiblecajero = 28400;
        public string mensaje { get; set; }
        public bool bancoABC;
        public bool error { get; set; }
        public int cantidadretiro { get; set; } = 0;

        public readonly List<Papeleta> papeletas;
        public class Papeleta
        {
            public int valor { get; set; }
            public int cantidad { get; set; }
            public int cantidadenuso { get; set; }
        }

        public CajeroModel()
        {
            this.papeletas = new List<Papeleta>() {

                new Papeleta(){valor=1000,cantidad=9},
                new Papeleta(){valor=500, cantidad=19},
                new Papeleta(){valor=100,cantidad=99}
            };
        }
        public void OnPost(int cantidadretiro)
        {

            if (cantidadretiro > totaldisponiblecajero)
            {
                this.error = true;
                mensaje = "El cajero no dispone de suficiente efectivo para realizar la transaccion";
            }
            else if (cantidadretiro > maxretiro)
            {
                this.error = true;
                mensaje = "Solo puede realizar un retiro maximo de RD$10,000";
            }
            else if (cantidadretiro < minretiro)
            {
                this.error = true;
                mensaje = "la cantidad minima de retiro es de: RD$2,000";
            }
            foreach (var item in this.papeletas)
            {
                //item.cantidadenuso = 0;
                this.obtenerCantidades(ref cantidadretiro, item);
                this.totaldisponiblecajero -= cantidadretiro;
            }
            //this.totaldisponiblecajero = totaldisponiblecajero - cantidadretiro;

            //if (cantidadretiro > 0)
            //{
            //    this.error = false;
            //    this.mensaje = "No se puede realizar esta transaccion";
            //    foreach (var item in papeletas)
            //    {
            //        item.cantidad += item.cantidadenuso;
            //        item.cantidadenuso = 0;
            //    }
            //    return;
            //}
            //else
            //{
            //    if (cantidadretiro > 2000)
            //    {
            //        this.error = true;
            //        this.mensaje = "La cantidad maxima permitida de otros banco es de 2,000 por transaccion.";

            //    }
            //    else if (cantidadretiro < 2000)
            //    {
            //        foreach (var item in this.papeletas)
            //        {
            //            item.cantidadenuso = 0;
            //            this.obtenerCantidades(ref cantidadretiro, item);
            //        }
            //    }
            //}
        }
        private int obtenerCantidades(ref int cantidadretiro, Papeleta papeleta)
        {
            if (papeleta.cantidad == 0)
            {
                return 0;
            }
            var residuoDivision = cantidadretiro % papeleta.valor;
            var cantBilletes = cantidadretiro / papeleta.valor;
            if (residuoDivision > 0)
            {
                papeleta.cantidad = 0;
            }
            else
            {
                papeleta.cantidad -= cantBilletes;
            }
            var retirado = cantBilletes * papeleta.valor;
            cantidadretiro -= retirado;
            papeleta.cantidadenuso = cantBilletes;
            return retirado;

        }
        public void OnGet()
        {
        }
    }

}
