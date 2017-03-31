using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrar
{
    class Program
    {


        static void Main(string[] args)
        {
            //Comentario : Aca agregamos todos los valores inventados de una persona
            string Nombre = "Alvaro Reyes";
            string Rut = "12312312-1";
            int Edad = 28;
            string Nacionalidad = "Chilena";
            int Monto = 550000;
            int Sueldo = 500000;
            int AmbiguedadTrabajoEnMeses = 55;
            bool Morocidades = false;

            bool cumple = true; //Esta es una variable de validacion


            Console.Write("Monto a solicitar :");
             int montoSolicitado = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            if(Edad <= 24 && Edad >= 79) //Validamos que tenga entre 24 y 79 años
            {
                cumple = false; // NO CUMPLE CON LA EDAD
            }
            if(Nacionalidad != "Chilena") //Validamos que no sea distinta a Chilena
            {
                cumple = false; // NO CUMPLE CON LA NACIONALIDAD
            }
            if(Sueldo <= 250000) //Validamos que su sueldo sea mayor o igual a 250000
            {
                cumple = false; // NO CUMPLE CON EL SUELDO
            }
            if(AmbiguedadTrabajoEnMeses <= 36) //Validamos que al menos tenga ambiguedad de 36 meses ( 3 años )
            {
                cumple = false; // NO CUMPLE CON LA AMBIGUEDAD
            }
            if(Morocidades == true) //Validamos que no tenga morocidades
            {
                cumple = false; // NO CUMPLE CON LA MOROCIDAD
            }

            if(cumple == true) //Si el valor Nocumple sigue valiendo 0 , significa que esta todo validado correctamente y no entro en ningun if
            {
                Console.WriteLine("----------------------\n");
                Console.WriteLine(Nombre + "    " + Rut + "\n");
                Console.WriteLine("----------------------\n");
                Console.WriteLine("Cumple con los requisitos");
                Console.WriteLine("Sueldo : " + Sueldo);
                Console.WriteLine("Monto Maximo : 5.000.000");
                Console.WriteLine("Monto solicitado : {0:N0}",montoSolicitado);
                Console.WriteLine("Taza mensual : 1,46 %");
                Console.WriteLine("Cuotas : 12\n");

                double totalApagar = montoSolicitado * 1.146; //sacamos el porcentaje de el total a pagar

                Console.WriteLine("-----------------------");
                Console.WriteLine("Total a pagar : {0:N0}", totalApagar);
                Console.WriteLine("-----------------------");

              

            }
            else //Si el valor de Nocumple es distinto de 0, significa que no cumple con algunas de las condiciones que nos piden
            {
                Console.WriteLine("----------------------\n");
                Console.WriteLine(Nombre + "    " + Rut + "\n");
                Console.WriteLine("----------------------\n");
                Console.WriteLine("NO CUMPLE CON LOS REQUISITOS");

            }

            Console.ReadKey();

        }
    }
}
